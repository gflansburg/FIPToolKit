using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPlaneConnect
{
    public class XPlaneConnector : IDisposable
    {
        private const int CheckInterval_ms = 1000;
        public TimeSpan MaxDataRefAge { get; private set; } = TimeSpan.FromSeconds(5);

        private CultureInfo EnCulture = new CultureInfo("en-US");

        private UdpClient server;
        private UdpClient client;
        public IPEndPoint XPlaneEP { get; private set; }
        private CancellationTokenSource ts;
        private Task serverTask;
        private Task observerTask;

        public delegate void RawReceiveHandler(string raw);
        public event RawReceiveHandler OnRawReceive;

        public delegate void DataRefReceived(DataRefElement dataRef);
        public event DataRefReceived OnDataRefReceived;

        public delegate void DataRefUpdated(List<DataRefElement> dataRefs);
        public event DataRefUpdated OnDataRefUpdated;

        public delegate void LogHandler(string message);
        public event LogHandler OnLog;

        public List<DataRefElement> DataRefs { get; private set; }
        private List<DataRefElement> UpdatedDataRefs = new List<DataRefElement>();
        public List<StringDataRefElement> StringDataRefs { get; private set; }
        private List<StringDataRefElement> UpdatedStringDataRefs = new List<StringDataRefElement>();
        public bool IsConnected { get; private set; }
        public bool IsStopping { get; private set; }
        public DateTime LastReceive { get; internal set; }
        public IEnumerable<byte> LastBuffer { get; internal set; }

        public IPEndPoint LocalEP
        {
            get
            {
                return ((IPEndPoint)client.Client.LocalEndPoint);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ip">IP of the machine running X-Plane, default 127.0.0.1 (localhost)</param>
        /// <param name="xplanePort">Port the machine running X-Plane is listening for, default 49000</param>
        public XPlaneConnector(string ip = "127.0.0.1", int xplanePort = 49000)
        {
            XPlaneEP = new IPEndPoint(IPAddress.Parse(ip), xplanePort);
            DataRefs = new List<DataRefElement>();
            StringDataRefs = new List<StringDataRefElement>();
        }

        /// <summary>
        /// Initialize the communication with X-Plane machine and starts listening for DataRefs
        /// </summary>
        public void Start()
        {
            if (!IsStopping)
            {
                client = new UdpClient();
                client.Client.ReceiveTimeout = 100;
                client.Connect(XPlaneEP.Address, XPlaneEP.Port);
                byte[] handshake = new byte[1];
                IPEndPoint remoteEP = null;

                try
                {
                    client.Send(handshake, 1);
                    client.Receive(ref remoteEP);
                    IsConnected = true;
                }
                catch (SocketException ex)
                {
                    IsConnected = ex.ErrorCode != 10054;
                }

                if (IsConnected)
                {
                    server = new UdpClient(LocalEP);
                    server.Client.ReceiveTimeout = 1;

                    ts = new CancellationTokenSource();
                    var token = ts.Token;

                    serverTask = Task.Factory.StartNew(() =>
                    {
                        while (!token.IsCancellationRequested && !IsStopping)
                        {
                            try
                            {
                                byte[] data = server.Receive(ref remoteEP);
                                IsConnected = true;
                                var raw = Encoding.UTF8.GetString(data);
                                LastReceive = DateTime.Now;
                                LastBuffer = data;
                                OnRawReceive?.Invoke(raw);
                                ThreadPool.QueueUserWorkItem(_ =>
                                {
                                    ParseResponse(data);
                                });
                            }
                            catch (SocketException ex)
                            {
                                IsConnected = ex.ErrorCode != 10054;
                            }
                        }

                        OnLog?.Invoke("Stopping server");
                        server.Close();
                    }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

                    observerTask = Task.Factory.StartNew(async () =>
                    {
                        while (!token.IsCancellationRequested && !IsStopping)
                        {
                            if (IsConnected && !IsStopping)
                            {
                                foreach (var dr in DataRefs)
                                {
                                    if (IsStopping)
                                    {
                                        break;
                                    }
                                    if (dr.Age > MaxDataRefAge || !dr.IsSubscribed)
                                    {
                                        dr.IsSubscribed = true;
                                        RequestDataRef(dr);
                                    }
                                }
                            }
                            await Task.Delay(CheckInterval_ms).ConfigureAwait(false);
                        }

                    }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                }
                else
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        /// <summary>
        /// Stops the comunications with the X-Plane machine
        /// </summary>
        /// <param name="timeout"></param>
        public void Stop(int timeout = 5000)
        {
            if (client != null)
            {
                IsStopping = true;
                
                UnsubscribeAll();

                if (ts != null)
                {
                    ts.Cancel();
                    Task.WaitAll(new[] { serverTask, observerTask }, timeout);
                    ts.Dispose();
                    ts = null;

                    client.Close();
                    client.Dispose();
                    client = null;

                    serverTask.Dispose();
                    serverTask = null;
                    observerTask.Dispose();
                    observerTask = null;
                }
                IsStopping = false;
            }
        }
        private void ParseResponse(byte[] buffer)
        {
            var pos = 0;
            var header = Encoding.UTF8.GetString(buffer, pos, 4);
            pos += 5; // Including tailing 0

            if (header == "RREF") // Ignore other messages
            {
                bool updated = false;
                UpdatedDataRefs.Clear();
                while (pos < buffer.Length)
                {
                    var id = BitConverter.ToInt32(buffer, pos);
                    pos += 4;

                    try
                    {
                        var value = BitConverter.ToSingle(buffer, pos);
                        pos += 4;
                        var localDataRefs = DataRefs.ToArray();
                        foreach (var dr in localDataRefs)
                        {
                            if (dr != null)
                            {
                                bool dataRefUpdated = dr.Update(id, value);
                                updated = dataRefUpdated ? true : updated;
                                if (dataRefUpdated)
                                {
                                    UpdatedDataRefs.Add(dr);
                                    OnDataRefReceived?.Invoke(dr);
                                }
                            }
                        }
                    }
                    catch (ArgumentException)
                    {

                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }
                }
                if (updated)
                    OnDataRefUpdated?.Invoke(UpdatedDataRefs);
            }
        }

        /// <summary>
        /// Sends a command
        /// </summary>
        /// <param name="command">Command to send</param>
        public void SendCommand(XPlaneCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var dg = new XPDatagram();
            dg.Add("CMND");
            dg.Add(command.Command);
            if (client != null)
                client.Send(dg.Get(), dg.Len);
        }

        /// <summary>
        /// Sends a command continously. Use return parameter to cancel the send cycle
        /// </summary>
        /// <param name="command">Command to send</param>
        /// <returns>Token to cancel the executing</returns>
        public CancellationTokenSource StartCommand(XPlaneCommand command)
        {
            var tokenSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (!tokenSource.IsCancellationRequested)
                {
                    SendCommand(command);
                }
            }, tokenSource.Token);

            return tokenSource;
        }

        public void StopCommand(CancellationTokenSource token)
        {
            token.Cancel();
        }

        /// <summary>
        /// Subscribe to a DataRef, notification will be sent every time the value changes
        /// </summary>
        /// <param name="dataref">DataRef to subscribe to</param>
        /// <param name="frequency">Times per seconds X-Plane will be seding this value</param>
        /// <param name="onchange">Callback invoked every time a change in the value is detected</param>
        public void Subscribe(DataRefElement dataref, int frequency = -1, Action<DataRefElement, float> onchange = null)
        {
            if (dataref == null)
                throw new ArgumentNullException(nameof(dataref));

            DataRefElement dataRefElement = DataRefs.FirstOrDefault(d => d.Id == dataref.Id && d.Index == dataref.Index);
            if (dataRefElement == null)
            {
                dataref.ForceUpdate = true;
                if (onchange != null)
                {
                    dataref.Subscribed = true;
                    DataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    dataref.Delegates.Add(changeHandler);
                    dataref.OnValueChange += changeHandler;
                }
                if (frequency > 0)
                    dataref.Frequency = frequency;

                DataRefs.Add(dataref);
            }
            else
            {
                dataRefElement.Frequency = frequency;
                dataRefElement.ForceUpdate = true;
                if (dataRefElement.Subscribed && onchange != null)
                {
                    DataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    if (!dataRefElement.Delegates.Contains(changeHandler))
                    {
                        dataRefElement.OnValueChange += (e, v) => { onchange(e, v); };
                    }
                }
                else if (!dataRefElement.Subscribed && onchange != null)
                {
                    dataRefElement.Subscribed = true;
                    DataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    dataRefElement.Delegates.Add(changeHandler);
                    dataRefElement.OnValueChange += changeHandler;
                }
                else if (dataRefElement.Subscribed && onchange == null)
                {
                    foreach (DataRefElement.NotifyChangeHandler changeHandler in dataRefElement.Delegates)
                    {
                        dataRefElement.OnValueChange -= changeHandler;
                    }
                    dataRefElement.Delegates.Clear();
                    dataRefElement.Subscribed = false;
                }
            }
        }

        /// <summary>
        /// Subscribe to a DataRef, notification will be sent every time the value changes
        /// </summary>
        /// <param name="dataref">DataRef to subscribe to</param>
        /// <param name="frequency">Times per seconds X-Plane will be seding this value</param>
        /// <param name="onchange">Callback invoked every time a change in the value is detected</param>
        public void Subscribe(StringDataRefElement dataref, int frequency = -1, Action<StringDataRefElement, string> onchange = null)
        {
            //if (onchange != null)
            //    dataref.OnValueChange += (e, v) => { onchange(e, v); };

            //Subscribe((DataRefElement)dataref, frequency);

            if (dataref == null)
                throw new ArgumentNullException(nameof(dataref));

            StringDataRefElement stringDataRefElement = StringDataRefs.FirstOrDefault(d => d.Id == dataref.Id);
            if (stringDataRefElement == null)
            {
                dataref.ForceUpdate = true;
                dataref.Frequency = frequency;

                if (onchange != null)
                {
                    dataref.Subscribed = true;
                    StringDataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    dataref.Delegates.Add(changeHandler);
                    dataref.OnValueChange += changeHandler;
                }

                for (var c = 0; c < dataref.StringLength; c++)
                {
                    var arrayElementDataRef = new DataRefElement
                    {
                        Id = dataref.Id,
                        DataRef = $"{dataref.DataRef}[{c}]",
                        Frequency = frequency,
                        Units = "String",
                        Description = dataref.Description,
                        Index = c
                    };

                    var currentIndex = c;
                    Subscribe(arrayElementDataRef, frequency, (e, v) =>
                    {
                        var character = Convert.ToChar(Convert.ToInt32(v));
                        dataref.Update(currentIndex, character);
                    });
                }

                StringDataRefs.Add(dataref);
            }
            else
            {
                stringDataRefElement.Frequency = frequency;
                stringDataRefElement.ForceUpdate = true;

                for (var c = 0; c < stringDataRefElement.StringLength; c++)
                {
                    var currentIndex = c;

                    string dataRef = $"{stringDataRefElement.DataRef}[{c}]";

                    DataRefElement dataRefElement = DataRefs.FirstOrDefault(d => d.Id == dataref.Id && d.Index == c);
                    if (dataRefElement != null)
                    {
                        dataRefElement.Frequency = frequency;
                        dataRefElement.ForceUpdate = true;
                    }
                }

                if (stringDataRefElement.Subscribed && onchange != null)
                {
                    StringDataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    if (!stringDataRefElement.Delegates.Contains(changeHandler))
                    {
                        stringDataRefElement.OnValueChange += (e, v) => { onchange(e, v); };
                    }
                }
                else if (!stringDataRefElement.Subscribed && onchange != null)
                {
                    stringDataRefElement.Subscribed = true;
                    StringDataRefElement.NotifyChangeHandler changeHandler = (e, v) => { onchange(e, v); };
                    stringDataRefElement.Delegates.Add(changeHandler);
                    stringDataRefElement.OnValueChange += changeHandler;
                }
                else if (stringDataRefElement.Subscribed && onchange == null)
                {
                    foreach (StringDataRefElement.NotifyChangeHandler changeHandler in stringDataRefElement.Delegates)
                    {
                        stringDataRefElement.OnValueChange -= changeHandler;
                    }
                    stringDataRefElement.Delegates.Clear();
                    stringDataRefElement.Subscribed = false;
                }
            }
        }

        private void RequestDataRef(DataRefElement element)
        {
            if (client != null)
            {
                var dg = new XPDatagram();
                dg.Add("RREF");
                dg.Add(element.Frequency);
                dg.Add(element.DataGramId);
                dg.Add(element.DataRef);
                dg.FillTo(413);
                if (client != null)
                    client.Send(dg.Get(), dg.Len);

                OnLog?.Invoke($"Requested {element.DataRef}@{element.Frequency}Hz with Id:{element.DataGramId}");
            }
        }

        /// <summary>
        /// Checks if X-Plane is sending data for this DataRef
        /// </summary>
        /// <param name="dataref">DataRef to check</param>
        public bool IsSubscribedToDataRef(DataRefElement dataRef)
        {
            return DataRefs.FirstOrDefault(d => d.Id == dataRef.Id) != null;
        }

        /// <summary>
        /// Checks if X-Plane is sending data for this DataRef
        /// </summary>
        /// <param name="dataref">DataRef to check</param>
        public bool IsSubscribedToDataRef(StringDataRefElement stringDataRef)
        {
            return StringDataRefs.FirstOrDefault(d => d.Id == stringDataRef.Id) != null;
        }

        /// <summary>
        /// Informs X-Plane to stop sending all DataRefs
        /// </summary>
        public void UnsubscribeAll()
        {

            var localStringDataRefs = StringDataRefs.ToArray();
            foreach (var dr in localStringDataRefs)
                Unsubscribe(dr);
            var localDataRefs = DataRefs.ToArray();
            foreach (var dr in localDataRefs)
                Unsubscribe(dr);
        }

        /// <summary>
        /// Informs X-Plane to stop sending this DataRef
        /// </summary>
        /// <param name="dataref">DataRef to unsubscribe to</param>
        public void Unsubscribe(DataRefElement dataref)
        {
            DataRefElement dr = DataRefs.FirstOrDefault(d => d.Id == dataref.Id && d.Index == dataref.Index);
            if (dr != null)
            {
                var dg = new XPDatagram();
                dg.Add("RREF");
                dg.Add(dr.DataGramId);
                dg.Add(0);
                dg.Add(dr.DataRef);
                dg.FillTo(413);
                if (client != null)
                    client.Send(dg.Get(), dg.Len);
                DataRefs.Remove(dr);

                OnLog?.Invoke($"Unsubscribed from {dr.DataRef}");
            }
        }

        /// <summary>
        /// Informs X-Plane to stop sending this DataRef
        /// </summary>
        /// <param name="dataref">DataRef to unsubscribe to</param>
        public void Unsubscribe(StringDataRefElement stringDataRef)
        {
            StringDataRefElement dr = StringDataRefs.FirstOrDefault(d => d.Id == stringDataRef.Id);
            if (dr != null)
            {
                for (var c = 0; c < dr.StringLength; c++)
                {
                    var currentIndex = c;
                    string dataRef = $"{dr.DataRef}[{c}]";
                    DataRefElement dataref = DataRefs.FirstOrDefault(d => d.Id == stringDataRef.Id && d.Index == currentIndex);
                    if (dataref != null)
                    {
                        var dg = new XPDatagram();
                        dg.Add("RREF");
                        dg.Add(dataref.DataGramId);
                        dg.Add(0);
                        dg.Add(dataref.DataRef);
                        dg.FillTo(413);
                        if (client != null)
                            client.Send(dg.Get(), dg.Len);
                        DataRefs.Remove(dataref);
                    }
                }
                StringDataRefs.Remove(dr);
                OnLog?.Invoke($"Unsubscribed from {stringDataRef.DataRef}");
            }
        }

        /// <summary>
        /// Informs X-Plane to change the value of the DataRef
        /// </summary>
        /// <param name="dataref">DataRef that will be changed</param>
        /// <param name="value">New value of the DataRef</param>
        public void SetDataRefValue(DataRefElement dataref, float value)
        {
            if (dataref == null)
                throw new ArgumentNullException(nameof(dataref));

            SetDataRefValue(dataref.DataRef, value);
        }

        /// <summary>
        /// Informs X-Plane to change the value of the DataRef
        /// </summary>
        /// <param name="dataref">DataRef that will be changed</param>
        /// <param name="value">New value of the DataRef</param>
        public void SetDataRefValue(string dataref, float value)
        {
            var dg = new XPDatagram();
            dg.Add("DREF");
            dg.Add(value);
            dg.Add(dataref);
            dg.FillTo(509);
            if (client != null)
                client.Send(dg.Get(), dg.Len);
        }
        /// <summary>
        /// Informs X-Plane to change the value of the DataRef
        /// </summary>
        /// <param name="dataref">DataRef that will be changed</param>
        /// <param name="value">New value of the DataRef</param>
        public void SetDataRefValue(string dataref, string value)
        {
            var dg = new XPDatagram();
            dg.Add("DREF");
            dg.Add(value);
            dg.Add(dataref);
            dg.FillTo(509);

            client.Send(dg.Get(), dg.Len);
        }

        /// <summary>
        /// Request X-Plane to close, a notification message will appear
        /// </summary>
        public void QuitXPlane()
        {
            var dg = new XPDatagram();
            dg.Add("QUIT");
            if (client != null)
                client.Send(dg.Get(), dg.Len);
        }

        /// <summary>
        /// Inform X-Plane that a system is failed
        /// </summary>
        /// <param name="system">Integer value representing the system to fail</param>
        public void Fail(int system)
        {
            var dg = new XPDatagram();
            dg.Add("FAIL");

            dg.Add(system.ToString(EnCulture));
            if (client != null)
                client.Send(dg.Get(), dg.Len);
        }

        /// <summary>
        /// Inform X-Plane that a system is back to normal functioning
        /// </summary>
        /// <param name="system">Integer value representing the system to recover</param>
        public void Recover(int system)
        {
            var dg = new XPDatagram();
            dg.Add("RECO");

            dg.Add(system.ToString(EnCulture));
            if (client != null)
                client.Send(dg.Get(), dg.Len);
        }

        protected virtual void Dispose(bool a)
        {
            server?.Dispose();
            client?.Dispose();
            ts?.Dispose();
            serverTask?.Dispose();
            observerTask?.Dispose();
            client = null;
            server = null;
            ts = null;
            serverTask = null;
            observerTask = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
