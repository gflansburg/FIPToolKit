namespace DCS_BIOS.EventArgs
{
    public class DCSBIOSDataEventArgs : System.EventArgs
    {
        public ushort Address { get; init; }

        public ushort Data { get; init; }
    }
}
