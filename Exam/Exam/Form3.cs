using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            button1.Enabled = false;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            using (ProductListEntities db = new ProductListEntities())
            {
                int index = 0;
                var list = db.Attributes.ToList();
                foreach (var element in list)
                {
                    listView1.Items.Add(new ListViewItem(element.Name));
                    listView1.Items[index++].Tag = element.IdAttribute;
                }
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string str = listView1.SelectedItems[0].Text;
            DialogResult result;
            Form4 form4 = new Form4(ref str);
            result = form4.ShowDialog();
            if (result == DialogResult.OK)
            {
                listView1.SelectedItems[0].Text = str;
                int id = (int)listView1.SelectedItems[0].Tag;
                using (ProductListEntities db = new ProductListEntities())
                {
                    var result2 = from a in db.Attributes
                                  where a.IdAttribute == id
                                  select a;
                    foreach (var item in result2)
                    {
                        item.Name = str;
                    }
                    db.SaveChanges();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != "")
            {
                using (ProductListEntities db = new ProductListEntities())
                {
                    var result = from c in db.Attributes
                                 where c.Name == textBox1.Text
                                 select c;
                    if (result.Count() > 0)
                    {
                        MessageBox.Show($"Атрибут с названием {textBox1.Text} существует", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        listView1.Items.Add(new ListViewItem(textBox1.Text));
                        Attributes attribute = new Attributes();
                        attribute.Name = textBox1.Text;
                        db.Attributes.Add(attribute);
                        db.SaveChanges();
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != "")
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                using (ProductListEntities db = new ProductListEntities())
                {
                    int id = (int)listView1.SelectedItems[0].Tag;
                    var result = from a in db.Attributes
                                 where a.IdAttribute == id
                                 select a;
                    foreach (var item in result)
                    {
                        db.Attributes.Remove(item);
                    }
                    db.SaveChanges();
                    listView1.Items.Clear();
                    int index = 0;
                    var list = db.Attributes.ToList();
                    foreach (var element in list)
                    {
                        listView1.Items.Add(new ListViewItem(element.Name));
                        listView1.Items[index++].Tag = element.IdAttribute;
                    }
                }
        }
    }
}
