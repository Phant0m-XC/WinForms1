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
    public partial class Form5 : Form
    {
        private ListView listView;
        public Form5(ref ListView listView)
        {
            InitializeComponent();
            this.listView = listView;
            using (ProductListEntities db = new ProductListEntities())
            {
                int index = 0;
                var list = db.Attributes.ToList();
                foreach (var element in list)
                {
                    listView1.Items.Add(new ListViewItem(element.Name));
                    listView1.Items[index].SubItems.Add("");
                    listView1.Items[index++].Tag = element.IdAttribute;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listView1.SelectedItems)
            {
                listView1.Items.Remove((ListViewItem)item);
                listView.Items.Add((ListViewItem)item);
            }
            this.Close();
        }
    }
}
