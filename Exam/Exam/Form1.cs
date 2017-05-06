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
                    listView1.Items[index++].SubItems.Add(element.Price.ToString());
                }
            }
        }
    }
}
