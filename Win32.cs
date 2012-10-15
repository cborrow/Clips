using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Clips
{
    public class Win32
    {
        public static int WM_CLIPBOARDUPDATE = 0x031D;
        public static int WM_CHANGECBCHAIN = 0x030D;
        public static int WM_DRAWCLIPBOARD = 0x0308;

        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardViewer(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool ChangeClipboardChain(IntPtr hwndRemove, IntPtr hwndNext);

        //The following methods only work in Vista or greater.
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
    }
}
