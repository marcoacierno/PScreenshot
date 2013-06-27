using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;


namespace Picu
{
    public partial class ListUp : Form
    {
        // error messages
        private const string url_picu = @"http://picu.site11.com/share_app.php";
        public Queue<string> thread_task;
        public Queue<int> thread_grid_task;
        private bool in_upload;
        public const string default_nolink = "No link";

        public ListUp()
        {
            InitializeComponent();

            this.FormClosing += ListUp_FormClosing;

            in_upload = false;

            thread_task = new Queue<string>();
            thread_grid_task = new Queue<int>();
        }

        void ListUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ha premuto il bottone "url"
            // apre l'url

            if (e.ColumnIndex == 2)
            {
                string url = (string)dataGridView1.Rows[e.RowIndex].Cells[2].Value;

                if (!url.Equals(default_nolink))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(url);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Impossibile aprire l'url (" + url + ")");
                    }
                }
            }
        }

        public void PreThread(string file)
        {
            // aggiunge il task al thread
            thread_task.Enqueue(file);
            // upload_list.dataGridView1.
            int n = dataGridView1.Rows.Add(Path.GetFileName(file), "No, in attesa.", default_nolink);
            thread_grid_task.Enqueue(n);

            // se il thread non è già attivo
            // lo starta

            if (!in_upload)
            {
                RunUploader();
            }
        }

        private void RunUploader()
        {
            if (thread_task.Count == 0)
            {
                in_upload = false;
                return;
            }

            in_upload = true;

            string file = thread_task.Dequeue();
            int id = thread_grid_task.Dequeue();

            dataGridView1.Rows[id].Cells[1].Value = "In upload.. attendere";

            WebClient wb = new WebClient();

            wb.UploadFileCompleted += (sender, e) => wb_UploadedEnded(sender, e, id, file);
            wb.UploadProgressChanged += (sender, e) => wb_UpdateProgress(sender, e, id);

            new Thread(
                () =>
                {
                    wb.UploadFileAsync(new Uri(url_picu), file);
                }
            ).Start();
        }

        void wb_UploadedEnded(object sender, UploadFileCompletedEventArgs e, int id, string name)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("La richiesta è stata cancellata (?)");
                return;
            }

            BeginInvoke(new MethodInvoker(() =>
            {
                Program.form1.last_ss_upload = Encoding.ASCII.GetString(e.Result);

                dataGridView1.Rows[id].Cells[2].Value = Program.form1.last_ss_upload;
                dataGridView1.Rows[id].Cells[1].Value = "No, caricato.";

                Program.form1.icon.BalloonTipText = "File " + Path.GetFileName(name) + " caricato. Clicca per aprire.";
                Program.form1.icon.ShowBalloonTip(Form1.info_time);

                Program.form1.ChangeAction(Form1.TEXT_BALLOONTIP_ACTION.OPEN_PIC, Program.form1.last_ss_upload);
            }
            ));

            RunUploader();
        }

        void wb_UpdateProgress(object sender, UploadProgressChangedEventArgs e, int id)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                dataGridView1.Rows[id].Cells[1].Value = "In Upload (" + e.ProgressPercentage + "%)";
            }
            ));
        }

        private void ListUp_Load(object sender, EventArgs e)
        {

        }
    }
}
