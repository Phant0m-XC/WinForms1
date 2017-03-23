using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
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
            Random random = new Random();
            DialogResult result;
            bool goOn = true;
            while (goOn)
            {
                int count = 0;
                do
                {
                    count++;
                    result = MessageBox.Show("Ваше число - " + random.Next(1, 2000).ToString() + '?', "Число", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                } while (result != DialogResult.Yes);
                MessageBox.Show($"Количество попыток - {count}", "Статистика", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                result = MessageBox.Show("Желаете сыграть ещё?", "Вопрос", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    goOn = false;
            }
        }
    }
}
