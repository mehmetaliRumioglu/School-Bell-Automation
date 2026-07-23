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
using System.Media;
using WMPLib;
namespace Okul_Zili__Mehmet_ali_rumioglu_
{
    public partial class Main : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        MelodiDataClass melodiler = new MelodiDataClass();
        string[] dersgiris = new string [8];
        string[] ogretmengiris = new string[8];
        string[] cikis = new string[8];
        string EskiSaat;
        bool cansave = true;
        
        public Main()
        {
            InitializeComponent();
        }
        void MelodileriIceriAktar()
        {
            if (File.Exists("MelodiAyarlari.conf"))
            {
                StreamReader sr = new StreamReader("MelodiAyarlari.conf");
                string line;

                List<string> MelodiPath = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    MelodiPath.Add(line);
                }
                if (MelodiPath.Count != 0)
                {
                    melodiler.ogrencigiris = MelodiPath[0];
                    melodiler.ogertmengiris = MelodiPath[1];
                    melodiler.cikis = MelodiPath[2];
                }
                else
                {
                    MessageBox.Show("Lutfen okul zillerini ayarlayiniz.");
                }

                sr.Close();
            }
            else
            {
                File.CreateText("MelodiAyarlari.conf");
                MessageBox.Show("Uygulama ayari yapilmistir lutfen uygulamayi tekrar baslatiniz");
                this.Dispose();
            }
        }
        void handleKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 58 || e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;

            TextBox box = (TextBox)sender;
            if (box.TextLength >= 5 && e.KeyChar == 8)
                e.Handled = false;
            else if (box.TextLength >= 5)
                e.Handled = true;
        }
        void DosyalariiceAktar() 
        {
            if (File.Exists("DersAyarlari.conf"))
            {
                StreamReader sr = new StreamReader("DersAyarlari.conf");
                string line;
                int index = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length == 2)
                        continue;
                    if (line.Length == 17)
                    {
                        int position = line.IndexOf("#");
                        string dersgirisRead = line.Substring(0, position);
                        line = line.Remove(0, position + 1);
                        string ogretmengirisRead = line.Substring(0, position);
                        line = line.Remove(0, position + 1);
                        string cikisRead = line;

                        dersgiris[index] = dersgirisRead;
                        ogretmengiris[index] = ogretmengirisRead;
                        cikis[index] = cikisRead;


                        index++;
                    }
                    else
                    {
                        MessageBox.Show("Saat ayarlamasinda bir hata var, Lutfen uygulamanin calisabilmesi icin duzeltiniz.");
                        break;
                    }
                    if (index == 8)
                        break;
                }
                sr.Close();

                giris1.Text = dersgiris[0];
                giris2.Text = dersgiris[1];
                giris3.Text = dersgiris[2];
                giris4.Text = dersgiris[3];
                giris5.Text = dersgiris[4];
                giris6.Text = dersgiris[5];
                giris7.Text = dersgiris[6];
                giris8.Text = dersgiris[7];

                ogretmengiris1.Text = ogretmengiris[0];
                ogretmengiris2.Text = ogretmengiris[1];
                ogretmengiris3.Text = ogretmengiris[2];
                ogretmengiris4.Text = ogretmengiris[3];
                ogretmengiris5.Text = ogretmengiris[4];
                ogretmengiris6.Text = ogretmengiris[5];
                ogretmengiris7.Text = ogretmengiris[6];
                ogretmengiris8.Text = ogretmengiris[7];

                cikis1.Text = cikis[0];
                cikis2.Text = cikis[1];
                cikis3.Text = cikis[2];
                cikis4.Text = cikis[3];
                cikis5.Text = cikis[4];
                cikis6.Text = cikis[5];
                cikis7.Text = cikis[6];
                cikis8.Text = cikis[7];
            }
            else
            {
                File.CreateText("DersAyarlari.conf");

            }

        }

        void CikisOnLeave(int index, object time)
        {

            
            TextBox box = (TextBox)time;
            if (box.Text.Length < 5 || box.Text.IndexOf(":") == -1)
            {
                MessageBox.Show("Lütfen saati HH:SS şeklinde yazınız, Örnek : 08:30");
                box.Text = "";
                cansave = false;
            }
            else
            {
                cansave = true;
            }
            
            cikis[index] = box.Text;
        }
        void OgretmenGirisOnLeave(int index, object time)
        {
            TextBox box = (TextBox)time;
            if (box.Text.Length < 5 || box.Text.IndexOf(":") == -1)
            {
                MessageBox.Show("Lütfen saati HH:SS şeklinde yazınız, Örnek : 08:30");
                box.Text = "";
                cansave = false;
            }
            else
                cansave = true;
            ogretmengiris[index] = box.Text;

        }
        void DersgirisOnLeave(int index,object time)
        {
            TextBox box = (TextBox)time;

            if (box.Text != "" &&( box.Text.Length < 5 || box.Text.IndexOf(":") == -1))
            {
                MessageBox.Show("Lütfen saati HH:SS şeklinde yazınız, Örnek : 08:30");
                box.Text = "";
                cansave = false;
            }
            else
                cansave = true;
            dersgiris[index] = box.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 0;
            trackBar1.Value = 100;
            durum.ForeColor = Color.Green;
            DosyalariiceAktar();
            MelodileriIceriAktar();
            
        }

        private void giris1_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(0, giris1);
        }

        private void giris2_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(1, giris2);
        }

        private void giris3_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(2, giris3);
        }

        private void giris4_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(3, giris4);
        }

        private void giris5_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(4, giris5);
        }

        private void giris6_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(5, giris6);
        }

        private void giris7_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(6, giris7);
        }

        private void giris8_Leave(object sender, EventArgs e)
        {
            DersgirisOnLeave(7, giris8);
        }

        private void ogretmengiris1_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(0,ogretmengiris1);
        }

        private void ogretmengiris2_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(1, ogretmengiris2);
        }

        private void ogretmengiris3_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(2, ogretmengiris3);
        }

        private void ogretmengiris4_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(3, ogretmengiris4);
        }

        private void ogretmengiris5_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(4, ogretmengiris5);
        }

        private void ogretmengiris6_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(5, ogretmengiris6);
        }

        private void ogretmengiris7_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(6, ogretmengiris7);
        }

        private void ogretmengiris8_Leave(object sender, EventArgs e)
        {
            OgretmenGirisOnLeave(7, ogretmengiris8);
        }

        private void cikis1_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(0, cikis1);
        }

        private void cikis2_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(1, cikis2);
        }

        private void cikis3_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(2, cikis3);
        }

        private void cikis4_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(3, cikis4);
        }

        private void cikis5_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(4, cikis5);
        }

        private void cikis6_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(5, cikis6);
        }

        private void cikis7_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(6, cikis7);
        }

        private void cikis8_Leave(object sender, EventArgs e)
        {
            CikisOnLeave(7, cikis8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cansave)
            {
                if (!File.Exists("DersAyarlari.conf"))
                {
                    File.CreateText("DersAyarlari.conf");
                }
                StreamWriter yazici = new StreamWriter("DersAyarlari.conf");

                for (int i = 0; i < 8; i++)
                {
                    yazici.WriteLine(dersgiris[i] + "#" + ogretmengiris[i] + "#" + cikis[i]);
                }
                yazici.Close();
            }
            else
            {
                MessageBox.Show("Lütfen saatleri doğru giriniz, Doğru girilmeden kayıt edilemez.");
                    
            }
        }

        private void giris1_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris2_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris3_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris4_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris5_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris6_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris7_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void giris8_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris1_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris2_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris3_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris4_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris5_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris6_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris7_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void ogretmengiris8_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis1_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis2_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis3_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis4_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis5_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis6_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis7_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void cikis8_KeyPress(object sender, KeyPressEventArgs e)
        {
            handleKeyPress(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {   if (!checkBox1.Checked)
            {
                durum.Text = "Devre dışı";
                durum.ForeColor = Color.Red;
            }
            else if (player.playState != WMPPlayState.wmppsPlaying)
            {
                durum.Text = "Aktif";
                durum.ForeColor = Color.Green;
            }
            else if (player.playState == WMPPlayState.wmppsPlaying)
            {
                durum.Text = "Çalıyor";
                durum.ForeColor = Color.Orange;
            }
  
             
            if (checkBox1.Checked)
            {
                string current = DateTime.Now.ToShortTimeString();
                if (EskiSaat == null)
                {
                    EskiSaat = current;
                }
                else if (EskiSaat != current)
                {
                    EskiSaat = current;
                    if (player.playState != WMPPlayState.wmppsPlaying)
                    {
                        if (cikis.Contains(EskiSaat))
                        {

                            player.URL = melodiler.cikis;
                            player.controls.play();
                            notifyIcon1.BalloonTipText = "Çıkış zili çalıyor";
                            notifyIcon1.ShowBalloonTip(10000);

                        }
                        else if (dersgiris.Contains(EskiSaat))
                        {

                            player.URL = melodiler.ogrencigiris;
                            player.controls.play();
                            notifyIcon1.BalloonTipText = "Ders Giriş zili çalıyor";
                            notifyIcon1.ShowBalloonTip(10000);

                        }
                        else if (ogretmengiris.Contains(EskiSaat))
                        {
                            notifyIcon1.BalloonTipText = "Öğretmen giriş zili çalıyor";
                            notifyIcon1.ShowBalloonTip(10000);
                            player.URL = melodiler.ogertmengiris;
                            player.controls.play();

                        }
                    }
                }

                saat.Text = current;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

        }

        private void melodiAyarlariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayarlar form = new Ayarlar(melodiler);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            player.URL = ".//marslar//istiklalmarsi.wav";
            player.controls.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            player.controls.stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;
            label13.Text = trackBar1.Value.ToString() + "%";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.controls.stop();

            player.URL = ".//marslar//1dksaygiveistiklalmarsi.wav";
            player.controls.play();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            player.controls.stop();

            player.URL = ".//marslar//saygidurusu.wav";
            player.controls.play();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            player.controls.stop();

            player.URL = ".//marslar//siren.wav";
            player.controls.play();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            player.controls.stop();

            player.URL = melodiler.ogrencigiris;
            player.controls.play();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            player.controls.stop();

            player.URL = melodiler.cikis;
            player.controls.play();
        }

        private void uygulamayiKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void hakkımdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hakkimda hakkimda = new Hakkimda();
            hakkimda.ShowDialog();
        }
    }
}
