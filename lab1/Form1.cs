using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace lab1
{
    public partial class Compiler : Form
    {
        private readonly FileWorks _fileWorks;
        private readonly Correction _correction;
        private readonly Reference _reference;

        private readonly string _helpPath = "E:\\6 �������\\�����\\lab3\\lab1\\Help.html";
        private readonly string _aboutPath = "E:\\6 �������\\�����\\lab3\\lab1\\About.html";

        public Compiler()
        {
            InitializeComponent();
            _fileWorks = new FileWorks(this);
            _correction = new Correction(richTextBox1);
            _reference = new Reference(_helpPath, _aboutPath);
            richTextBox1.TextChanged += richTextBox1_TextChanged;

            this.FormClosing += Compiler_FormClosing;

            this.MinimumSize = new Size(600, 400);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            _fileWorks.MarkAsModified();
        }

        public RichTextBox Editor => richTextBox1;

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _fileWorks.CreateNewFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _fileWorks.OpenFile();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveFile();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _correction.Undo();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            _correction.Redo();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            _correction.Copy();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            _correction.Cut();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            _correction.Paste();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            var lexer = new Lexer();
            int errorCount;
            var tokens = lexer.Analyze(richTextBox1.Text, out errorCount);

            dataGridView1.Rows.Clear();

            foreach (var token in tokens)
            {
                dataGridView1.Rows.Add(token.Code, token.Type, token.Lexeme, token.Position);
            }

            if (errorCount > 0)
            {
                MessageBox.Show($"������ �������� � ��������. ����� ������: {errorCount}", "������ � ������������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("������ ������������� ��� ������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            _reference.ShowHelp();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            _reference.ShowAbout();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.CreateNewFile();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.OpenFile();
        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveFile();
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.SaveAsFile();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fileWorks.Exit();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Undo();
        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Redo();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Cut();
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Copy();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Paste();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.Delete();
        }

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _correction.SelectAll();
        }

        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reference.ShowHelp();
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _reference.ShowAbout();
        }

        public void ChangeFontSize(float newSize)
        {
            if (newSize < 8 || newSize > 72) return;

            Editor.Font = new Font(Editor.Font.FontFamily, newSize, Editor.Font.Style);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Compiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ���� CheckUnsavedChanges() ���������� true, ������ ������������ ����� "Cancel"
            if (_fileWorks.CheckUnsavedChanges())
            {
                e.Cancel = true; // �������� �������� ����
            }
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lexer = new Lexer();
            int errorCount;
            var tokens = lexer.Analyze(richTextBox1.Text, out errorCount);

            dataGridView1.Rows.Clear();

            foreach (var token in tokens)
            {
                dataGridView1.Rows.Add(token.Code, token.Type, token.Lexeme, token.Position);
            }

            if (errorCount > 0)
            {
                MessageBox.Show($"������ �������� � ��������. ����� ������: {errorCount}", "������ � ������������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("������ ������������� ��� ������.", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}