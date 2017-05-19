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
    public partial class Form2 : Form
    {
        private string[] str;
        public Form2(string[] str)
        {
            InitializeComponent();
            this.str = str;
            textBox1.Text = this.str[0];
            textBox2.Text = this.str[1];
            textBox3.Text = this.str[2];
            loadAttribute();
            buttonSave.Enabled = false;
            textBox2.KeyPress += TextBox2_KeyPress;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.str[0] = textBox1.Text;
            this.str[1] = textBox2.Text;
            this.str[2] = textBox3.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(ref listView1);
            form5.ShowDialog();
            using (ProductListEntities db = new ProductListEntities())
            {
                foreach (var item in listView1.Items)
                {
                    Set set = new Set();
                    set.IdProduct = Convert.ToInt32(str[3]);
                    set.IdAttribute = (int)((ListViewItem)item).Tag;
                    set.Value = "";
                    db.Set.Add(set);
                }
                db.SaveChanges();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                if (listView1.SelectedItems[0].SubItems[1].Text != null && listView1.SelectedItems[0].SubItems[1].Text != "")
                    textBox4.Text = listView1.SelectedItems[0].SubItems[1].Text;
                else
                    textBox4.Text = "";
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].SubItems[1].Text = textBox4.Text;
                using (ProductListEntities db = new ProductListEntities())
                {
                    int idProd = Convert.ToInt32(str[3]);
                    int idAttrib = (int)listView1.SelectedItems[0].Tag;
                    var result = from s in db.Set
                                 join a in db.Attributes on s.IdAttribute equals a.IdAttribute
                                 join p in db.Product on s.IdProduct equals p.IdProduct
                                 where s.IdProduct == idProd && s.IdAttribute == idAttrib
                                 select s;
                    foreach (var item in result)
                    {
                        item.Value = textBox4.Text;
                    }
                    db.SaveChanges();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                using (ProductListEntities db = new ProductListEntities())
                {
                    int idProd = Convert.ToInt32(str[3]);
                    int idAttrib = (int)listView1.SelectedItems[0].Tag;
                    var result = from s in db.Set
                                 join a in db.Attributes on s.IdAttribute equals a.IdAttribute
                                 join p in db.Product on s.IdProduct equals p.IdProduct
                                 where s.IdProduct == idProd && s.IdAttribute == idAttrib
                                 select s;
                    foreach (var item in result)
                    {
                        db.Set.Remove(item);
                    }
                    db.SaveChanges();
                }
            }
            loadAttribute();
        }

        private void loadAttribute()
        {
            listView1.Items.Clear();
            int id = Convert.ToInt32(str[3]);
            using (ProductListEntities db = new ProductListEntities())
            {
                var result = from s in db.Set
                             join a in db.Attributes on s.IdAttribute equals a.IdAttribute
                             join p in db.Product on s.IdProduct equals p.IdProduct
                             where p.IdProduct == id
                             select new { Name = a.Name, Value = s.Value, IdAttr = a.IdAttribute };
                int index = 0;
                foreach (var item in result)
                {
                    listView1.Items.Add(item.Name);
                    listView1.Items[index].Tag = item.IdAttr;
                    listView1.Items[index++].SubItems.Add(item.Value);
                }
            }
        }
    }
}