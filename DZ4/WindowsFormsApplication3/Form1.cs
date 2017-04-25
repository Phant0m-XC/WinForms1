using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        ToolStripMenuItem menu;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TopLevelMenu.Text != null && TopLevelMenu.Text != "")
            {
                menu = new ToolStripMenuItem(TopLevelMenu.Text);
                menuStrip1.Items.Add(menu);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SubItem.Text != null && SubItem.Text != "")
                if (TopLevelMenu.Text != null && TopLevelMenu.Text != "")
                {
                    int i;
                    for(i = 0; i < menuStrip1.Items.Count; i++)
                    {
                        if (menuStrip1.Items[i].Text == TopLevelMenu.Text)
                            break;
                    }
                    menu = (ToolStripMenuItem) menuStrip1.Items[i];
                    menu.DropDownItems.Add(new ToolStripMenuItem(SubItem.Text));
                }
        }
    }
}