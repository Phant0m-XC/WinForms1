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
    public partial class Form2 : Form
    {
        private Product prod;
        public Form2(Product prod)
        {
            InitializeComponent();
            this.prod = prod;
            textBox1.Text = prod.Name;
            textBox2.Text = prod.Charactiristics;
            textBox3.Text = prod.Description;
            textBox4.Text = prod.Price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != null && textBox1.Text != "" &&
                textBox2.Text != null && textBox2.Text != "" &&
                textBox3.Text != null && textBox3.Text != "" &&
                textBox4.Text != null && textBox4.Text != "")
                {
                    prod.Name = textBox1.Text;
                    prod.Charactiristics = textBox2.Text;
                    prod.Description = textBox3.Text;
                    prod.Price = Convert.ToDouble(textBox4.Text);
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
