using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml;

namespace Picu3
{
    class DoScreen
    {
        /// <summary>
        /// Esegue lo screen
        /// </summary>
        /// <param name="area">Indica la porzione dove eseguire lo screen</param>
        /// <param name="format">Indica il formato da utilizzare, se viene passato il valore null utilizza i valori settings del Form1</param>
        public static void Screenshot(SCREEN_AREA area, ImageFormat format = null)
        {
            if (format == null)
            {
                format = Form1.settings.Formato;
            }

            if (!Directory.Exists(Form1.settings.GalleryPath))
            {
                Directory.CreateDirectory(Form1.settings.GalleryPath);
            }

            string time = DateTime.Now.ToString("dd-mm-yyyy HH-mm-ss");
            string url = Form1.settings.GalleryPath + "\\" + time + "." + Utils.StringFromImageFormat(format);

            int idx = 1;

            while (File.Exists(url))
            {
                url = Form1.settings.GalleryPath + "\\" + time + " (" + idx + ")" + "." + Utils.StringFromImageFormat(format);
                ++idx;
            }

            ScreenShot.ScreenCapture sc = null;
            Image img = null;

            switch (area)
            {
                case SCREEN_AREA.CAPTURE_ALL:
                    sc = new ScreenShot.ScreenCapture();
                    img = sc.CaptureScreen();

                    img.Save(url, format);
                    break;
                case SCREEN_AREA.CAPTURE_ACT_WINDOW:
                    sc = new ScreenShot.ScreenCapture();
                    img = sc.CaptureWindow(Form1.GetForegroundWindow());

                    img.Save(url, format);
                    break;
            }

            Form1.notify.SendMessage("Screen catturato", "Lo screen " + Path.GetFileNameWithoutExtension(url) + " e' stato salvato.", System.Windows.Forms.ToolTipIcon.Info, (ee, s) => { Process.Start(url); });
            Form1.upload.AddUpload(url);
        }

        public static void UpdateScreenList(string name, string result, bool error)
        {
            using (StreamWriter w = new StreamWriter(Settings._ImagesList, true))
            {
                w.WriteLine(name + "|" + result + "|" + error.ToString());
            }
        }
    }
}
