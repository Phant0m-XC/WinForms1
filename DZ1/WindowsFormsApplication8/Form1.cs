using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        private double[] priceOfOil;
        public Form1()
        {
            InitializeComponent();
            priceOfOil = new double[] { 1.13, 1.21, 1.25, 1.32, 0.64 };
            comboBox1.SelectedIndex = 0;
            textBox4.Text = "1,2";
            textBox5.Text = "2,5";
            textBox6.Text = "1,9";
            textBox7.Text = "0,9";
            textBox1.Text = priceOfOil[comboBox1.SelectedIndex].ToString();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            textBox3.TextChanged += TextBox3_TextChanged;
            textBox3.KeyPress += TextBox_KeyPress;
            textBox2.TextChanged += TextBox2_TextChanged;
            textBox2.KeyPress += TextBox_KeyPress;
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
            checkBox4.CheckedChanged += CheckBox_CheckedChanged;
            textBox11.KeyPress += TextBoxWithoutPoint_KeyPress;
            textBox10.KeyPress += TextBoxWithoutPoint_KeyPress;
            textBox9.KeyPress += TextBoxWithoutPoint_KeyPress;
            textBox8.KeyPress += TextBoxWithoutPoint_KeyPress;
            textBox11.TextChanged += CheckBox_CheckedChanged;
            textBox10.TextChanged += CheckBox_CheckedChanged;
            textBox9.TextChanged += CheckBox_CheckedChanged;
            textBox8.TextChanged += CheckBox_CheckedChanged;
            button1.Click += Button1_Click;
            timer1.Tick += Timer1_Tick;
        }

        private void resetForm()
        {
            if (radioButton2.Checked)
                textBox2.Text = "";
            if (radioButton1.Checked)
                textBox3.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            label12.Text = "";
            if (checkBox1.Checked)
                checkBox1.Checked = false;
            if (checkBox2.Checked)
                checkBox2.Checked = false;
            if (checkBox3.Checked)
                checkBox3.Checked = false;
            if (checkBox4.Checked)
                checkBox4.Checked = false;
            timer1.Stop();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Расчёт закончен?", "Рассчёт", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                label14.Text = (Convert.ToDouble(label7.Text) + Convert.ToDouble(label10.Text)).ToString();
                resetForm();
            }
            else
                timer1.Stop();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label12.Text = (Convert.ToDouble(label7.Text) + Convert.ToDouble(label10.Text)).ToString();
                timer1.Start();
            }
            else
            {
                label12.Text = (Convert.ToDouble(textBox2.Text) + Convert.ToDouble(label10.Text)).ToString();
                timer1.Start();
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            double sum = 0;
            if (checkBox1.Checked)
            {
                if (textBox11.Text != null && textBox11.Text != "")
                    sum += Convert.ToDouble(textBox4.Text) * Convert.ToDouble(textBox11.Text);
            }
            else
            {
                textBox11.Text = "";
                sum += 0;
            }
            ////
            if (checkBox2.Checked)
            {
                if (textBox10.Text != null && textBox10.Text != "")
                    sum += Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox10.Text);
            }
            else
            {
                textBox10.Text = "";
                sum += 0;
            }
            ////
            if (checkBox3.Checked)
            {
                if (textBox9.Text != null && textBox9.Text != "")
                    sum += Convert.ToDouble(textBox6.Text) * Convert.ToDouble(textBox9.Text);
            }
            else
            {
                textBox9.Text = "";
                sum += 0;
            }
            ////
            if (checkBox4.Checked)
            {
                if (textBox8.Text != null && textBox8.Text != "")
                    sum += Convert.ToDouble(textBox7.Text) * Convert.ToDouble(textBox8.Text);
            }
            else
            {
                textBox8.Text = "";
                sum += 0;
            }
            ////
            label10.Text = sum.ToString();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(44))
                e.Handled = true;
        }

        private void TextBoxWithoutPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                e.Handled = true;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != null && textBox2.Text.CompareTo("") != 0)
            {
                double result = Convert.ToDouble(textBox2.Text) / Convert.ToDouble(textBox1.Text);
                label7.Text = result.ToString();
            }
            else
                label7.Text = "0";
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != null && textBox3.Text.CompareTo("") != 0)
            {
                double result = Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox3.Text);
                label7.Text = result.ToString();
            }
            else
                label7.Text = "0";
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox3.Text = "";
                textBox3.ReadOnly = true;
                textBox2.ReadOnly = false;
                groupBox3.Text = "К выдаче";
                label6.Text = "л.";
            }
            else
            {
                textBox3.ReadOnly = false;
                textBox2.Text = "";
                textBox2.ReadOnly = true;
                groupBox3.Text = "К оплате";
                label6.Text = "руб.";
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = priceOfOil[comboBox1.SelectedIndex].ToString();
            if (radioButton1.Checked)
                TextBox3_TextChanged(null, null);
            else if (radioButton2.Checked)
                TextBox2_TextChanged(null, null);
        }
    }
}
