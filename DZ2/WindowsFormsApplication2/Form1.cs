using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace WindowsFormsApplication2
{   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            textBox4.KeyPress += TextBox4_KeyPress;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FilterIndex == 1)
            {
                using (FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Truncate, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(file);
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string str = listBox1.Items[i].ToString();
                        sw.WriteLine(str);
                        sw.Flush();
                    }
                    sw.Close();
                }
            }
            if(saveFileDialog1.FilterIndex == 2)
            {
                using (FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Truncate, FileAccess.Write))
                {
                    XmlTextWriter writer = new XmlTextWriter(file, Encoding.Default);
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Persons");
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        string str = listBox1.Items[i].ToString();
                        string[] temp = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        writer.WriteStartElement("Person");
                        writer.WriteElementString("firstname", temp[0]);
                        writer.WriteElementString("secondname", temp[1]);
                        writer.WriteElementString("e-mail", temp[2]);
                        writer.WriteElementString("phone", temp[3]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Close();
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FilterIndex == 1)
            {
                using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(file);
                    listBox1.Items.Clear();
                    while (!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();
                        listBox1.Items.Add(str);
                    }
                    sr.Close();
                }
            }
            if (openFileDialog1.FilterIndex == 2)
            {
                using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    string[] temp = new string[4];
                    XPathDocument doc = new XPathDocument(file);
                    XPathNavigator nav = doc.CreateNavigator();
                    XPathNodeIterator iterator = nav.Select("//Persons/Person/*");
                    listBox1.Items.Clear();
                    while (iterator.MoveNext())
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            temp[i] = iterator.Current.Value;
                            if (i != 3)
                                iterator.MoveNext();
                        }
                        listBox1.Items.Add($"{temp[0]} {temp[1]} {temp[2]} {temp[3]}");
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (textBox1.Text != null && textBox1.Text != "" &&
                    textBox2.Text != null && textBox2.Text != "" &&
                    textBox3.Text != null && textBox3.Text != "" &&
                    textBox4.Text != null && textBox4.Text != "")
                    listBox1.Items[listBox1.SelectedIndex] = ($"{textBox1.Text} {textBox2.Text} {textBox3.Text} {textBox4.Text}");
                else
                    MessageBox.Show("Заполните все поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                e.Handled = true;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string str = listBox1.SelectedItem.ToString();
                string[] temp = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length == 4)
                {
                    textBox1.Text = temp[0];
                    textBox2.Text = temp[1];
                    textBox3.Text = temp[2];
                    textBox4.Text = temp[3];
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != "" &&
                textBox2.Text != null && textBox2.Text != "" &&
                textBox3.Text != null && textBox3.Text != "" &&
                textBox4.Text != null && textBox4.Text != "")
                listBox1.Items.Add($"{textBox1.Text} {textBox2.Text} {textBox3.Text} {textBox4.Text}");
            else
                MessageBox.Show("Заполните все поля", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}