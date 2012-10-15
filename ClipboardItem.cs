using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clips
{
    public class ClipboardItem
    {
        public ClipboardDataType DataType;
        public string Name;
        public string AddtionalInfo;
        public string Format;
        public object Data;

        public ClipboardItem()
        {
            this.Name = string.Empty;
            this.AddtionalInfo = string.Empty;
            this.Data = null;
        }

        public ClipboardItem(string name, string format, object data) : this(name, null, format, data) { }

        public ClipboardItem(string name, string addtionalInfo, string format, object data)
            : this()
        {
            this.Name = name;
            this.AddtionalInfo = addtionalInfo;
            this.Format = format;
            this.Data = data;
        }
    }
}
