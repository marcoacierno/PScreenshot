using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Picu3
{
    /// <summary>
    /// Contiene le informazioni riguardo un upload, ovvero:
    /// il nome del file, l'url del file e l'id che UploadList gli ha assegnato nella listView del form
    /// </summary>
    struct UploadInfo
    {
        /// <summary>
        /// Contiene il nome del file
        /// </summary>
        public string fileName;
        /// <summary>
        /// Contiene l'url del file
        /// </summary>
        public string fileUrl;
        /// <summary>
        /// Contiene l'ID assegnato all'upload
        /// </summary>
        public ListViewItem listViewID;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="fname">Nome del file</param>
        /// <param name="path">L'url del file</param>
        /// <param name="id">L'id assegnato</param>
        public UploadInfo(string fname, string path, ListViewItem id)
        {
            fileName = fname;
            fileUrl = path;
            listViewID = id;
        }
    }

    /// <summary>
    /// La classe si occupa di gestire tutti gli upload dell'applicazione
    /// </summary>
    public class Upload
    {
        public static Uri url = new Uri(@"http://picu.site11.com/share_app.php");

        private WebClient wc = new WebClient();
        /// <summary>
        /// Una copia del form UploadList, usato per accedere ai membri per lavorare sulle liste
        /// </summary>
        public static UploadList uploadlist;
        /// <summary>
        /// Indica se il worker sta lavorando su qualcosa
        /// </summary>
        public bool inWorking { get; private set; }
        /// <summary>
        /// Indica l'UploadInfo su cui l'uploader sta lavorando
        /// </summary>
        private UploadInfo working_UI;
        /// <summary>
        /// Contiene tutti gli uploads da eseguire
        /// </summary>
        private Queue<UploadInfo> queue = new Queue<UploadInfo>();

        /// <summary>
        /// Costruttore, associo i delegate al WebClient per gestire le fasi dell'upload. Non richiede parametri
        /// </summary>
        public Upload()
        {
            wc.UploadFileCompleted += wc_UploadFileCompleted;
            wc.UploadProgressChanged += wc_UploadProgressChanged;

            inWorking = false;
        }
        /// <summary>
        /// Chaimato dall'uploader quando ci sono aggiornamenti nell'upload (per ottenere la percentuale)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            uploadlist.UpdateResultStatus(working_UI.listViewID, e.ProgressPercentage.ToString() + "%");
        }

        /// <summary>
        /// Chiamato dall'uploader quando l'upload è terminato; comunica il risultato e riavvia l'uploader fino a completare la coda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                Uri isOk;
                string result = Encoding.ASCII.GetString(e.Result);

                if (Uri.TryCreate(result, UriKind.Absolute, out isOk) && isOk.Scheme == Uri.UriSchemeHttp)
                {
                    // L'upload è terminato con successo
                    uploadlist.UpdateGroup(working_UI.listViewID, 1);
                    uploadlist.UpdateResultStatus(working_UI.listViewID, "Completato! Clicca per aprire");
                    uploadlist.UpdateToolTipText(working_UI.listViewID, result);

                    DoScreen.UpdateScreenList(working_UI.fileName, result, false);
                }
                else
                {
                    // Si è verificato un errore
                    uploadlist.UpdateGroup(working_UI.listViewID, 2);
                    uploadlist.UpdateResultStatus(working_UI.listViewID, result);

                    DoScreen.UpdateScreenList(working_UI.fileName, result, true);

                    Logs.Log("L'upload del file è fallito. Errore: " + result);
                }
            }
            catch(Exception ee)
            {
                Logs.Log("Exception dall'uploader; Expcetion: " + ee.Message + " ; Stack trace: " + ee.StackTrace);
                Logs.Log("^^ Innerexception: " + ee.InnerException.Message + " ; stack trace: " + ee.InnerException.StackTrace);

                uploadlist.UpdateGroup(working_UI.listViewID, 2);
                uploadlist.UpdateResultStatus(working_UI.listViewID, "Impossibile risolvere l'host (?)");

                DoScreen.UpdateScreenList(working_UI.fileName, "??", true);
            }
            finally
            {
                // Riesegue l'uploader fino a completare la queue
                RunUploader();
            }
        }

        /// <summary>
        /// Consente l'aggiunta di un elemento all'interno della lista d'attesa
        /// </summary>
        /// <param name="fileUrl">L'url dell'immagine/file</param>
        /// <param name="fileName">Il nome del file, se null viene preso dalla path</param>
        public void AddUpload(string fileUrl, string fileName = null)
        {
            if (fileName == null)
                fileName = Path.GetFileNameWithoutExtension(fileUrl);

            queue.Enqueue(new UploadInfo(fileName, fileUrl, uploadlist.AddElementToList(fileName, "In attesa...", 0, 0)));

            PrepareUploader();
        }

        /// <summary>
        /// Prepara l'uploader per l'upload lol
        /// </summary>
        private void PrepareUploader()
        {
            if (inWorking)
                return;

            inWorking = true;
            RunUploader();
        }

        /// <summary>
        /// Esegue effettivamente l'uploader prendendo i dati dalla queue e avviando il WebClient per l'upload
        /// </summary>
        private void RunUploader()
        {
            if (queue.Count < 1)
            {
                inWorking = false;
                return;
            }

            // Estrae le informazioni
            working_UI = queue.Dequeue();
            uploadlist.UpdateToolTipText(working_UI.listViewID, "ND");

            // In se per se UploadFileASync già crea un suo thread, ma avvio il thread in un altro thread per evitare che l'applicazioni si blocchi quando WebClient
            // prova a stabilire una connessione con il server
            new Thread(() =>
            {
                uploadlist.UpdateResultStatus(working_UI.listViewID, "In upload..");
                wc.UploadFileAsync(url, working_UI.fileUrl);
            }).Start();
        }
    }
}
