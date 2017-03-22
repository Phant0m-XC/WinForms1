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
            this.radioButton1.Checked = true;
            this.button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Today;
                DateTime valueDate = Convert.ToDateTime(this.textBox1.Text);
                TimeSpan result = valueDate - currentDate;
                if (this.radioButton1.Checked) //года
                {
                    double year = result.TotalDays / 365;
                    this.textBox2.Text = year.ToString();
                }
                else if (this.radioButton2.Checked) //месяцы
                {
                    double month = result.TotalDays / 30;
                    this.textBox2.Text = month.ToString();
                }
                else if (this.radioButton3.Checked) //дни
                {
                    this.textBox2.Text = result.TotalDays.ToString();
                }
                else if (this.radioButton4.Checked) //минуты
                {
                    this.textBox2.Text = result.TotalMinutes.ToString();
                }
                else if (this.radioButton5.Checked) //секунды
                {
                    this.textBox2.Text = result.TotalSeconds.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат даты\nВведите DD/MM/YYYY", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}