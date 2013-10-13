using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Picu3
{
    public partial class UploadList : Form
    {
        public UploadList()
        {
            InitializeComponent();
            
            if (!System.IO.File.Exists("icon.ico"))
                MessageBox.Show("Impossibile trovare icon.ico");
            else
                this.Icon = new Icon("icon.ico", 128, 128);

            //this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.listView2.MouseDoubleClick += listView1_MouseDoubleClick;
        }

        private void UploadList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        
        private void UploadList_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Inserisce un elemento all'interno di una delle due liste UploadList
        /// </summary>
        /// <param name="name">Indica il nome del file</param>
        /// <param name="result">Indica il result da inserire all'inizio</param>
        /// <param name="error">Indica se deve essere aggiunto al gruppo degli errori</param>
        /// <param name="list">Se viene specificato 1 verrà inserito nella lista degli upload, 0 in quella dei già caricati.</param>
        /// <returns></returns>
        public ListViewItem AddElementToList(string name, string result, int group, int list)
        {
            ListViewItem view = new ListViewItem();
            view.Group = listView1.Groups[group];
            view.Text = name;
            view.SubItems.Add(result);

            return listView1.Items.Add(view);
        }

        #region Update Item methods
        // Tutti i metodi che servono per gestire l'item
        public delegate void DelegateView();
        /// <summary>
        /// Aggiorna il titolo dell'upload
        /// </summary>
        /// <param name="item">Item dove applicare il nuovo titolo</param>
        /// <param name="title">Nuovo titolo da applicare</param>
        public void UpdateMainTitle(ListViewItem item, string title)
        {
            item.ListView.Invoke(new DelegateView(() => { item.Text = title; }));
        }
        /// <summary>
        /// Aggiorna lo status dell'upload
        /// </summary>
        /// <param name="item">Item dove applicare il nuovo status</param>
        /// <param name="result">Nuovo status da applicare</param>
        public void UpdateResultStatus(ListViewItem item, string result)
        {
            item.ListView.Invoke(new DelegateView(() => { item.SubItems[1].Text = result; }));
        }
        /// <summary>
        /// Modifica il gruppo di un item
        /// </summary>
        /// <param name="item">Item a cui cambiare gruppo</param>
        /// <param name="new_group">ID del nuovo gruppo</param>
        public void UpdateGroup(ListViewItem item, int new_group)
        {
            item.ListView.Invoke(new DelegateView(() => { item.Group = item.ListView.Groups[new_group]; }));
        }
        /// <summary>
        /// Aggiorna il tooltiptext dell'item
        /// </summary>
        /// <param name="item">Item a cui cambiare tooltiptext</param>
        /// <param name="tooltiptext">Nuovo tooltiptext da applicare</param>
        public void UpdateToolTipText(ListViewItem item, string tooltiptext)
        {
            item.ListView.Invoke(new DelegateView(() => { item.ToolTipText = tooltiptext; }));
        }
        /// <summary>
        /// Questo metodo si occupa di eliminare l'item specificato dalla listview
        /// </summary>
        /// <param name="item">L'item da distruggere</param>
        public void DeleteItem(ListViewItem item)
        {
            if(item != null)
                item.ListView.Items.Remove(item);
        }
        #endregion

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit;

            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
                hit = listView1.HitTest(e.X, e.Y);
            else
                hit = listView2.HitTest(e.X, e.Y);

            // Ha premuto su qualcosa
            if (hit.Item != null)
            {
                // il controllo per l'url corretto viene già effettuato, quindi qua dovrei andare sul sicuro
                if (hit.Item.ToolTipText == "ND" || hit.Item.ToolTipText.Length < 1)
                {
                    // ToDo Qualcosa
                    return;
                }

                Process.Start(hit.Item.ToolTipText);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                if (!File.Exists(Settings._ImagesList)) { listView2.Items.Clear(); return; }
                try
                {
                    listView2.Items.Clear();

                    // ToDo Convert to XML?
                    using (StreamReader reader = new StreamReader(Settings._ImagesList))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            // 0|1|2
                            // name|data|error
                            string[] split = line.Split('|');

                            if (split.Length < 3)
                                continue;

                            if (split[2] == "False")
                            {
                                // 0 => no error
                                ListViewItem item = new ListViewItem();

                                item.Group = listView2.Groups[0];
                                item.Text = split[0];
                                item.ToolTipText = split[1];
                                item.SubItems.Add(split[1]);

                                listView2.Items.Add(item);
                            }
                            else
                            {
                                // 1 => error
                                ListViewItem item = new ListViewItem();

                                item.Group = listView2.Groups[1];
                                item.Text = split[0];
                                item.ToolTipText = "ND";
                                item.SubItems.Add(split[1]);

                                listView2.Items.Add(item);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logs.Log("Error nel caricare e analizzare il file images.txt; Exception: " + ex.Message + " ; Stack trace: " + ex.StackTrace);
                    MessageBox.Show("Si è verificato un errore nel caricare la lista delle immagini.");
                }
            }
        }

        private void cancellaListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.ClearList(false);
        }

        private void cancellaListaFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.ClearList(true);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancellaCodaUploadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.upload.ClearQueue();
        }

        private void cancellaCodaUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.upload.ClearQueue(true);
        }

        private void anteprimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(listView1.SelectedItems[0].ToolTipText);
        }

        private void contextUpload_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                anteprimaToolStripMenuItem.Enabled = true;

                if (listView1.SelectedItems[0].Group == listView1.Groups[1])
                {
                    copiaURLToolStripMenuItem1.Enabled = true;
                }
                else
                {
                    copiaURLToolStripMenuItem1.Enabled = false;
                }
            }
            else
            {
                anteprimaToolStripMenuItem.Enabled = false;
                copiaURLToolStripMenuItem1.Enabled = false;
            }
        }

        private void anteprimaApriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(listView2.SelectedItems[0].ToolTipText);
        }

        private void copiaURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView2.SelectedItems[0].ToolTipText);
        }

        private void contextList_Opening(object sender, CancelEventArgs e)
        {
            copiaURLToolStripMenuItem.Enabled = (listView2.SelectedItems.Count > 0);
            anteprimaApriToolStripMenuItem.Enabled = (listView2.SelectedItems.Count > 0);
        }

        private void copiaURLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].ToolTipText);
        }

        private void cancellaTuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.upload.ClearQueue(true);
            listView1.Items.Clear();
        }
    }
}
