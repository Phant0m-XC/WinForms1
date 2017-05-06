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
            string[] str = new string[3];
            using(ProductListEntities db = new ProductListEntities())
            {
                var list = db.Product.Find(listView1.SelectedItems[0].Tag);
                str[0] = list.Name;
                str[1] = list.Price.ToString();
                str[2] = list.Manufacturer.Name.ToString();
            }
            Form2 form2 = new Form2(ref str);
            form2.Show();
        }
    }
}
