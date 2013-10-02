using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Picu3
{
    public class Notify
    {
        /// <summary>
        /// Icona che la classe deve gestire, passata nel costruttore
        /// </summary>
        private NotifyIcon icon;
        /// <summary>
        /// Durata default (o ultima) del ballontip
        /// </summary>
        private int durata;
        /// <summary>
        /// Evento da chiamare se l'icona viene premuta
        /// Viene gestito quando viene richiamato SendMessage
        /// </summary>
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
        /// <summary>
        /// Si occupa di mostrare il ballontip
        /// </summary>
        /// <param name="title">Titolo da usare</param>
        /// <param name="text">Testo da usare</param>
        /// <param name="icona">Icona da usare</param>
        /// <param name="durata">La durata del ballontip</param>
        /// <param name="method">Il metodo da richiamare se l'utente clicca sul ballontip</param>
        public void SendMessage(string title, string text, ToolTipIcon icona, int durata, EventHandler method)
        {
            icon.BalloonTipText = text;
            icon.BalloonTipTitle = title;
            icon.BalloonTipIcon = icona;
            this.durata = durata;
            clickHandle = method;

            icon.ShowBalloonTip(durata);
        }
        /// <summary>
        /// Si occupa di mostrare il ballontip
        /// </summary>
        /// <param name="title">Titolo da usare</param>
        /// <param name="text">Testo da usare</param>
        /// <param name="icona">Icona da usare</param>
        /// <param name="method">Il metodo da richiamare se l'utente clicca sul ballontip</param>
        public void SendMessage(string title, string text, ToolTipIcon icona, EventHandler method)
        {
            icon.BalloonTipText = text;
            icon.BalloonTipTitle = title;
            icon.BalloonTipIcon = icona;
            //this.durata = 1000;
            clickHandle = method;

            icon.ShowBalloonTip(1000);
        }

        /// <summary>
        /// Esegue solamente ShowBalloonTip senza modificare ne titolo ne testo
        /// </summary>
        public void ReShowLast()
        {
            icon.ShowBalloonTip(durata);
        }
        /// <summary>
        /// Esegue solamente ShowBalloonTip senza modificare ne titolo ne testo specifica anche la durata.
        /// </summary>
        /// <param name="durata">La durata del ballontip</param>
        public void ReShowLast(int durata)
        {
            this.durata = durata;
            icon.ShowBalloonTip(durata);
        }
    }
}
