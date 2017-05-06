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
        private TreeNode tempTreeElement = null;
        private ListViewItem tempListItem = null;
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
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 5))); //folder_open 2
            treeView1.ImageList = imageList1;
            listView1.SmallImageList = imageList1;
            listView1.LargeImageList = imageList1;
            string[] logDrive = Directory.GetLogicalDrives();
            for (int i = 0; i < logDrive.Length; i++)
            {
                tempTreeElement = new TreeNode(logDrive[i], 0, 0);
                tempTreeElement.Tag = logDrive[i];
                treeView1.Nodes.Add(tempTreeElement);
            }
            treeView1.ImageList.ImageSize = new Size(30, 30);
            listView1.View = View.Tile;
            button2.Image = imageList1.Images[2];
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            comboBox1.SelectedIndex = 0;
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DirectoryInfo info;
            try {
                var senderList = (ListView)sender;
                var clickedItem = senderList.HitTest(e.Location).Item;
                if (clickedItem != null)
                {
                    loadStartIcon();
                    int index = 3;
                    string[] subDir = Directory.GetDirectories((string)clickedItem.Tag);
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        info = new DirectoryInfo(subDir[i]);
                        tempListItem = new ListViewItem(info.Name, 1);
                        tempListItem.Tag = subDir[i];
                        listView1.Items.Add(tempListItem);
                    }
                    string[] files = Directory.GetFiles((string)clickedItem.Tag);
                    for (int i = 0; i < files.Length; i++)
                    {
                        imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                        tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                        tempListItem.Tag = files[i];
                        listView1.Items.Add(tempListItem);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DirectoryInfo info;
            try
            {
                loadStartIcon();
                int index = 3;
                if (Directory.Exists((string)treeView1.SelectedNode.Tag))
                {
                    string[] subDir = Directory.GetDirectories((string)treeView1.SelectedNode.Tag);
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        info = new DirectoryInfo(subDir[i]);
                        tempListItem = new ListViewItem(info.Name, 1);
                        tempListItem.Tag = subDir[i];
                        listView1.Items.Add(tempListItem);
                        tempTreeElement = new TreeNode(info.Name, 1, 1);
                        tempTreeElement.Tag = subDir[i];
                        for(int j = 0; j <= treeView1.SelectedNode.Nodes.Count; j++)
                        {
                            if (j == treeView1.SelectedNode.Nodes.Count)
                            {
                                treeView1.SelectedNode.Nodes.Add(tempTreeElement);
                                break;
                            }
                            if(((string)treeView1.SelectedNode.Nodes[j].Tag).CompareTo(subDir[i]) == 0)
                                break;
                        }
                        treeView1.SelectedNode.Expand();
                    }
                    string[] files = Directory.GetFiles((string)treeView1.SelectedNode.Tag);
                    for (int i = 0; i < files.Length; i++)
                    {
                        imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                        tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                        tempListItem.Tag = files[i];
                        listView1.Items.Add(tempListItem);
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo info;
            try
            {
                loadStartIcon();
                int index = 3;
                if (Directory.Exists(textBox1.Text))
                {
                    string[] subDir = Directory.GetDirectories(textBox1.Text);
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        info = new DirectoryInfo(subDir[i]);
                        tempListItem = new ListViewItem(info.Name, 1);
                        tempListItem.Tag = subDir[i];
                        listView1.Items.Add(tempListItem);
                    }
                    string[] files = Directory.GetFiles(textBox1.Text);
                    for (int i = 0; i < files.Length; i++)
                    {
                        imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                        tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                        tempListItem.Tag = files[i];
                        listView1.Items.Add(tempListItem);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox temp = (ComboBox)sender;
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    listView1.View = View.Tile;
                    break;
                case 1:
                    listView1.View = View.LargeIcon;
                    break;
                case 2:
                    listView1.View = View.SmallIcon;
                    break;
                case 3:
                    listView1.View = View.Details;
                    break;
                case 4:
                    listView1.View = View.List;
                    break;
            }
        }
        private void loadStartIcon()
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 9))); //0 - disk
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 4))); //1 - folder
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 5))); //2 - folder_open
        }
    }
}
