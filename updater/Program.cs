using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Diagnostics;
using System.Windows.Forms;

/// Updater si occupa di aggiornare l'applicazione Picu

namespace updater
{
    static class Program
    {
        /*
        public static string picuDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\";
        //http://revonline.comuf.com/downloads/picu/version.xml
        //http://revonline.comuf.com/downloads/picu/latest.zip
        static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();    
            Console.WriteLine("Attendere mentre mi connetto al sito...");

            xml.Load("http://revonline.comuf.com/downloads/picu/version.xml");

            string version = xml.SelectSingleNode("picu/version").InnerText;
            
            Console.WriteLine("Attendere mentre controllo la versione installata..");

            if (!Directory.Exists(picuDir) || !File.Exists(picuDir + "version.txt"))
            {
                Console.WriteLine("La cartella Picu o il file version.txt non è stato trovato.. sei sicuro di aver aperto Picu almeno una volta?");
            }
            else
            {
                if (!Directory.Exists(picuDir + "update"))
                {
                    Directory.CreateDirectory(picuDir + "update");
                }

                string my_version = File.ReadAllText(picuDir + "version.txt");

                Console.WriteLine("Ultima versione: " + version);
                Console.WriteLine("Versione in uso: " + my_version);

                my_version = my_version.Replace(Environment.NewLine, string.Empty);

                if (version.Equals(my_version))
                {
                    Console.WriteLine("Questa versione è la più recente");
                }
                else
                {
                    Console.WriteLine(".. Attendere mentre scarico la nuova versione.");
                    try
                    {
                        WebClient wc = new WebClient();
                        wc.DownloadFile("http://revonline.comuf.com/downloads/picu/latest.zip", picuDir + "update\\latest.zip");

                        if (!File.Exists(picuDir + "\\update\\latest.zip"))
                        {
                            Console.WriteLine("Il file latest.zip non esiste... ???");
                        }
                        else
                        {
                            using (ZipInputStream zip = new ZipInputStream(File.OpenRead(picuDir + "update\\latest.zip")))
                            {
                                ZipEntry entry;

                                while ((entry = zip.GetNextEntry()) != null)
                                {
                                    Console.WriteLine(".. sto espertando: " + entry.Name);

                                    // Controllo se il file è in una cartella
                                    string dirName = Path.GetDirectoryName(entry.Name);

                                    if (dirName.Length > 0) Directory.CreateDirectory(dirName);

                                    string file = Path.GetFileName(entry.Name);

                                    if (file != string.Empty)
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

                            Console.WriteLine("La nuova versione è stata scaricata con successo.");
                            Console.WriteLine("'Y' Per eseguire ora l'aggiornamento, 'N' per aprire la cartella e chiudere l'updater.");
                            Console.WriteLine("Attenzione, se si sta eseguendo un upload con picu premere Y causerà la chiusura dell'applicazione e la perdita dell'upload.");
                            string line = Console.ReadLine();

                            if (line == "Y")
                            {
                                foreach (Process info in Process.GetProcessesByName("Picu3"))
                                {
                                    info.Kill();
                                }

                                Process.Start(picuDir + "update\\Picu.msi");
                            }
                            else
                            {
                                Process.Start(picuDir + "update");
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("L'update è FALLITO.");
                        Console.WriteLine("Motivo: " + e.Message);
                    }
                }
            }

            Console.ReadKey(true);
        }
        */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
