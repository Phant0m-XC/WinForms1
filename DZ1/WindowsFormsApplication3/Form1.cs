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
        private int oldX;
        private int oldY;
        private bool isControlPress;
        public Form1()
        {
            InitializeComponent();
            MouseMove += Form1_MouseMove;
            MouseClick += Form1_MouseClick;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            oldX = 0;
            oldY = 0;
            isControlPress = false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.Control)
                isControlPress = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                isControlPress = true;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                Text = $"Параметры окна: ширина - {ClientSize.Width}, высота - {ClientSize.Height}";
            if (e.Button == MouseButtons.Left)
            {
                if (isControlPress)
                    Close();
                else if (e.X < 10 || e.Y < 10 || e.X > ClientSize.Width - 10 || e.Y > ClientSize.Height - 10)
                    Text = "Клик с внешней стороны";
                else if (e.X == 10 || e.Y == 10 || e.X == ClientSize.Width - 10 || e.Y == ClientSize.Height - 10)
                    Text = "Клик на границе";
                else if (e.X > 10 || e.Y > 10 || e.X < ClientSize.Width - 10 || e.Y < ClientSize.Height - 10)
                    Text = "Клик внутри границы";
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X != oldX || e.Y != oldY)
                Text = $"Координаты мыши X - {e.X}, Y - {e.Y}";
            oldX = e.X;
            oldY = e.Y;
        }
    }
}