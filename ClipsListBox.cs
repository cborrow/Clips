using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Clips
{
    public class ClipsListBox : ListBox
    {
        Font addtionalInfoFont;
        public Font AddtionalInfoFont
        {
            get { return addtionalInfoFont; }
            set { addtionalInfoFont = value; }
        }

        public ClipsListBox()
        {
            this.ItemHeight = 32;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= this.Items.Count)
                return;

            //e.DrawBackground();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                LinearGradientBrush brush = new LinearGradientBrush(new Point(e.Bounds.X, e.Bounds.Y), new Point(e.Bounds.X, e.Bounds.Bottom),
                    Color.AliceBlue, Color.LightSkyBlue);

                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            e.DrawFocusRectangle();

            ClipboardItem ci = (ClipboardItem)this.Items[e.Index];

            /*e.Graphics.DrawString(ci.Name, this.Font, Brushes.Black, new PointF(2 + e.Bounds.X, 2 + e.Bounds.Y));
            e.Graphics.DrawString(ci.AddtionalInfo, this.AddtionalInfoFont, Brushes.Gray, new PointF(2 + e.Bounds.X, 15 + e.Bounds.Y));*/
            TextRenderer.DrawText(e.Graphics, ci.Name, this.Font, e.Bounds, Color.Black, Color.Transparent, 
                TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
            TextRenderer.DrawText(e.Graphics, ci.AddtionalInfo, this.AddtionalInfoFont, e.Bounds, Color.Gray, Color.Transparent,
                TextFormatFlags.Bottom | TextFormatFlags.Left);

            base.OnDrawItem(e);
        }
    }
}
