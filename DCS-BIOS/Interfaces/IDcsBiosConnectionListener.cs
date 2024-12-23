﻿namespace DCS_BIOS.Interfaces
{
    using EventArgs;

    public interface IDcsBiosConnectionListener
    {
        void DcsBiosConnectionActive(object sender, DCSBIOSConnectionEventArgs e);
        void DcsBiosConnectionInActive(object sender, DCSBIOSConnectionEventArgs e);
    }
}
