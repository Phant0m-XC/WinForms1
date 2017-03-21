using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Today;
                DateTime valueDate = Convert.ToDateTime(this.textBox1.Text);
                TimeSpan result = valueDate - currentDate;
                this.textBox2.Text = result.TotalDays.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат даты\nВведите DD/MM/YYYY", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
