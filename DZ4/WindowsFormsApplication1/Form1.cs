using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shell32;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            [DllImport("Kernel32.dll")]
        public extern static IntPtr LoadLibrary(string libName);
        [DllImport("User32.dll")]
        public extern static IntPtr LoadIcon(IntPtr libHandle, int lpIconName);
        IntPtr lib = LoadLibrary("shell32.dll");
            InitializeComponent();
    }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }
}
}