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
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Text);
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    textBox1.Text = "Понедельник";
                    break;
                case DayOfWeek.Tuesday:
                    textBox1.Text = "Вторник";
                    break;
                case DayOfWeek.Wednesday:
                    textBox1.Text = "Среда";
                    break;
                case DayOfWeek.Thursday:
                    textBox1.Text = "Четверг";
                    break;
                case DayOfWeek.Friday:
                    textBox1.Text = "Пятница";
                    break;
                case DayOfWeek.Saturday:
                    textBox1.Text = "Суббота";
                    break;
                case DayOfWeek.Sunday:
                    textBox1.Text = "Воскресение";
                    break;
            }
        }
    }
}
