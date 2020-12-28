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

namespace Project_Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Kayit_Yeri;

        private const int HT_CAPTION = 0x2;
        private const int WM_NCLBUTTONDOWN = 0xA1;

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            // Panel'e ya da Form'a gelen mouse hareketlerini yakalama (Capture = false).
            (sender as Control).Capture = false;
            // Sanki başlık çubuğu (Caption Bar) üzerinde sol mouse butonu tıklaması
            // başlamış gibi yap: 1) önce sahte mesajı oluştur.
            Message msg = Message.Create(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION,

                IntPtr.Zero);
            // 2) Sonra sahte mesajı uygulamanın WndProc() metoduna gönder.
            base.WndProc(ref msg);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button_Copy_Click(object sender, EventArgs e)
        {
            if (TextArea.SelectedText.Length > 0)
            {
                Clipboard.SetText(TextArea.SelectedText);
                TextArea.Focus();
            }
        }

        private void Button_Cut_Click(object sender, EventArgs e)
        {
            if (TextArea.SelectedText.Length > 0)
            {
                Clipboard.SetText(TextArea.SelectedText);
                TextArea.SelectedText = "";
                TextArea.Focus();
            }
        }

        private void Button_Paste_Click(object sender, EventArgs e)
        {
            TextArea.SelectedText = Clipboard.GetText();
            TextArea.Focus();
        }

        private void Button_Bold_Click(object sender, EventArgs e)
        {
            Font Bold = new Font(TextArea.Font, FontStyle.Bold);
            Font Regular = new Font(TextArea.Font, FontStyle.Regular);

            if (TextArea.SelectionFont != Bold)
            {
                TextArea.SelectionFont = Bold;
            }
            else
            {
                TextArea.SelectionFont = Regular;
            }
            TextArea.Focus();
        }

        private void Button_Underline_Click(object sender, EventArgs e)
        {
            Font Underline = new Font(TextArea.Font, FontStyle.Underline);
            Font Regular = new Font(TextArea.Font, FontStyle.Regular);

            if (TextArea.SelectionFont != Underline)
            {
                TextArea.SelectionFont = Underline;
            }
            else
            {
                TextArea.SelectionFont = Regular;
            }
            TextArea.Focus();
        }

        private void Button_Italic_Click(object sender, EventArgs e)
        {
            Font Italic = new Font(TextArea.Font, FontStyle.Italic);
            Font Regular = new Font(TextArea.Font, FontStyle.Regular);

            if (TextArea.SelectionFont != Italic)
            {
                TextArea.SelectionFont = Italic;
            }
            else
            {
                TextArea.SelectionFont = Regular;
            }
            TextArea.Focus();
        }

        private void Button_Strikeout_Click(object sender, EventArgs e)
        {
            Font Strikeout = new Font(TextArea.Font, FontStyle.Strikeout);
            Font Regular = new Font(TextArea.Font, FontStyle.Regular);

            if (TextArea.SelectionFont != Strikeout)
            {
                TextArea.SelectionFont = Strikeout;
            }
            else
            {
                TextArea.SelectionFont = Regular;
            }
            TextArea.Focus();
        }

        private void Button_Increase_Click(object sender, EventArgs e)
        {
            float Font_Size = TextArea.SelectionFont.Size + 2;
            TextArea.SelectionFont = new Font(TextArea.SelectionFont.FontFamily, Font_Size, TextArea.SelectionFont.Style);
            TextArea.Focus();
        }

        private void Button_Decrease_Click(object sender, EventArgs e)
        {
            float Font_Size = TextArea.SelectionFont.Size - 2;
            TextArea.SelectionFont = new Font(TextArea.SelectionFont.FontFamily, Font_Size, TextArea.SelectionFont.Style);
            TextArea.Focus();
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void açToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                string Dosya_Yolu = openFileDialog1.FileName;
                TextArea.LoadFile(Dosya_Yolu, RichTextBoxStreamType.RichText);
                Kayit_Yeri = Dosya_Yolu;
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya açarken bir  hata oluştu");
            }
            

        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextArea.Text != "")
            {
                DialogResult SoruBox = MessageBox.Show("Kaydetmezseniz yaptığınız değişiklikler kaybolacaktır.", "Kaydetmek ister misiniz?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );
                if (SoruBox == DialogResult.Yes)
                {
                    DialogResult DialogOK = saveFileDialog1.ShowDialog();
                    if (DialogOK == DialogResult.OK)
                    {
                        string Kayit_Adresi = saveFileDialog1.FileName;
                        TextArea.SaveFile(Kayit_Adresi, RichTextBoxStreamType.RichText);
                    }
                }
                else if (SoruBox == DialogResult.No)
                {
                    Application.Restart();
                }
                else if (SoruBox == DialogResult.Abort)
                {

                }
            }
        }

        private void kaydetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Kayit_Yeri != null)
            {
                TextArea.SaveFile(Kayit_Yeri, RichTextBoxStreamType.RichText);
            }
            else
            {
                DialogResult DialogOK = saveFileDialog1.ShowDialog();
                if (DialogOK == DialogResult.OK)
                {
                    string Kayit_Adresi = saveFileDialog1.FileName;
                    TextArea.SaveFile(Kayit_Adresi);
                }
            }
        }

        private void farklıKaydetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult DialogOK = saveFileDialog1.ShowDialog();
            if (DialogOK == DialogResult.OK)
            {
                string Kayit_Adresi = saveFileDialog1.FileName;
                TextArea.SaveFile(Kayit_Adresi);
            }
        }

        private void geriAlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextArea.Undo();
        }

        private void kesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextArea.SelectedText);
            TextArea.SelectedText = "";
            TextArea.Focus();
        }

        private void kopyalaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextArea.SelectedText);
            TextArea.Focus();
        }

        private void yapıştırToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextArea.SelectedText = Clipboard.GetText();
            TextArea.Focus();
        }

        private void silToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextArea.SelectedText = "";
            TextArea.Focus();
        }

        private void tümünüSeçToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TextArea.SelectAll();
            TextArea.Focus();
        }

        private void sözcükKaydırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sözcükKaydırToolStripMenuItem.Checked)
            {
                TextArea.WordWrap = true;
            }
            else
            {
                TextArea.WordWrap = false;
            }
        }

        private void yazıStiliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult FontOK = fontDialog1.ShowDialog();
            if (FontOK == DialogResult.OK)
            {
                TextArea.SelectionFont = fontDialog1.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (TextArea.WordWrap)
            {
                sözcükKaydırToolStripMenuItem.Checked = true;
            }
            else
            {
                sözcükKaydırToolStripMenuItem.Checked = false;
            }
        }
    }
}
