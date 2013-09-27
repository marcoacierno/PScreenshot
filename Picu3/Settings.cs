using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Xml;

namespace Picu3
{
    public enum SCREEN_AREA
    {
        CAPTURE_ALL,
        CAPTURE_ACT_WINDOW,
    }

    public struct Settings
    {
        private SCREEN_AREA _ScreenArea;
        public SCREEN_AREA ScreenArea
        {
            get { return _ScreenArea; }
            set
            {
                _ScreenArea = value;
                SaveConfig();
            }
        }

        private ImageFormat _Formato;
        public ImageFormat Formato
        {
            get { return _Formato; }
            set
            {
                _Formato = value;
                SaveConfig();
            }
        }

        private string _GalleryDir;
        public string GalleryDir
        {
            get { return _GalleryDir; }
            set
            {
                _GalleryDir = value;
                GalleryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\" + value;

                SaveConfig();
            }
        }
        public static string _ImagesList = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\images.txt";

        public string GalleryPath;

        #region Save and Load
        /// <summary>
        /// Inserisce all'interno del file i settings default
        /// </summary>
        public void DefaultConfig()
        {
            _ScreenArea = SCREEN_AREA.CAPTURE_ALL;
            _Formato = ImageFormat.Png;
            _GalleryDir = "Galleria";
            GalleryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\Galleria";

            SaveConfig();
        }

        /// <summary>
        /// Carica la configurazione dal file
        /// </summary>
        public void LoadConfig()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\settings.xml")) { DefaultConfig(); return; }

            try
            {
                using(XmlReader reader = XmlReader.Create("settings.xml"))
                {
                    bool readed = false;
                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case "screenarea":
                                if (readed) { readed = false; continue; }

                                reader.Read();
                                readed = true;
                                _ScreenArea = (SCREEN_AREA)Convert.ToInt16(reader.Value);
                                break;
                            case "formato":
                                if (readed) { readed = false; continue; }
                                reader.Read();
                                readed = true;
                                _Formato = Utils.ImageFormatFromString(reader.Value);
                                break;
                            case "gallery":
                                if (readed) { readed = false; continue; }
                                reader.Read();
                                readed = true;
                                _GalleryDir = reader.Value;
                                GalleryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\" + _GalleryDir;
                                break;
                        }
                    }
                }

                Logs.Log("Caricamento config OK. Memoria liberata con successo.");
            }
            catch (Exception e) 
            { 
                // Loading fallito
                // Carica default config
                Logs.Log("Caricamento del config fallito. Exception: " + e.Message + " ; Stack trace:" + e.StackTrace);
                DefaultConfig();
            }
        }

        /// <summary>
        /// Salva la configurazione
        /// </summary>
        public void SaveConfig()
        {
            try
            {
                XmlWriterSettings wsetitngs = new XmlWriterSettings();
                wsetitngs.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\settings.xml", wsetitngs))
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("settings");

                    writer.WriteElementString("screenarea", ((int)_ScreenArea).ToString());

                    writer.WriteElementString("formato", Utils.StringFromImageFormat(_Formato));

                    writer.WriteElementString("gallery", _GalleryDir);

                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                }

                Logs.Log("Salvataggio config OK. Memoria liberata.");
            }
            catch(Exception e)
            {
                Logs.Log("Salvataggio del config fallito. Exception: " + e.Message + " -- Stack trace: " + e.StackTrace );
            }
        } 
        #endregion
    }
}
