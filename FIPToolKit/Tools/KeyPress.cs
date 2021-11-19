using FIPToolKit.Models;
using FSUIPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIPToolKit.Tools
{
    public class KeyPress
    {
        static public bool Stop { get; set; }

        private const int SLEEP_VALUE = 32;

        static public void SendKeys(KeyPressLengths breakLength, VirtualKeyCode[] virtualKeyCodes, KeyPressLengths keyPressLength)
        {
            var keyPressLengthTimeConsumed = 0;
            var breakLengthConsumed = 0;

            Stop = false;
            while (breakLengthConsumed < (int)breakLength)
            {
                Thread.Sleep(SLEEP_VALUE);
                breakLengthConsumed += SLEEP_VALUE;
                if (Stop)
                {
                    return;
                }
            }
            var inputs = new NativeMethods.INPUT[virtualKeyCodes.Count()];
            while (keyPressLengthTimeConsumed < (int)keyPressLength && !Stop)
            {
                var modifierCount = 0;
                foreach (VirtualKeyCode virtualKeyCode in virtualKeyCodes)
                {
                    if (KeyModifiers.IsModifierKey(virtualKeyCode))
                    {
                        modifierCount++;
                    }
                }
                //Add modifiers
                for (var i = 0; i < virtualKeyCodes.Count(); i++)
                {
                    var virtualKeyCode = virtualKeyCodes[i];
                    if (KeyModifiers.IsModifierKey(virtualKeyCode))
                    {
                        inputs[i].type = NativeMethods.INPUT_KEYBOARD;
                        inputs[i].InputUnion.ki.time = 0;
                        inputs[i].InputUnion.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE;
                        if (KeyModifiers.IsExtendedKey(virtualKeyCode))
                        {
                            inputs[i].InputUnion.ki.dwFlags |= NativeMethods.KEYEVENTF_EXTENDEDKEY;
                        }
                        inputs[i].InputUnion.ki.wVk = 0;
                        inputs[i].InputUnion.ki.wScan = (ushort)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0);
                        inputs[i].InputUnion.ki.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                    }
                }
                //[x][x] [] []
                // 0  1  2  3
                // 1  2  3  4
                //Add normal keys
                for (var i = modifierCount; i < virtualKeyCodes.Count(); i++)
                {
                    var virtualKeyCode = virtualKeyCodes[i];
                    if (!KeyModifiers.IsModifierKey(virtualKeyCode) && virtualKeyCode != VirtualKeyCode.VK_NULL)
                    {
                        inputs[i].type = NativeMethods.INPUT_KEYBOARD;
                        inputs[i].InputUnion.ki.time = 0;
                        inputs[i].InputUnion.ki.dwFlags = NativeMethods.KEYEVENTF_SCANCODE;

                        inputs[i].InputUnion.ki.wVk = 0;
                        inputs[i].InputUnion.ki.wScan = (ushort)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0);
                        inputs[i].InputUnion.ki.dwExtraInfo = NativeMethods.GetMessageExtraInfo();
                    }
                }
                NativeMethods.SendInput((uint)inputs.Count(), inputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));

                if (keyPressLength != KeyPressLengths.Indefinite)
                {
                    Thread.Sleep(SLEEP_VALUE);
                    keyPressLengthTimeConsumed += SLEEP_VALUE;
                }
                else
                {
                    Thread.Sleep(20);
                }
            }
            for (var i = 0; i < inputs.Count(); i++)
            {
                inputs[i].InputUnion.ki.dwFlags |= NativeMethods.KEYEVENTF_KEYUP;
            }
            Array.Reverse(inputs);
            //Release same keys
            NativeMethods.SendInput((uint)inputs.Count(), inputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }

        public static void SendKeyToFS(KeyPressLengths breakLength, VirtualKeyCode[] virtualKeyCodes)
        {
            var breakLengthConsumed = 0;

            Stop = false;
            while (breakLengthConsumed < (int)breakLength)
            {
                Thread.Sleep(SLEEP_VALUE);
                breakLengthConsumed += SLEEP_VALUE;
                if (Stop)
                {
                    return;
                }
            }
            SendModifierKeys modifierKeys = 0;
            //Press modifiers
            for (var i = 0; i < virtualKeyCodes.Count(); i++)
            {
                var virtualKeyCode = virtualKeyCodes[i];
                if (KeyModifiers.IsModifierKey(virtualKeyCode))
                {
                    if (virtualKeyCode == VirtualKeyCode.CONTROL || virtualKeyCode == VirtualKeyCode.LCONTROL || virtualKeyCode == VirtualKeyCode.RCONTROL)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Control;
                    }
                    else if (virtualKeyCode == VirtualKeyCode.LMENU || virtualKeyCode == VirtualKeyCode.RMENU)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Alt;
                    }
                    else if (virtualKeyCode == VirtualKeyCode.MENU)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Menu;
                    }
                    else if (virtualKeyCode == VirtualKeyCode.SHIFT || virtualKeyCode == VirtualKeyCode.LSHIFT || virtualKeyCode == VirtualKeyCode.RSHIFT)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Shift;
                    }
                    else if (virtualKeyCode == VirtualKeyCode.RWIN || virtualKeyCode == VirtualKeyCode.LWIN)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Windows;
                    }
                    else if (virtualKeyCode == VirtualKeyCode.TAB)
                    {
                        modifierKeys = modifierKeys | SendModifierKeys.Tab;
                    }
                }
            }
            //Press normal keys
            for (var i = 0; i < virtualKeyCodes.Count(); i++)
            {
                var virtualKeyCode = virtualKeyCodes[i];
                if (!KeyModifiers.IsModifierKey(virtualKeyCode) && virtualKeyCode != VirtualKeyCode.VK_NULL)
                {
                    FSUIPCConnection.SendKeyToFS((System.Windows.Forms.Keys)virtualKeyCode, modifierKeys);
                }
            }
        }

        public static void KeyBdEvent(KeyPressLengths breakLength, VirtualKeyCode[] virtualKeyCodes, KeyPressLengths keyPressLength)
        {
            var keyPressLengthTimeConsumed = 0;
            var breakLengthConsumed = 0;

            /*
                //keybd_event
                http://msdn.microsoft.com/en-us/library/windows/desktop/ms646304%28v=vs.85%29.aspx
            */
            Stop = false;
            while (breakLengthConsumed < (int)breakLength)
            {
                Thread.Sleep(SLEEP_VALUE);
                breakLengthConsumed += SLEEP_VALUE;
                if (Stop)
                {
                    return;
                }
            }
            while (keyPressLengthTimeConsumed < (int)keyPressLength && !Stop)
            {
                //Debug.WriteLine("VK = " + virtualKeyCodes[1] + " length = " + keyPressLength);
                //Press modifiers
                for (var i = 0; i < virtualKeyCodes.Count(); i++)
                {
                    var virtualKeyCode = virtualKeyCodes[i];
                    if (KeyModifiers.IsModifierKey(virtualKeyCode))
                    {
                        if (KeyModifiers.IsExtendedKey(virtualKeyCode))
                        {
                            NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), (int)NativeMethods.KEYEVENTF_EXTENDEDKEY | 0, 0);
                            //keybd_event(VK_LCONTROL, 0, KEYEVENTF_EXTENDEDKEY, 0);
                        }
                        else
                        {
                            NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), 0, 0);
                        }
                    }
                }
                //Press normal keys
                for (var i = 0; i < virtualKeyCodes.Count(); i++)
                {
                    var virtualKeyCode = virtualKeyCodes[i];
                    if (!KeyModifiers.IsModifierKey(virtualKeyCode) && virtualKeyCode != VirtualKeyCode.VK_NULL)
                    {
                        NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), 0, 0);
                    }
                }
                if (keyPressLength != KeyPressLengths.Indefinite)
                {
                    Thread.Sleep(SLEEP_VALUE);
                    keyPressLengthTimeConsumed += SLEEP_VALUE;
                }
                else
                {
                    Thread.Sleep(20);
                }
                if (keyPressLength != KeyPressLengths.Indefinite)
                {
                    ReleaseKeys(virtualKeyCodes);
                }
            }
            if (keyPressLength == KeyPressLengths.Indefinite)
            {
                ReleaseKeys(virtualKeyCodes);
            }
        }

        static private void ReleaseKeys(VirtualKeyCode[] virtualKeyCodes)
        {
            //Release normal keys
            foreach (VirtualKeyCode virtualKeyCode in virtualKeyCodes)
            {
                if (!KeyModifiers.IsModifierKey(virtualKeyCode))
                {
                    NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), (int)NativeMethods.KEYEVENTF_KEYUP, 0);
                }
            }
            //Release modifiers
            foreach (VirtualKeyCode virtualKeyCode in virtualKeyCodes)
            {
                if (KeyModifiers.IsModifierKey(virtualKeyCode))
                {
                    if (KeyModifiers.IsExtendedKey(virtualKeyCode))
                    {
                        NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), (int)(NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP), 0);
                    }
                    else
                    {
                        NativeMethods.keybd_event((byte)virtualKeyCode, (byte)NativeMethods.MapVirtualKey((uint)virtualKeyCode, 0), (int)NativeMethods.KEYEVENTF_KEYUP, 0);
                    }
                }
            }
        }
    }
}
