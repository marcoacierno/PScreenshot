using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Picu3
{
    class Utils
    {
        private static Dictionary<string, ImageFormat> extensions = new Dictionary<string, ImageFormat>()
        {
            {"png", ImageFormat.Png},
            {"jpeg", ImageFormat.Jpeg},
            {"bmp", ImageFormat.Bmp},
            {"gif", ImageFormat.Gif}
        };

        private static Dictionary<ImageFormat, string> ifExtensions = new Dictionary<ImageFormat, string>()
        {
            {ImageFormat.Png, "png"},
            {ImageFormat.Jpeg, "jpeg"},
            {ImageFormat.Bmp, "bmp"},
            {ImageFormat.Gif, "gif"}
        };

        public static string StringFromImageFormat(ImageFormat imageFormat)
        {
            if (imageFormat == null) throw new Exception("Il valore default per il formato delle immagini non è stato assegnato.");

            return ifExtensions[imageFormat];
        }

        public static ImageFormat ImageFormatFromString(string format)
        {
            if (format[0] == '.') format = format.Substring(1);
            return extensions[format.ToLower()];
        }

        public static bool IsValidExtension(string extension)
        {
            return extensions.ContainsKey(extension);
        }
    }

    class Logs
    {
        public static void Log(string text)
        {
            using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\logs.txt", true))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "]: " + text);
            }
        }
    }
}
