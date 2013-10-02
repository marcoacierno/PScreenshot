using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Picu3
{
    public class Notify
    {
        private NotifyIcon icon;
        private int durata;
        private EventHandler clickHandle;

        /// <summary>
        /// Costruttore, imposta come durata default 1sec.
        /// </summary>
        /// <param name="icon">Icona a cui la classe deve fare riferimento</param>
        public Notify(NotifyIcon icon) { this.icon = icon; this.icon.BalloonTipClicked += icon_BalloonTipClicked; this.durata = 1000; }

        /// <summary>
        /// Viene richiamato quando clicca sul ballon; viene gestito dall'utente che passa l'eventhandler ad ogni chiamata di SendMessage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void icon_BalloonTipClicked(object sender, EventArgs e)
        {
            clickHandle(sender, e);
        }

        public void SendMessage(string title, string text, ToolTipIcon icona, int durata, EventHandler method)
        {
            icon.BalloonTipText = text;
            icon.BalloonTipTitle = title;
            icon.BalloonTipIcon = icona;
            this.durata = durata;
            clickHandle = method;

            icon.ShowBalloonTip(durata);
        }

        public void SendMessage(string title, string text, ToolTipIcon icona, EventHandler method)
        {
            icon.BalloonTipText = text;
            icon.BalloonTipTitle = title;
            icon.BalloonTipIcon = icona;
            //this.durata = 1000;
            clickHandle = method;

            icon.ShowBalloonTip(1000);
        }


        public void ReShowLast()
        {
            icon.ShowBalloonTip(durata);
        }

        public void ReShowLast(int durata)
        {
            icon.ShowBalloonTip(durata);
        }
    }
}
