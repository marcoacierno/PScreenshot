using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace updater
{
    public partial class Form1 : Form
    {
        public static string picuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\";
        //http://revonline.comuf.com/downloads/picu/version.xml
        //http://revonline.comuf.com/downloads/picu/latest.zip
        private WebClient wc = new WebClient();
        private string version;
        private bool inDownload;

        public Form1()
        {
            InitializeComponent();

            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;

            inDownload = false;
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!File.Exists(picuDir + "\\update\\latest.zip"))
            {
                label1.Text = "Download fallito.";
                return;
            }

            label1.Text = "Estrazione..";
            inDownload = false;

            try
            {
                using (ZipInputStream zip = new ZipInputStream(File.OpenRead(picuDir + "update\\latest.zip")))
                {
                    ZipEntry entry;

                    while ((entry = zip.GetNextEntry()) != null)
                    {
                        Console.WriteLine(".. sto espertando: " + entry.Name);

                        // Controllo se il file è in una cartella
                        string dirName = Path.GetDirectoryName(entry.Name);

                        if (dirName.Length > 0)
                        {
                            Directory.CreateDirectory(picuDir + "update\\" + dirName);
                        }

                        if (Path.GetFileName(entry.Name) != string.Empty)
                        {
                            using (FileStream flusso = File.Create(picuDir + "update\\" + entry.Name))
                            {
                                byte[] b = new byte[2048];
                                int size = 2048;

                                while (true)
                                {
                                    size = zip.Read(b, 0, b.Length);

                                    if (size > 0)
                                    {
                                        flusso.Write(b, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                label1.Text = "L'estrazione è fallita.. motivo: " + ex.Message + (ex.InnerException != null ? (", " + ex.InnerException.Message) : ".");
            }

            label1.Text = "Estrazione terminata. Picu sarà chiuso per permettere l'update";

            foreach(Process p in Process.GetProcessesByName("Picu"))
            {
                p.Kill();
            }

            Process.Start(picuDir + "update//Picu.msi");
            this.Close();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Attendere.. Scaricati " + e.BytesReceived + " bytes di " + e.TotalBytesToReceive + " bytes.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("http://revonline.comuf.com/downloads/picu/version.xml");
            progressBar1.Value = 50;
            version = xml.SelectSingleNode("picu/version").InnerText;
            progressBar1.Value = 100;

            if (!Directory.Exists(picuDir) || !File.Exists(picuDir + "version.txt"))
            {
                label1.Text = "La cartella Picu o il file version.txt non è stato trovato.. sei sicuro di aver aperto Picu almeno una volta?";
            }
            else
            {
                if (!Directory.Exists(picuDir + "update"))
                {
                    Directory.CreateDirectory(picuDir + "update");
                }
                else
                {
                    Directory.Delete(picuDir + "update", true);
                    Directory.CreateDirectory(picuDir + "update");
                }

                string my_version = File.ReadAllText(picuDir + "version.txt");

                my_version = my_version.Replace(Environment.NewLine, string.Empty);

                if (version.Equals(my_version))
                {
                    label1.Text = "La versione in uso è l'ultima versione rilasciata";
                }
                else
                {
                    label1.Text = "Stai usando la versione " + my_version + ", l'ultima rilasciata é: " + version;
                    
                    wc.DownloadFileAsync(new Uri("http://revonline.comuf.com/downloads/picu/latest.zip"), picuDir + "update\\latest.zip");
                    inDownload = true;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (inDownload)
            //{
            //    wc.CancelAsync();
            //    if (File.Exists(picuDir + "\\update\\latest.zip"))
            //    {
            //        File.Delete(picuDir + "\\update\\latest.zip");
            //        return;
            //    }
            //}
        }
    }
}
