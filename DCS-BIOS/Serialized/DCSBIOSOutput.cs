using System.Linq;
using DCS_BIOS.Json;
using DCS_BIOS.StringClasses;


namespace DCS_BIOS.Serialized
{
    using System;
    using System.ComponentModel;
    using DCS_BIOS;
    using ControlLocator;
    using Newtonsoft.Json;

    public enum DCSBiosOutputType
    {
        StringType,
        IntegerType,
        LED,
        ServoOutput,
        FloatBuffer,
        None
    }

    public enum DCSBiosOutputComparison
    {
        [Description("Equals")]
        Equals,
        [Description("Less than")]
        LessThan,
        [Description("Bigger than")]
        BiggerThan,
        [Description("Not equals")]
        NotEquals
    }

    /// <summary>
    /// This class represents the output sent from DCS-BIOS.
    /// When a DCS-BIOS Control value has been sent each class
    /// listening for specific DCS-BIOS value(s) (Address & Data)
    /// can check via the Address part whether it was a match and if
    /// it was then extract the Data part. Data is bit shifted so it can't be
    /// read directly. This class holds the information on how much to shift
    /// and with what.
    /// </summary>
    [Serializable]
    [SerializeCritical]
    public class DCSBIOSOutput
    {
        // The target value used for comparison as chosen by the user
        private ushort _specifiedValueUShort;

        private ushort _address;
        private volatile ushort _lastUShortValue = ushort.MaxValue;
        private volatile float _lastFloatValue = float.MaxValue;
        private volatile string _lastStringValue = "";
        private bool _ushortValueHasChanged;
        private bool _floatValueHasChanged;
        private string _defaultVariableChangeValue = "3200";

        [NonSerialized] private object _lockObject = new();

        public static DCSBIOSOutput CreateCopy(DCSBIOSOutput dcsbiosOutput)
        {
            var tmp = new DCSBIOSOutput
            {
                DCSBiosOutputType = dcsbiosOutput.DCSBiosOutputType,
                ControlId = dcsbiosOutput.ControlId,
                Address = dcsbiosOutput.Address,
                ControlDescription = dcsbiosOutput.ControlDescription,
                DCSBiosOutputComparison = dcsbiosOutput.DCSBiosOutputComparison,
                Mask = dcsbiosOutput.Mask,
                MaxLength = dcsbiosOutput.MaxLength,
                MaxValue = dcsbiosOutput.MaxValue,
                ShiftValue = dcsbiosOutput.ShiftValue
            };

            switch (tmp.DCSBiosOutputType)
            {
                case DCSBiosOutputType.IntegerType:
                    tmp.SpecifiedValueUShort = dcsbiosOutput.SpecifiedValueUShort;
                    break;
            }

            return tmp;
        }

        public void Copy(DCSBIOSOutput dcsbiosOutput)
        {
            DCSBiosOutputType = dcsbiosOutput.DCSBiosOutputType;
            ControlId = dcsbiosOutput.ControlId;
            Address = dcsbiosOutput.Address;
            ControlDescription = dcsbiosOutput.ControlDescription;
            DCSBiosOutputComparison = dcsbiosOutput.DCSBiosOutputComparison;
            Mask = dcsbiosOutput.Mask;
            MaxLength = dcsbiosOutput.MaxLength;
            MaxValue = dcsbiosOutput.MaxValue;
            ShiftValue = dcsbiosOutput.ShiftValue;
            AddressIdentifier = dcsbiosOutput.AddressIdentifier;
            AddressMaskIdentifier = dcsbiosOutput.AddressMaskIdentifier;
            AddressMaskShiftIdentifier = dcsbiosOutput.AddressMaskShiftIdentifier;

            if (DCSBiosOutputType == DCSBiosOutputType.IntegerType)
            {
                SpecifiedValueUShort = dcsbiosOutput.SpecifiedValueUShort;
            }
        }
        public void Consume(DCSBIOSControl dcsbiosControl, DCSBiosOutputType dcsBiosOutputType)
        {
            ControlId = dcsbiosControl.Identifier;
            ControlDescription = dcsbiosControl.Description;
            try
            {
                if (!dcsbiosControl.HasOutput())
                {
                    DCSBiosOutputType = DCSBiosOutputType.None;
                    return;
                }

                foreach (var dcsbiosControlInput in dcsbiosControl.Inputs.Where(dcsbiosControlInput => dcsbiosControlInput.SuggestedStep is >= 0))
                {
                    _defaultVariableChangeValue = dcsbiosControlInput.SuggestedStep.ToString();
                }

                foreach (var dcsbiosControlOutput in dcsbiosControl.Outputs)
                {
                    if (dcsbiosControlOutput.OutputDataType == dcsBiosOutputType)
                    {
                        DCSBiosOutputType = dcsbiosControlOutput.OutputDataType;
                        _address = dcsbiosControlOutput.Address;
                        Mask = dcsbiosControlOutput.Mask;
                        MaxValue = dcsbiosControlOutput.MaxValue;
                        MaxLength = dcsbiosControlOutput.MaxLength;
                        ShiftValue = dcsbiosControlOutput.ShiftBy;

                        AddressIdentifier = dcsbiosControlOutput.AddressIdentifier;
                        AddressMaskIdentifier = dcsbiosControlOutput.AddressMaskIdentifier;
                        AddressMaskShiftIdentifier = dcsbiosControlOutput.AddressMaskShiftIdentifier;

                        if (dcsBiosOutputType == DCSBiosOutputType.StringType)
                        {
                            DCSBIOSStringManager.AddListeningAddress(this);
                        }

                        DCSBIOSProtocolParser.RegisterAddressToBroadCast(_address);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception($"Failed to copy control {ControlId}. Control output is missing.{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Checks :
        /// <para>* if there is a there is a change in the value since last comparison</para>
        /// <para>* test is true using chosen comparison operator with new value and reference value</para>
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UShortConditionIsMet(ushort address, ushort data)
        {
            _lockObject ??= new object();
            var result = false;

            lock (_lockObject)
            {
                if (Address != address)
                {
                    return false;
                }

                ushort newValue = (ushort)((data & Mask) >> ShiftValue);

                var resultComparison = DCSBiosOutputComparison switch
                {
                    DCSBiosOutputComparison.BiggerThan => newValue > _specifiedValueUShort,
                    DCSBiosOutputComparison.LessThan => newValue < _specifiedValueUShort,
                    DCSBiosOutputComparison.NotEquals => newValue != _specifiedValueUShort,
                    DCSBiosOutputComparison.Equals => newValue == _specifiedValueUShort,
                    _ => throw new Exception("Unexpected DCSBiosOutputComparison value")
                };

                result = resultComparison && !newValue.Equals(LastUShortValue);
                //Debug.WriteLine($"(EvaluateUShort) Result={result} Target={_specifiedValueUShort} Last={LastUShortValue} New={newValue}");
                LastUShortValue = newValue;
            }

            return result;
        }

        /// <summary>
        /// Checks :
        /// <para>for address match</para>
        /// <para>that new value differs from previous</para>
        /// <para>stores new value</para>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns>Returns true when all checks are true.</returns>
        public bool UShortValueHasChanged(ushort address, ushort data)
        {
            _lockObject ??= new object();

            lock (_lockObject)
            {
                if (address != Address)
                {
                    // Not correct control
                    return false;
                }

                if (GetUShortValue(data) == LastUShortValue && !_ushortValueHasChanged)
                {
                    // Value hasn't changed
                    return false;
                }

                _ushortValueHasChanged = false;
                LastUShortValue = GetUShortValue(data);
            }

            return true;
        }

        /// <summary>
        /// Checks :
        /// <para>for address match</para>
        /// <para>that new value differs from previous</para>
        /// <para>stores new value</para>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns>Returns true when all checks are true.</returns>
        public bool FloatValueHasChanged(ushort address, ushort data)
        {
            _lockObject ??= new object();

            lock (_lockObject)
            {
                if (address != Address)
                {
                    // Not correct control
                    return false;
                }

                if (GetFloatValue(data) == LastFloatValue && !_floatValueHasChanged)
                {
                    // Value hasn't changed
                    return false;
                }

                _floatValueHasChanged = false;
                LastFloatValue = GetFloatValue(data);
            }

            return true;
        }

        /// <summary>
        /// Checks :
        /// <para>for address match</para>
        /// <para>that new string value differs from previous</para>
        /// <para>stores new value</para>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="stringData"></param>
        /// <returns>Returns true when all checks are true.</returns>
        public bool StringValueHasChanged(ushort address, string stringData)
        {
            _lockObject ??= new object();

            lock (_lockObject)
            {
                if (address != Address)
                {
                    // Not correct control
                    return false;
                }

                if ((_lastStringValue ?? string.Empty) != (stringData ?? string.Empty))
                {
                    _lastStringValue = stringData;
                    return true;
                }
            }

            return false;
        }

        public ushort GetUShortValue(ushort data)
        {
            /*
             * Fugly workaround, side effect of using deep clone DCSBIOSDecoder is that this is null
             */
            _lockObject ??= new object();

            lock (_lockObject)
            {
                LastUShortValue = (ushort)((data & Mask) >> ShiftValue);
                return LastUShortValue;
            }
        }

        public float GetFloatValue(ushort data)
        {
            /*
             * Fugly workaround, side effect of using deep clone DCSBIOSDecoder is that this is null
             */
            _lockObject ??= new object();

            lock (_lockObject)
            {
                ushort value = (ushort)((data & Mask) >> ShiftValue);
                byte[] bytes = BitConverter.GetBytes(value);
                LastFloatValue = bytes[1] * 256 + bytes[0];
                return LastFloatValue;
            }
        }

        public override string ToString()
        {
            if (DCSBiosOutputType == DCSBiosOutputType.StringType)
            {
                return "";
            }

            return "DCSBiosOutput{" + ControlId + "|" + DCSBiosOutputComparison + "|" + _specifiedValueUShort + "}";
        }

        public void ImportString(string str)
        {
            // DCSBiosOutput{AAP_EGIPWR|Equals|0}
            var value = str;
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("DCSBiosOutput cannot import null string.");
            }

            if (!str.StartsWith("DCSBiosOutput{") || !str.EndsWith("}"))
            {
                throw new Exception($"DCSBiosOutput cannot import string : {str}");
            }

            value = value.Replace("DCSBiosOutput{", string.Empty).Replace("}", string.Empty);

            // AAP_EGIPWR|Equals|0
            var entries = value.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            ControlId = entries[0];
            var dcsBIOSControl = DCSBIOSControlLocator.GetControl(ControlId);
            Consume(dcsBIOSControl, DCSBiosOutputType.IntegerType);
            DCSBiosOutputComparison = (DCSBiosOutputComparison)Enum.Parse(typeof(DCSBiosOutputComparison), entries[1]);
            _specifiedValueUShort = (ushort)int.Parse(entries[2]);
        }

        [JsonProperty("ControlId", Required = Required.Default)]
        public string ControlId { get; set; }

        [JsonProperty("Address", Required = Required.Default)]
        public ushort Address
        {
            get => _address;
            set
            {
                _address = value;
                DCSBIOSProtocolParser.RegisterAddressToBroadCast(_address);
            }
        }

        [JsonProperty("Mask", Required = Required.Default)]
        public ushort Mask { get; set; }

        [JsonProperty("Shiftvalue", Required = Required.Default)]
        public int ShiftValue { get; set; }

        [JsonProperty("DCSBiosOutputType", Required = Required.Default)]
        public DCSBiosOutputType DCSBiosOutputType { get; set; } = DCSBiosOutputType.None;

        [JsonProperty("DCSBiosOutputComparison", Required = Required.Default)]
        public DCSBiosOutputComparison DCSBiosOutputComparison { get; set; } = DCSBiosOutputComparison.Equals;

        [JsonIgnore]
        public ushort SpecifiedValueUShort
        {
            get => _specifiedValueUShort;
            set
            {
                if (DCSBiosOutputType != DCSBiosOutputType.IntegerType)
                {
                    throw new Exception($"Invalid DCSBiosOutput. Specified value (trigger value) set to [int] but DCSBiosOutputType set to {DCSBiosOutputType}");
                }

                _specifiedValueUShort = value;
            }
        }

        [JsonProperty("ControlDescription", Required = Required.Default)]
        public string ControlDescription { get; set; }

        [JsonProperty("MaxValue", Required = Required.Default)]
        public ushort MaxValue { get; set; }

        [JsonIgnore]
        public string AddressIdentifier { get; set; }

        [JsonIgnore]
        public string AddressMaskIdentifier { get; set; }

        [JsonIgnore]
        public string AddressMaskShiftIdentifier { get; set; }

        [JsonProperty("MaxLength", Required = Required.Default)]
        public ushort MaxLength { get; set; }

        [Obsolete]
        [JsonIgnore]
        public string ControlType { get; set; }
        
        [JsonIgnore]
        public ushort LastUShortValue
        {
            get => _lastUShortValue;
            set
            {
                if (value != _lastUShortValue)
                {
                    _ushortValueHasChanged = true;
                }
                _lastUShortValue = value;
            }
        }

        [JsonIgnore]
        public float LastFloatValue
        {
            get => _lastFloatValue;
            set
            {
                if (value != _lastFloatValue)
                {
                    _floatValueHasChanged = true;
                }
                _lastFloatValue = value;
            }
        }

        [JsonIgnore]
        public string LastStringValue
        {
            get => _lastStringValue;
            set => _lastStringValue = value;
        }

        public static DCSBIOSOutput GetUpdateCounter()
        {
            var counter = DCSBIOSControlLocator.GetUShortDCSBIOSOutput("_UPDATE_COUNTER");
            return counter;
        }

        public string GetOutputType()
        {
            return DCSBiosOutputType switch
            {
                DCSBiosOutputType.IntegerType => "integer",
                DCSBiosOutputType.StringType => "string",
                DCSBiosOutputType.None => "none",
                DCSBiosOutputType.FloatBuffer => "float",
                DCSBiosOutputType.LED => "led",
                DCSBiosOutputType.ServoOutput => "servo output",
                _ => throw new Exception($"GetOutputType() : Failed to identify {DCSBiosOutputType} output type.")
            };
        }
    }
}
