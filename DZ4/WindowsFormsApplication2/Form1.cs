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
        private enum CutCopyStatus { CUT, COPY, NOCHOOSE };
        private TreeNode tempTreeElement = null;
        private ListViewItem tempListItem = null;
        [DllImport(@"C:\Windows\System32\Kernel32.dll")]
        public extern static IntPtr LoadLibrary(string libName);
        [DllImport(@"C:\Windows\System32\User32.dll")]
        public extern static IntPtr LoadIcon(IntPtr libHandle, int lpIconName);
        IntPtr lib = LoadLibrary(@"C:\Windows\System32\shell32.dll");
        private string[] cutCopyList;
        private CutCopyStatus state = CutCopyStatus.NOCHOOSE;
        private List<string> listExtensions;

        public Form1()
        {
            InitializeComponent();
            listExtensions = new List<string>();
            listExtensions.AddRange(new string[] { ".jpg", ".jpeg", ".bmp" });
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 9))); //disk 0
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 4))); //folder 1
            imageList1.Images.Add(Icon.FromHandle(LoadIcon(LoadLibrary(@"C:\Windows\System32\shell32.dll"), 5))); //folder_open 2
            treeView1.ImageList = imageList1;
            listView1.SmallImageList = imageList1;
            listView1.LargeImageList = imageList1;
            listView1.SmallImageList.ImageSize = new Size(30, 30);
            listView1.LargeImageList.ImageSize = new Size(30, 30);
            treeView1.ImageList.ImageSize = new Size(30, 30);
            loadNewTree();
            listView1.View = View.Tile;
            button2.Image = imageList1.Images[2];
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.MouseClick += ListView1_MouseClick;
            comboBox1.SelectedIndex = 0;
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new Point(this.Location.X + listView1.Location.X + e.Location.X,
                    this.Location.Y + listView1.Location.Y + e.Location.Y);
                contextMenuStrip1.Show(point);
            }
            else if (e.Button == MouseButtons.Left)
            {
                var senderList = (ListView)sender;
                var clickedItem = senderList.HitTest(e.Location).Item;
                toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
                FileInfo info = new FileInfo(((string)clickedItem.Tag).ToString());
                if (File.Exists(((string)clickedItem.Tag).ToString()))
                {
                    toolStripStatusLabel4.Text = string.Format($"{Path.GetExtension(((string)clickedItem.Tag).ToString())} - файл");
                    
                    toolStripStatusLabel6.Text = info.Length.ToString();
                }
                else if (Directory.Exists(((string)clickedItem.Tag).ToString()))
                {
                    toolStripStatusLabel4.Text = "Папка";
                    toolStripStatusLabel6.Text = "0";
                }
                toolStripStatusLabel8.Text = Directory.GetCreationTime(info.FullName).ToString();
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DirectoryInfo info;
            try
            {
                var senderList = (ListView)sender;
                var clickedItem = senderList.HitTest(e.Location).Item;
                if (Directory.Exists(((string)clickedItem.Tag).ToString()))
                {
                    if (clickedItem.Tag != null && ((string)clickedItem.Tag).ToString() != "")
                        textBox1.Text = ((string)clickedItem.Tag).ToString();
                    if (clickedItem != null)
                    {
                        loadStartIcon();
                        int index = 3, elementNum = 0;
                        string[] subDir = Directory.GetDirectories((string)clickedItem.Tag);
                        for (int i = 0; i < subDir.Length; i++)
                        {
                            info = new DirectoryInfo(subDir[i]);
                            tempListItem = new ListViewItem(info.Name, 1);
                            tempListItem.Tag = subDir[i];
                            listView1.Items.Add(tempListItem);
                            listView1.Items[elementNum].SubItems.Add("");
                            listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(subDir[i])).ToString());
                        }
                        string[] files = Directory.GetFiles((string)clickedItem.Tag);
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (listExtensions.Contains(Path.GetExtension(files[i])))
                            {
                                Image image = Image.FromFile(files[i]);
                                imageList1.Images.Add(image);
                            }
                            else
                                imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                            tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                            tempListItem.Tag = files[i];
                            listView1.Items.Add(tempListItem);
                            FileInfo inf = new FileInfo(files[i]);
                            listView1.Items[elementNum].SubItems.Add(inf.Length.ToString());
                            listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(files[i])).ToString());
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
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
                int index = 3, elementNum = 0;
                if (Directory.Exists((string)treeView1.SelectedNode.Tag))
                {
                    textBox1.Text = ((string)treeView1.SelectedNode.Tag).ToString();
                    string[] subDir = Directory.GetDirectories((string)treeView1.SelectedNode.Tag);
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        info = new DirectoryInfo(subDir[i]);
                        tempListItem = new ListViewItem(info.Name, 1);
                        tempListItem.Tag = subDir[i];
                        listView1.Items.Add(tempListItem);
                        listView1.Items[elementNum].SubItems.Add("");
                        listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(subDir[i])).ToString());
                        tempTreeElement = new TreeNode(info.Name, 1, 1);
                        tempTreeElement.Tag = subDir[i];
                        for (int j = 0; j <= treeView1.SelectedNode.Nodes.Count; j++)
                        {
                            if (j == treeView1.SelectedNode.Nodes.Count)
                            {
                                treeView1.SelectedNode.Nodes.Add(tempTreeElement);
                                break;
                            }
                            if (((string)treeView1.SelectedNode.Nodes[j].Tag).CompareTo(subDir[i]) == 0)
                                break;
                        }
                        treeView1.SelectedNode.Expand();
                    }
                    string[] files = Directory.GetFiles((string)treeView1.SelectedNode.Tag);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (listExtensions.Contains(Path.GetExtension(files[i])))
                        {
                            Image image = Image.FromFile(files[i]);
                            imageList1.Images.Add(image);
                        }
                        else
                            imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                        tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                        tempListItem.Tag = files[i];
                        listView1.Items.Add(tempListItem);
                        FileInfo inf = new FileInfo(files[i]);
                        listView1.Items[elementNum].SubItems.Add(inf.Length.ToString());
                        listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(files[i])).ToString());
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
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
                int index = 3, elementNum = 0;
                if (Directory.Exists(textBox1.Text))
                {
                    string[] subDir = Directory.GetDirectories(textBox1.Text);
                    for (int i = 0; i < subDir.Length; i++)
                    {
                        info = new DirectoryInfo(subDir[i]);
                        tempListItem = new ListViewItem(info.Name, 1);
                        tempListItem.Tag = subDir[i];
                        listView1.Items.Add(tempListItem);
                        listView1.Items[elementNum].SubItems.Add("");
                        listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(subDir[i])).ToString());
                    }
                    string[] files = Directory.GetFiles(textBox1.Text);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (listExtensions.Contains(Path.GetExtension(files[i])))
                        {
                            Image image = Image.FromFile(files[i]);
                            imageList1.Images.Add(image);
                        }
                        else
                            imageList1.Images.Add(Icon.ExtractAssociatedIcon(files[i]));
                        tempListItem = new ListViewItem(Path.GetFileName(files[i]), index++);
                        tempListItem.Tag = files[i];
                        listView1.Items.Add(tempListItem);
                        FileInfo inf = new FileInfo(files[i]);
                        listView1.Items[elementNum].SubItems.Add(inf.Length.ToString());
                        listView1.Items[elementNum++].SubItems.Add((Directory.GetCreationTime(files[i])).ToString());
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
            switch (comboBox1.SelectedIndex)
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

        private void loadNewTree()
        {
            treeView1.Nodes.Clear();
            string[] logDrive = Directory.GetLogicalDrives();
            for (int i = 0; i < logDrive.Length; i++)
            {
                tempTreeElement = new TreeNode(logDrive[i], 0, 0);
                tempTreeElement.Tag = logDrive[i];
                treeView1.Nodes.Add(tempTreeElement);
            }
        }

        private void cutFunction()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                cutCopyList = new string[listView1.SelectedItems.Count];
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    cutCopyList[i] = ((string)listView1.SelectedItems[i].Tag).ToString();
                state = CutCopyStatus.CUT;
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutFunction();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutFunction();
        }

        private void toolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            cutFunction();
        }

        private void copyFunction()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                cutCopyList = new string[listView1.SelectedItems.Count];
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    cutCopyList[i] = ((string)listView1.SelectedItems[i].Tag).ToString();
                state = CutCopyStatus.COPY;
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyFunction();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyFunction();
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            copyFunction();
        }

        private void pasteFunction()
        {
            DirectoryInfo infoDir;
            FileInfo infoFile;
            switch (state)
            {
                case CutCopyStatus.CUT:
                    for (int i = 0; i < cutCopyList.Length; i++)
                        if (Directory.Exists(cutCopyList[i]))
                        {
                            infoDir = new DirectoryInfo(cutCopyList[i]);
                            Directory.Move(cutCopyList[i], Path.Combine(textBox1.Text, infoDir.Name));
                        }
                        else if (File.Exists(cutCopyList[i]))
                        {
                            infoFile = new FileInfo(cutCopyList[i]);
                            File.Move(cutCopyList[i], Path.Combine(textBox1.Text, infoFile.Name));
                        }
                    break;
                case CutCopyStatus.COPY:
                    for (int i = 0; i < cutCopyList.Length; i++)
                        copy(cutCopyList[i], textBox1.Text);
                    break;
            }
            state = CutCopyStatus.NOCHOOSE;
            loadNewTree();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteFunction();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteFunction();
        }

        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            pasteFunction();
        }

        private void copy(string path, string prevPath)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo info = Directory.CreateDirectory(Path.Combine(prevPath, Path.GetFileName(path)));
                string[] subDir = Directory.GetDirectories(path);
                for (int i = 0; i < subDir.Length; i++)
                    copy(subDir[i], info.FullName);
                subDir = Directory.GetFiles(path);
                DialogResult result;
                for (int i = 0; i < subDir.Length; i++)
                {
                    if (File.Exists(Path.Combine(info.FullName, Path.GetFileName(subDir[i]))))
                    {
                        result = MessageBox.Show($"{Path.Combine(info.FullName, Path.GetFileName(subDir[i]))} существует\nПерезаписать?",
                            "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes)
                            File.Copy(subDir[i], Path.Combine(info.FullName, Path.GetFileName(subDir[i])), true);
                        else if (result == DialogResult.No)
                            File.Copy(subDir[i], Path.Combine(info.FullName, Path.GetFileName(subDir[i])), false);
                    }
                    else
                        File.Copy(subDir[i], Path.Combine(info.FullName, Path.GetFileName(subDir[i])), false);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
