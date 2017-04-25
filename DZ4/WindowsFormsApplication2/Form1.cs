using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Windows\System32\Kernel32.dll")]
        public extern static IntPtr LoadLibrary(string libName);
        [DllImport(@"C:\Windows\System32\User32.dll")]
        public extern static IntPtr LoadIcon(IntPtr libHandle, int lpIconName);
        IntPtr lib = LoadLibrary(@"C:\Windows\System32\shell32.dll");

        public Form1()
        {
            InitializeComponent();
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 9))); //disk 0
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 4))); //folder 1
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 3))); //file 2
            treeView1.ImageList = imageList1;
            listView1.LargeImageList = imageList1;
            string[] logDrive = Directory.GetLogicalDrives();
            for (int i = 0; i < logDrive.Length; i++)
                treeView1.Nodes.Add(new TreeNode(logDrive[i], 0, 0));
            listView1.LargeImageList = imageList1;
            listView1.LargeImageList.ImageSize = new Size(30, 30);
            listView1.View = View.LargeIcon;

            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = (ListView)sender;
            var clickedItem = senderList.HitTest(e.Location).Item;
            if (clickedItem != null)
            {
                listView1.Items.Clear();
                string[] subDir = Directory.GetDirectories(clickedItem.Text);
                for (int i = 0; i < subDir.Length; i++)
                    listView1.Items.Add(new ListViewItem(subDir[i], 1));
                string[] files = Directory.GetFiles(clickedItem.Text);
                for (int i = 0; i < files.Length; i++)
                    listView1.Items.Add(new ListViewItem(files[i], 2));
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Regex reg = new Regex("[\\s\\w\\d]*\\.[\\w\\s\\d]*$|[\\s\\w\\d]*$");
            //Match match;
            listView1.Items.Clear();
            if (Directory.Exists(treeView1.SelectedNode.Text))
            {
                string[] subDir = Directory.GetDirectories(treeView1.SelectedNode.Text);
                for (int i = 0; i < subDir.Length; i++)
                {
                    //match = reg.Match(subDir[i]);
                    listView1.Items.Add(new ListViewItem(subDir[i]/*match.Value*/, 1));
                    treeView1.SelectedNode.Nodes.Add(new TreeNode(subDir[i]/*match.Value*/, 1, 1));
                    treeView1.SelectedNode.Expand();
                }
                string[] files = Directory.GetFiles(treeView1.SelectedNode.Text);
                for (int i = 0; i < files.Length; i++)
                {
                    //match = reg.Match(files[i]);
                    listView1.Items.Add(new ListViewItem(files[i]/*match.Value*/, 2));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(textBox1.Text))
            {
                listView1.Items.Clear();
                string[] subDir = Directory.GetDirectories(textBox1.Text);
                for (int i = 0; i < subDir.Length; i++)
                    listView1.Items.Add(new ListViewItem(subDir[i], 1));
                string[] files = Directory.GetFiles(textBox1.Text);
                for (int i = 0; i < files.Length; i++)
                    listView1.Items.Add(new ListViewItem(files[i], 2));
            }
        }
    }
}
