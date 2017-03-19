using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resume.Click += Resume_Click;
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            string[] str = { "Ситушкин Антон Александрович",
                "Образование высшее",
                "Обучался в КА ШАГ на кафедре",
                "Разработка програмного обеспечения" };
            int sum = 0;
            for (int i = 0; i < str.Length; i++) {
                sum += str[i].Length;
                if(i!=str.Length - 1)
                MessageBox.Show(str[i], "Резюме", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                else
                    MessageBox.Show(str[i], $"Среднее количество слов на странице - {(int)(sum/(i+1))}", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
