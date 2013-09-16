using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Picu3
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (Mutex _m = new Mutex(false, "OEsN-O6mS-AIge-Q6kN-qxy7-HDcb-O8wt-EyZg"))
                {
                    if (!_m.WaitOne(0, false))
                    {
                        Logs.Log("Mutex failed; picu è già stato aperto.");
                        MessageBox.Show("E' possibile aprire Picu una sola volta");
                        return;
                    }

                    Logs.Log("Programma avviato");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }
            catch(Exception e)
            {
                Logs.Log("Exception: " + e.Message + "; Stack trace: " + e.StackTrace);
            }
        }
    }
}
