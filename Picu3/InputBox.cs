using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Picu3
{
    public partial class InputBox : Form
    {
        public event EventHandler Button1_Press;
        public event EventHandler Button2_Press;

        // ToDo: Improve "placeholder" system
        public string Placeholder;
        public string InputText
        {
            get
            {
                return textBox1.Text;
            }
        }

        public InputBox(string title, string message, string placeholder, string button1, string button2)
        {
            InitializeComponent();

            label1.Text = message;
            this.Text = title;
            this.button1.Text = button1;
            this.button2.Text = button2;
            this.textBox1.Text = Placeholder = placeholder;

            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            label1.AutoSize = true;
            textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;
            this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Button1_Press != null) Button1_Press(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Button2_Press != null) Button2_Press(sender, e);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                textBox1.Text = Placeholder;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == Placeholder)
                textBox1.Text = "";
        }
    }
}
