using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form2 : Form
    {
        private Form1 form;
        public Form2(Form1 form, string text)
        {
            InitializeComponent();
            this.form = form;
            textBox1.Text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.changeText(textBox1.Text);
            this.Close();
        }
    }
}
