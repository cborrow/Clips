using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Clips
{
    public class ClipsApplicationContext : ApplicationContext
    {
        Container components;
        NotifyIcon notifyIcon;
        ContextMenuStrip contextMenuStrip;
        MainForm mainForm;

        public ClipsApplicationContext()
        {
            InitializeContext();
        }

        private void InitializeContext()
        {
            components = new Container();
            contextMenuStrip = new ContextMenuStrip();
            mainForm = new MainForm();

            notifyIcon = new NotifyIcon(components);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.Icon = Clips.Properties.Resources.TrayIcon;
            notifyIcon.Text = "Clips Clipboard Manager";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_MouseClick);

            contextMenuStrip.Items.Add(new ToolStripMenuItem("Properties", null, new EventHandler(Properties_Clicked)));
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem("Quit", null, new EventHandler(Quit_Clicked)));
        }

        void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle screenRect = Screen.PrimaryScreen.WorkingArea;
            mainForm.Location = new Point((screenRect.Width - mainForm.Width), (screenRect.Height - mainForm.Height));

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (mainForm.Visible)
                    mainForm.Hide();
                else
                    mainForm.Show();
            }
        }

        private void Properties_Clicked(object sender, EventArgs e)
        {

        }

        private void Quit_Clicked(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}
