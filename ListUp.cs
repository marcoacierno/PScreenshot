using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Picu
{
    public partial class ListUp : Form
    {
        // error messages
        public ListUp()
        {
            InitializeComponent();

            this.FormClosing += ListUp_FormClosing;
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

                if (!url.Equals(Form1.default_nolink))
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

        private void ListUp_Load(object sender, EventArgs e)
        {

        }
    }
}
