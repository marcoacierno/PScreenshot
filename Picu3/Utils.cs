using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Picu3
{
    class Utils
    {
        /// <summary>
        /// Passando come key la stringa ti ritorna sotto forma di ImageFormat il formato
        /// </summary>
        private static Dictionary<string, ImageFormat> extensions = new Dictionary<string, ImageFormat>()
        {
            {"png", ImageFormat.Png},
            {"jpeg", ImageFormat.Jpeg},
            {"bmp", ImageFormat.Bmp},
            {"gif", ImageFormat.Gif}
        };
        /// <summary>
        /// Passando come key l'imageformat ti ritorna il formato sotto forma di stringa
        /// </summary>
        private static Dictionary<ImageFormat, string> ifExtensions = new Dictionary<ImageFormat, string>()
        {
            {ImageFormat.Png, "png"},
            {ImageFormat.Jpeg, "jpeg"},
            {ImageFormat.Bmp, "bmp"},
            {ImageFormat.Gif, "gif"}
        };

        public static string StringFromImageFormat(ImageFormat imageFormat)
        {
            if (imageFormat == null) throw new ArgumentException("Il valore default per il formato delle immagini non è stato assegnato.");

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

        /// <summary>
        /// Cancella tutti i files della lista o dal computer se deleteFiles è true.
        /// </summary>
        /// <param name="deleteFiles">Se true verranno cancellati tutti i files</param>
        public static void ClearList(bool deleteFiles)
        {
            if (Directory.Exists(Form1.settings.GalleryPath) && deleteFiles)
            {
                Directory.Delete(Form1.settings.GalleryPath, true);
            }

            File.Delete(Settings._ImagesList);
        }
    }
    /// <summary>
    /// Classe che si occupa di eseguire i log sul file
    /// </summary>
    class Logs
    {
        public static void Log(string text)
        {
            using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\logs.txt", true))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "]: " + text);
            }
        }

        public static void Log(Exception e)
        {
            using (StreamWriter sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\logs.txt", true))
            {
                sw.WriteLine("[" + DateTime.Now.ToString() + "] == Exception " + e.ToString() + " in " + e.Source + " ==");
                sw.WriteLine("Message: " + e.Message);
                sw.WriteLine("Stack trace: \n" + e.StackTrace);
                
                Exception ex = e.InnerException;

                while(ex != null)
                {
                    sw.WriteLine("== Inner exception " + ex.ToString() + " in " + ex.Source + " ==");
                    sw.WriteLine("Message: " + ex.Message);
                    sw.WriteLine("Stack trace: \n" + ex.StackTrace);
                    sw.WriteLine("== Fine inner exception ==");

                    ex = ex.InnerException;
                }

                sw.WriteLine("== Fine exception ==");
            }
        }
    }
}
