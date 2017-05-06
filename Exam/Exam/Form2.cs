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
        public Form2(ref string[] str)
        {
            InitializeComponent();
            textBox1.Text = str[0];
            textBox2.Text = str[1];
            textBox3.Text = str[2];
        }
    }
}