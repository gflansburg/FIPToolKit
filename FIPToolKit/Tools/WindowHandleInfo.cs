using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Tools
{
    public class WindowHandle
    {
        public IntPtr hWnd { get; set; }
        public IntPtr hWndParent { get; set; }
        public List<WindowHandle> Children { get; set; }

        public WindowHandle()
        {
            Children = new List<WindowHandle>();
            hWnd = IntPtr.Zero;
            hWndParent = IntPtr.Zero;
        }
    }

    public class WindowHandleInfo
    {
        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);

        private IntPtr _MainHandle;

        public WindowHandleInfo(IntPtr handle)
        {
            this._MainHandle = handle;
        }

        public List<WindowHandle> GetAllChildHandles()
        {
            List<WindowHandle> childHandles = new List<WindowHandle>();

            GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
            IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(this._MainHandle, childProc, pointerChildHandlesList);
            }
            finally
            {
                gcChildhandlesList.Free();
            }

            return childHandles;
        }

        private WindowHandle FindParentWindow(List<WindowHandle> childHandles, IntPtr hWnd)
        {
            foreach(WindowHandle windowHandle in childHandles)
            {
                if(windowHandle.hWnd == hWnd)
                {
                    return windowHandle;
                }
                WindowHandle child = FindParentWindow(windowHandle.Children, hWnd);
                if(child != null)
                {
                    return child;
                }
            }
            return null;
        }

        private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<WindowHandle> childHandles = gcChildhandlesList.Target as List<WindowHandle>;

            IntPtr parent = NativeMethods.GetParent(hWnd);
            WindowHandle windowHandle = FindParentWindow(childHandles, parent);
            if (windowHandle == null)
            {
                childHandles.Add(new WindowHandle()
                {
                    hWnd = hWnd,
                    hWndParent = parent
                });
            }
            else
            {
                windowHandle.Children.Add(new WindowHandle()
                {
                    hWnd = hWnd,
                    hWndParent = parent
                });
            }
            return true;
        }
    }
}
