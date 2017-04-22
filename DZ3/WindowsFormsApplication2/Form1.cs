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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            Form2 form2 = new Form2(prod);
            form2.ShowDialog();
            if (prod.Name != null && prod.Name != "" &&
                prod.Charactiristics != null && prod.Charactiristics != "" &&
                prod.Description != null && prod.Description != "")
                comboBox1.Items.Add(prod);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ((Product)comboBox1.SelectedItem).Price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                Product prod = (Product)comboBox1.SelectedItem;
                comboBox1.Items.Remove(prod);
                Form2 form2 = new Form2(prod);
                form2.ShowDialog();
                comboBox1.Text = prod.ToString();
                comboBox1.Items.Add(prod);
            }
            else
                MessageBox.Show("Элемнт не выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                double sum;
                if (textBox2.Text == null || textBox2.Text == "")
                    sum = 0;
                else
                    sum = Convert.ToDouble(textBox2.Text);
                listBox1.Items.Add(comboBox1.SelectedItem);
                sum += ((Product)comboBox1.SelectedItem).Price;
                textBox2.Text = sum.ToString();
            }
            else
                MessageBox.Show("Элемнт не выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                double sum;
                if (textBox2.Text == null || textBox2.Text == "")
                    sum = 0;
                else
                    sum = Convert.ToDouble(textBox2.Text);
                sum -= ((Product)listBox1.SelectedItem).Price;
                textBox2.Text = sum.ToString();
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
                MessageBox.Show("Элемнт не выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}