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

namespace Okul_Zili__Mehmet_ali_rumioglu_
{
    public partial class Ayarlar : Form
    {
        MelodiDataClass melodiler;
        public Ayarlar(MelodiDataClass melodiler)
        {
            this.melodiler = melodiler;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var ok = dialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var ok = dialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Media Files|*.wav;*.mp3;";
            var ok = dialog.ShowDialog();
            if (ok == DialogResult.OK)
            {
                textBox3.Text = dialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("MelodiAyarlari.conf"))
            {
                File.CreateText("MelodiAyarlari.conf");
            }
            StreamWriter yazici = new StreamWriter("MelodiAyarlari.conf");
            yazici.WriteLine(textBox1.Text);
            yazici.WriteLine(textBox2.Text);
            yazici.WriteLine(textBox3.Text);
            melodiler.ogrencigiris = textBox1.Text;
            melodiler.ogertmengiris = textBox2.Text;
            melodiler.cikis = textBox3.Text;
            yazici.Close();
            this.Close();
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            textBox1.Text = melodiler.ogrencigiris;
            textBox2.Text = melodiler.ogertmengiris;
            textBox3.Text = melodiler.cikis;
        }
    }
}
