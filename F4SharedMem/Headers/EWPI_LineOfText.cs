﻿using System;
using System.Runtime.InteropServices;

namespace F4SharedMem.Headers
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EWPI_LineOfText
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public byte[] chars;
    }
}
