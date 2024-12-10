namespace DCS_BIOS.EventArgs
{
    public class DCSBIOSStringDataEventArgs : System.EventArgs                 
    {
        public ushort Address { get; init; }

        public string StringData { get; init; }
    }
}
