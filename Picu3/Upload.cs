using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Salta ogni controllo etc, riesegue l'uploader
            if (e.Cancelled)
            {
                RunUploader();
                return;
            }

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

                    Form1.notify.SendMessage("Upload OK", "L'Upload di " + working_UI.fileName + " è terminato.", ToolTipIcon.Info, (ee, s) => { Process.Start(result); });
                }
                else
                {
                    // Si è verificato un errore
                    uploadlist.UpdateGroup(working_UI.listViewID, 2);
                    uploadlist.UpdateResultStatus(working_UI.listViewID, result);

                    DoScreen.UpdateScreenList(working_UI.fileName, result, true);

                    Form1.notify.SendMessage("Errore upload", "L'upload di " + working_UI.fileName + " è fallito.", ToolTipIcon.Error, null);

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

            ListViewItem item = uploadlist.AddElementToList(fileName, "In attesa...", 0, 0);
            queue.Enqueue(new UploadInfo(fileName, fileUrl, item));
            uploadlist.UpdateToolTipText(item, fileUrl);

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
            uploadlist.UpdateToolTipText(working_UI.listViewID, working_UI.fileUrl);

            // In se per se UploadFileASync già crea un suo thread, ma avvio il thread in un altro thread per evitare che l'applicazioni si blocchi quando WebClient
            // prova a stabilire una connessione con il server
            new Thread(() =>
            {
                uploadlist.UpdateResultStatus(working_UI.listViewID, "In upload..");
                wc.UploadFileAsync(url, working_UI.fileUrl);
            }).Start();
        }

        /// <summary>
        /// Si occupa di cancellare tutta la coda upload ma non cancella l'upload in corso
        /// </summary>
        /// <param name="inUpload">Se true il metodo deve cancellare anche l'upload in corso</param>
        public void ClearQueue(bool inUpload = false)
        {
            // Se ci sono elementi nella queue, la pulisce
            if (queue.Count > 0)
            {
                // Non credo che il multi thread sia un problema
                // RunUploader non viene eseguito su un thread diverso
                // wc_UploadFileCompleted dovrebbe tornare sull'UI thread

                foreach (UploadInfo info in queue)
                {
                    uploadlist.DeleteItem(info.listViewID);
                }

                queue.Clear();
            }

            //Controllo se l'utente vuole cancellare anche l'upload in corso; 
            if(inUpload)
            {
                // Vedo se il worker sta effettivamente lavorando
                if (inWorking)
                {
                    uploadlist.DeleteItem(working_UI.listViewID);
                    wc.CancelAsync();
                }
            }
        }
    }
}
