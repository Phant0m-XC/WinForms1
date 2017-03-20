using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        private int x;
        private int y;
        private int count;
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseClick += Form1_MouseClick;
            this.count = 0;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                for(int i = 0; i < count; i++)
                {
                    if(this.Controls.ContainsKey("button" + i.ToString()))
                        this.Controls.RemoveByKey("button" + i.ToString());
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (Math.Abs(x - e.X) >= 10 && Math.Abs(y - e.Y) >= 10)
                {
                    System.Windows.Forms.Button newForm = new System.Windows.Forms.Button();
                    newForm.Name = "button" + count.ToString();
                    newForm.Text = count.ToString();
                    newForm.Location = new Point(x, y);
                    newForm.Size = new Size(Math.Abs(x - e.X), Math.Abs(y - e.Y));
                    this.Controls.Add(newForm);
                    count++;
                }
                else
                    MessageBox.Show("Выделенная область меньше 10x10", "Малый размер статика", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                x = e.X;
                y = e.Y;
            }
        }
    }
}
