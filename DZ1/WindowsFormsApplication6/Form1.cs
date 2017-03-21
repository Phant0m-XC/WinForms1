using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
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
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Text);
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.textBox1.Text = "Понедельник";
                    break;
                case DayOfWeek.Tuesday:
                    this.textBox1.Text = "Вторник";
                    break;
                case DayOfWeek.Wednesday:
                    this.textBox1.Text = "Среда";
                    break;
                case DayOfWeek.Thursday:
                    this.textBox1.Text = "Четверг";
                    break;
                case DayOfWeek.Friday:
                    this.textBox1.Text = "Пятница";
                    break;
                case DayOfWeek.Saturday:
                    this.textBox1.Text = "Суббота";
                    break;
                case DayOfWeek.Sunday:
                    this.textBox1.Text = "Воскресение";
                    break;
            }
        }
    }
}
