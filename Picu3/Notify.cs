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
        /// Evento da chiamare se l'icona viene premuta
        /// Viene gestito quando viene richiamato SendMessage
        /// </summary>
        private EventHandler clickHandle;
        /// <summary>
        /// Se true, specifica che l'handle del click deve essere pulito dopo che viene chiamato
        /// </summary>
        private bool clearHandleAfterCall;
        /// <summary>
        /// Costruttore, imposta come durata default 1sec.
        /// </summary>
        /// <param name="icon">Icona a cui la classe deve fare riferimento</param>
        public Notify(NotifyIcon icon) { this.icon = icon; this.icon.BalloonTipClicked += icon_BalloonTipClicked; }

        /// <summary>
        /// Viene richiamato quando clicca sul ballon; viene gestito dall'utente che passa l'eventhandler ad ogni chiamata di SendMessage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void icon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (clickHandle != null)
            {
                clickHandle(sender, e);

                if (clearHandleAfterCall) { clickHandle = null; clearHandleAfterCall = true; }
            }
        }
        /// <summary>
        /// Si occupa di mostrare il ballontip
        /// </summary>
        /// <param name="title">Titolo da usare</param>
        /// <param name="text">Testo da usare</param>
        /// <param name="icona">Icona da usare</param>
        /// <param name="method">Il metodo da richiamare se l'utente clicca sul ballontip</param>
        public void SendMessage(string title, string text, ToolTipIcon icona, EventHandler method, bool clearHandleAfterCall = true)
        {
            icon.BalloonTipText = text;
            icon.BalloonTipTitle = title;
            icon.BalloonTipIcon = icona;
            clickHandle = method;
            this.clearHandleAfterCall = clearHandleAfterCall;

            icon.ShowBalloonTip(1000);
        }

        /// <summary>
        /// Esegue solamente ShowBalloonTip senza modificare ne titolo ne testo
        /// </summary>
        //public void ReShowLast()
        //{
        //    icon.ShowBalloonTip(durata);
        //}
        ///// <summary>
        ///// Esegue solamente ShowBalloonTip senza modificare ne titolo ne testo specifica anche la durata.
        ///// </summary>
        ///// <param name="durata">La durata del ballontip</param>
        //public void ReShowLast(int durata)
        //{
        //    this.durata = durata;
        //    icon.ShowBalloonTip(durata);
        //}
        /// <summary>
        /// Questo metodo si occupa di modificare il testo dell'icona
        /// </summary>
        /// <param name="text">Testo da applicare</param>
        public void SetIconText(string text)
        {
            if (text.Length < 64)
                icon.Text = text;
            else
                icon.Text = text.Substring(0, 60) + "...";
        }
        /// <summary>
        /// Resetta il testo al valore originale
        /// </summary>
        public void SetIconText()
        {
            icon.Text = "Picu3 - Clicca per aprire.";
        }
    }
}
