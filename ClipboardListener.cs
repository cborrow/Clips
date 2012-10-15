using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clips
{
    public class ClipboardListener
    {
        public delegate void ClipboardDataChangedEventHandler(ClipboardItem item);
        public event ClipboardDataChangedEventHandler ClipboardDataChanged;

        IntPtr hwndNext;
        IntPtr hwnd;

        public ClipboardListener()
        {
            ClipboardDataChanged = new ClipboardDataChangedEventHandler(OnClipboardDataChanged);
        }

        public bool Attach(IntPtr hwnd)
        {
            if (Environment.OSVersion.Version.Major < 6)
                return AttachRetroListener(hwnd);
            else
                return AttachListener(hwnd);
        }

        public bool Remove(IntPtr hwnd)
        {
            if (hwnd == null)
                return false;

            if (Environment.OSVersion.Version.Major < 6)
                return RemoveRetroListener(hwnd);
            else
                return RemoveListener(hwnd);
        }

        public void MsgProc(ref Message msg)
        {
            if (msg.Msg == Win32.WM_CHANGECBCHAIN)
            {
                hwndNext = (IntPtr)msg.LParam;
            }
            else if (msg.Msg == Win32.WM_DRAWCLIPBOARD)
            {
                ClipboardUpdated();
            }
            else if (msg.Msg == Win32.WM_CLIPBOARDUPDATE)
            {
                ClipboardUpdated();
            }
        }

        protected bool AttachRetroListener(IntPtr hwnd)
        {
            hwndNext = Win32.SetClipboardViewer(hwnd);
            return true;
        }

        protected bool AttachListener(IntPtr hwnd)
        {
            return Win32.AddClipboardFormatListener(hwnd);
        }

        protected bool RemoveRetroListener(IntPtr hwnd)
        {
            return Win32.ChangeClipboardChain(hwnd, hwndNext);
        }

        protected bool RemoveListener(IntPtr hwnd)
        {
            return Win32.RemoveClipboardFormatListener(hwnd);
        }

        protected void ClipboardUpdated()
        {
            ClipboardItem ci = new ClipboardItem();

            if (Clipboard.ContainsText())
            {
                ci.DataType = ClipboardDataType.Text;
                ci.Name = Clipboard.GetText();
                ci.AddtionalInfo = "System.String";
                ci.Format = Clipboard.GetDataObject().GetFormats()[0];
                ci.Data = Clipboard.GetText();
            }
            else if (Clipboard.ContainsImage())
            {
                ci.DataType = ClipboardDataType.Image;
                ci.AddtionalInfo = "System.Drawing.Image";
                ci.Data = Clipboard.GetImage();
                ci.Format = Clipboard.GetDataObject().GetFormats()[0];
                ci.Name = string.Format("Image ({0} bytes)", System.Runtime.InteropServices.Marshal.SizeOf(ci.Data));
            }
            else if (Clipboard.ContainsFileDropList())
            {
                ci.DataType = ClipboardDataType.Files;
                ci.AddtionalInfo = "File(s) List";
                ci.Format = Clipboard.GetDataObject().GetFormats()[0];
                ci.Data = Clipboard.GetFileDropList();
            }
            else
                return;

            ClipboardDataChanged(ci);
        }

        protected virtual void OnClipboardDataChanged(ClipboardItem item)
        {

        }
    }
}
