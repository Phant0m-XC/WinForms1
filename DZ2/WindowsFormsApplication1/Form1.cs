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
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openfile.Click += openfile_Click;
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            using (FileStream file = File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int) file.Length;
                progressBar1.Value = 0;
                byte[] quant = new byte[1];
                for(int i = 0; i < file.Length; i++)
                {
                    file.Read(quant, 0, 1);
                    textBox1.Text += Encoding.Default.GetString(quant);
                    progressBar1.Value++;
                    Thread.Sleep(1);
                }
                file.Close();
            }
        }
    }
}
