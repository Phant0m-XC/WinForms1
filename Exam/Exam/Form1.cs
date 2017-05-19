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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            using(ProductListEntities db = new ProductListEntities())
            {
                int index = 0;
                var list = db.Product.ToList();
                foreach(var element in list)
                {
                    listView1.Items.Add(new ListViewItem(element.Name));
                    listView1.Items[index].Tag = element.IdProduct;
                    listView1.Items[index++].SubItems.Add(element.Price.ToString());
                }
            }
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result;
            string[] str = new string[4];
            using(ProductListEntities db = new ProductListEntities())
            {
                var product = db.Product.Find(listView1.SelectedItems[0].Tag);
                str[0] = product.Name;
                str[1] = product.Price.ToString();
                str[2] = product.Manufacturer.Name.ToString();
                str[3] = ((int)listView1.SelectedItems[0].Tag).ToString();
            }
            Form2 form2 = new Form2(str);
            result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (ProductListEntities db = new ProductListEntities())
                {
                    var product = db.Product.Find(listView1.SelectedItems[0].Tag);
                    product.Name = str[0];
                    product.Price = Convert.ToInt32(str[1]);
                    product.Manufacturer.Name = str[2];
                    db.SaveChanges();
                    listView1.Items.Clear();
                    int index = 0;
                    var list = db.Product.ToList();
                    foreach (var element in list)
                    {
                        listView1.Items.Add(new ListViewItem(element.Name));
                        listView1.Items[index].Tag = element.IdProduct;
                        listView1.Items[index++].SubItems.Add(element.Price.ToString());
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
