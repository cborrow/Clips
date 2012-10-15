using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clips
{
    public partial class MainForm : Form
    {
        ClipboardListener clipboardListener;

        public MainForm()
        {
            InitializeComponent();

            clipboardListener = new ClipboardListener();
            clipboardListener.ClipboardDataChanged += new ClipboardListener.ClipboardDataChangedEventHandler(clipboardListener_ClipboardDataChanged);
            clipboardListener.Attach(this.Handle);

            contextMenuStrip1.Opening += new CancelEventHandler(contextMenuStrip1_Opening);
        }

        void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ClipboardItem item = GetSelectedItem();

            if (item == null)
            {
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[1].Enabled = false;
            }
            else
            {
                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = true;
            }
        }

        public ClipboardItem GetSelectedItem()
        {
            ClipsListBox.SelectedObjectCollection selectedItems = clipsListBox1.SelectedItems;

            if (selectedItems.Count > 0)
                return (ClipboardItem)selectedItems[0];
            return null;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            clipboardListener.Remove(this.Handle);

            base.OnClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            clipboardListener.MsgProc(ref m);

            base.WndProc(ref m);
        }

        private void clipboardListener_ClipboardDataChanged(ClipboardItem item)
        {
            clipsListBox1.Items.Add(item);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardItem item = GetSelectedItem();
            Clipboard.SetData(item.Format, item.Data);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(clipsListBox1.SelectedIndex >= 0 && clipsListBox1.SelectedIndex < clipsListBox1.Items.Count)
                clipsListBox1.Items.RemoveAt(clipsListBox1.SelectedIndex);
        }

        private void clipsListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                if(clipsListBox1.SelectedItems.Count > 0)
                    contextMenuStrip1.Show(e.X, e.Y);
            }
        }
    }
}
