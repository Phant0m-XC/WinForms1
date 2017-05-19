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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        FileStream file;
        StreamReader sReader;
        StreamWriter sWriter;
        public Form1()
        {
            file = null;
            sReader = null;
            sWriter = null;
            InitializeComponent();
            this.Text = "NotePad";
            fontDialog1.ShowColor = true;
            richTextBox1.DragEnter += RichTextBox1_DragEnter;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;
            richTextBox1.AllowDrop = true;
            richTextBox1.MouseUp += RichTextBox1_MouseUp;
        }

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new Point(this.Location.X + richTextBox1.Location.X + e.Location.X,
                        this.Location.Y + richTextBox1.Location.Y + e.Location.Y);
                contextMenuStrip1.Show(point);
            }
        }

        private void RichTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
            {
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < objects.Length; i++)
                    if (string.Compare(Path.GetExtension(objects[i]), ".txt") == 0)
                        richTextBox1.Text += File.ReadAllText(objects[i], Encoding.Default) + "\r\n";
                    //else
                        //MessageBox.Show($"Файл {objects[i]} не является файлом .txt", "Внимание", MessageBoxButtons.OK,
                            //MessageBoxIcon.Asterisk);
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = fontDialog1.Font;
            richTextBox1.SelectionColor = fontDialog1.Color;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.OK;
            if (richTextBox1.Text != null || richTextBox1.Text != "")
                result = MessageBox.Show("Введённый текст будет потерян.\nПродолжить?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                richTextBox1.Clear();
                this.Text = "NotePad";
                if (sReader != null)
                    sReader.Close();
                if (sWriter != null)
                    sWriter.Close();
                if (file != null)
                    file.Close();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.OK;
            if (richTextBox1.Text != null || richTextBox1.Text != "")
                result = MessageBox.Show("Введённый текст будет потерян.\nПродолжить?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                richTextBox1.Clear();
                this.Text = "NotePad";
                if (sReader != null)
                    sReader.Close();
                if (sWriter != null)
                    sWriter.Close();
                if (file != null)
                    file.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Введённый текст будет потерян.\nПродолжить?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            if (sReader != null)
                sReader.Close();
            if (file != null)
                file.Close();
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                sReader = new StreamReader(file, Encoding.Default);
                richTextBox1.Text = sReader.ReadToEnd();
                this.Text = openFileDialog1.FileName;
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Введённый текст будет потерян.\nПродолжить?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
                return;
            if (sReader != null)
                sReader.Close();
            if (file != null)
                file.Close();
            openFileDialog1.ShowDialog();
            if (File.Exists(openFileDialog1.FileName))
            {
                file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                sReader = new StreamReader(file, Encoding.Default);
                richTextBox1.Text = sReader.ReadToEnd();
                this.Text = openFileDialog1.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null && file.CanWrite)
            {
                if (file.CanSeek)
                    file.Seek(0, SeekOrigin.Begin);
                file.SetLength(0);
                sWriter = new StreamWriter(file, Encoding.Default);
                sWriter.Write(richTextBox1.Text);
                sWriter.Flush();
                this.Text = (openFileDialog1.FileName != null && openFileDialog1.FileName != "") ? openFileDialog1.FileName : saveFileDialog1.FileName;
            }
            else
            {
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog1.FileName))
                        file = new FileStream(saveFileDialog1.FileName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    else
                        file = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (file.CanSeek)
                        file.Seek(0, SeekOrigin.Begin);
                    sWriter = new StreamWriter(file, Encoding.Default);
                    sWriter.Write(richTextBox1.Text);
                    sWriter.Flush();
                    this.Text = saveFileDialog1.FileName;
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (file != null && file.CanWrite)
            {
                if (file.CanSeek)
                    file.Seek(0, SeekOrigin.Begin);
                file.SetLength(0);
                sWriter = new StreamWriter(file, Encoding.Default);
                sWriter.Write(richTextBox1.Text);
                sWriter.Flush();
                this.Text = (openFileDialog1.FileName != null && openFileDialog1.FileName != "") ? openFileDialog1.FileName : saveFileDialog1.FileName;
            }
            else
            {
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (File.Exists(saveFileDialog1.FileName))
                        file = new FileStream(saveFileDialog1.FileName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    else
                        file = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    if (file.CanSeek)
                        file.Seek(0, SeekOrigin.Begin);
                    sWriter = new StreamWriter(file, Encoding.Default);
                    sWriter.Write(richTextBox1.Text);
                    sWriter.Flush();
                    this.Text = saveFileDialog1.FileName;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sWriter != null)
                sWriter.Close();
            if (file != null)
                file.Close();
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
                if (File.Exists(saveFileDialog1.FileName))
                    file = new FileStream(saveFileDialog1.FileName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                else
                    file = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            if (file != null && file.CanWrite)
            {
                if (file.CanSeek)
                    file.Seek(0, SeekOrigin.Begin);
                sWriter = new StreamWriter(file);
                sWriter.Write(richTextBox1.Text);
                sWriter.Flush();
                this.Text = saveFileDialog1.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
                richTextBox1.Undo();
        }

        private void choiceFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = fontDialog1.Font;
            richTextBox1.SelectionColor = fontDialog1.Color;
        }

        private void colorFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void selectAllВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
                richTextBox1.Undo();
        }

        private void toolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void toolStripMenuItemUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }
    }
}