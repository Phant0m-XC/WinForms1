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

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (Directory.Exists(textBox1.Text))
            {
                if (textBox2.Text != null && textBox2.Text != "")
                {
                    string[] filesName = Directory.GetFiles(textBox1.Text, textBox2.Text);
                    for (int i = 0; i < filesName.Length; i++)
                    {
                        listBox1.Items.Add(filesName[i]);
                    }
                }
                else
                    MessageBox.Show("Пустая строка маски", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Пустая строка адреса или директория отсутствует", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}