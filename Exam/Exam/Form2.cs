using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form2 : Form
    {
        private string[] str;
        public Form2(string[] str)
        {
            InitializeComponent();
            this.str = str;
            textBox1.Text = this.str[0];
            textBox2.Text = this.str[1];
            textBox3.Text = this.str[2];
            buttonSave.Enabled = false;
            textBox2.KeyPress += TextBox2_KeyPress;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.str[0] = textBox1.Text;
            this.str[1] = textBox2.Text;
            this.str[2] = textBox3.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }
    }
}