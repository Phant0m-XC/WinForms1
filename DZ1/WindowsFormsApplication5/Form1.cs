using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MouseMove += Form1_MouseMove;
            button1.MouseMove += Button1_MouseMove;
        }

        private void Button1_MouseMove(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            Controls[0].Location = new Point(random.Next(0, ClientSize.Width - Controls[0].Size.Width),
                    random.Next(0, ClientSize.Height - Controls[0].Size.Height));
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            if ((e.X > Controls[0].Location.X - 10) &&
                (e.X < Controls[0].Location.X + Controls[0].Size.Width + 10) &&
                (e.Y > Controls[0].Location.Y - 10) &&
                (e.Y < Controls[0].Location.Y + Controls[0].Size.Height + 10))
                Controls[0].Location = new Point(random.Next(0, ClientSize.Width - Controls[0].Size.Width),
                    random.Next(0, ClientSize.Height - Controls[0].Size.Height));
        }
    }
}
