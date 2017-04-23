using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    StreamReader SR = new StreamReader(file, Encoding.Default);
                    textBox1.Text = SR.ReadToEnd();
                    SR.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(this, textBox1.Text);
            form.Show();
        }

        public void changeText(string text)
        {
            textBox1.Text = text;
        }
    }
}