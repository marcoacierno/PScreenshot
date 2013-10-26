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
        /// <summary>
        /// Indica che deve catturare tutto lo schermo
        /// </summary>
        CAPTURE_ALL,
        /// <summary>
        /// Indica che deve catturare solamente la finestra attiva
        /// </summary>
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

        public string GalleryPath;

        private string _GalleryDir;
        public string GalleryDir
        {
            get { return _GalleryDir; }
            set
            {
                _GalleryDir = value;
                GalleryPath = SpecialPaths.DocFolder + value;

                SaveConfig();
            }
        }

        public static string _ImagesList = SpecialPaths.DocFolder + "images.txt";

        //private bool _OpenPicuForm;
        //public bool OpenPicuForm
        //{
        //    get { return _OpenPicuForm; }
        //    set
        //    {
        //        _OpenPicuForm = value;
        //        SaveConfig();
        //    }
        //}

        #region Save and Load
        /// <summary>
        /// Inserisce all'interno del file i settings default
        /// </summary>
        public void DefaultConfig()
        {
            _ScreenArea = SCREEN_AREA.CAPTURE_ALL;
            _Formato = ImageFormat.Png;
            _GalleryDir = "Galleria";
            GalleryPath = SpecialPaths.DocFolder + "Galleria";
            //_OpenPicuForm = true;

            SaveConfig();
        }

        /// <summary>
        /// Carica la configurazione dal file
        /// </summary>
        public void LoadConfig()
        {
            if (!File.Exists(SpecialPaths.DocFolder + "settings.xml")) { DefaultConfig(); return; }

            try
            {
                using(XmlReader reader = XmlReader.Create(SpecialPaths.DocFolder + "settings.xml"))
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
                                GalleryPath = SpecialPaths.DocFolder + _GalleryDir;
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
                //Logs.Log("Caricamento del config fallito. Exception: " + e.Message + " ; Stack trace:" + e.StackTrace);
                Logs.Log(e);
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

                using (XmlWriter writer = XmlWriter.Create(SpecialPaths.DocFolder + "settings.xml", wsetitngs))
                {
                    writer.WriteStartDocument();

                    writer.WriteStartElement("settings");

                    writer.WriteElementString("screenarea", ((int)_ScreenArea).ToString());

                    writer.WriteElementString("formato", Utils.StringFromImageFormat(_Formato));

                    writer.WriteElementString("gallery", _GalleryDir);

                    //writer.WriteElementString("picuform", _OpenPicuForm.ToString());

                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                }

                Logs.Log("Salvataggio config OK. Memoria liberata.");
            }
            catch(Exception e)
            {
                Logs.Log(e);
                //Logs.Log("Salvataggio del config fallito. Exception: " + e.Message + " -- Stack trace: " + e.StackTrace );
            }
        } 
        #endregion
    }
}
