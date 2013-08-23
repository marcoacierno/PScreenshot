using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Picu
{
    static class Program
    {
        public static Form1 form1;
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex _m = new Mutex(false, "OEsN-O6mS-AIge-Q6kN-qxy7-HDcb-O8wt-EyZg"))
            {
                if (!_m.WaitOne(0, false))
                {
                    MessageBox.Show("Picu è già stato aperto");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(form1 = new Form1());
            }
        }
    }
}
