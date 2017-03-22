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
            this.count = 0;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (Math.Abs(x - e.X) >= 10 && Math.Abs(y - e.Y) >= 10)
                {
                    System.Windows.Forms.GroupBox newForm = new System.Windows.Forms.GroupBox();

                    if ((e.X - x) > 0 && (e.Y - y) > 0)
                        newForm.Location = new Point(x, y);
                    else if ((e.X - x) > 0 && (e.Y - y) < 0)
                        newForm.Location = new Point(x, e.Y);
                    else if ((e.X - x) < 0 && (e.Y - y) > 0)
                        newForm.Location = new Point(e.X, y);
                    else if ((e.X - x) < 0 && (e.Y - y) < 0)
                        newForm.Location = new Point(e.X, e.Y);

                    newForm.Name = count.ToString();
                    newForm.Text = count.ToString();
                    newForm.Size = new Size(Math.Abs(x - e.X), Math.Abs(y - e.Y));
                    //newForm.MouseDoubleClick += this.NewForm_MouseDoubleClick;
                    this.Controls.Add(newForm);
                    count++;
                }
                else
                    MessageBox.Show("Выделенная область меньше 10x10", "Малый размер статика", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void NewForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GroupBox temp = (GroupBox)sender;
            if(e.Button == MouseButtons.Left)
                temp.Dispose();
            if(e.Button == MouseButtons.Right)
            {
                string nameControl = "\0";
                for(int i = 0; i < Controls.Count; i++)
                    if (e.X >= Controls[i].Location.X &&
                        e.Y >= Controls[i].Location.Y &&
                        e.X <= Controls[i].Location.X + Controls[i].Size.Width &&
                        e.Y <= Controls[i].Location.Y + Controls[i].Size.Height)
                        if (Controls[i].Text.CompareTo(nameControl) > 0)
                            temp = (GroupBox)Controls[i];
                Text = "Площадь - " + (temp.Size.Height * temp.Size.Width) + " Координаты - " + temp.Location;
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
