using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Picu3
{
    class Notify
    {
        private NotifyIcon icon;
        private ContextMenuStrip context;

        public Notify(string title)
        {
            icon = new NotifyIcon();
            icon.Text = title;
        }
    }
}
