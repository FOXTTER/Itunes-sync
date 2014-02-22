using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;

namespace Itunes_syncroniser
{
    /// <summary>
    /// Bliver oprettet for at det er muligt at holde styr på de forskellige indstillinger
    /// </summary>
    public partial class FormIndstillinger : Form
    {

        public FormIndstillinger(Form1 frm1)
        {
            InitializeComponent();
            slettedeLister = new List<string>();
            forælder = frm1;
            // Når formen bliver oprettet indlæses der samtidigt de forskellige værdier der kan indstilles
            Output.Text = forælder.LæsFraData("Output")[0];
            godkendtListe.Items.AddRange(forælder.LæsFraData("Enheder"));
            ForetrukneList.Items.AddRange(forælder.LæsFraData("Foretrukne"));
            if (forælder.LæsFraData("Husk")[0] == "1")
            {
                Husk.Checked = true;
            }
            else if (forælder.LæsFraData("Husk")[0] == "0")
            {
                Husk.Checked = false;
                Husk.Enabled = false;
            }
        }
        Form1 forælder;
        List<string> slettedeLister;
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ds = vælgMappe.ShowDialog();
            if (ds == DialogResult.OK)
            {
                Output.Text = vælgMappe.SelectedPath;
            }
            else if (ds != DialogResult.Cancel && vælgMappe.SelectedPath == "")
            {
                MessageBox.Show("Vælg en gyldig mappe");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            forælder.SkrivTilData("Output", Output.Text);
            // Idet man klikker OK godkender man samtidigt de indtastede værdier, og disse bliver skrevet til datafilen
            foreach (string item in godkendtListe.Items)
            {
                forælder.TilføjData("Enheder", item);
            }
            foreach (string item in ForetrukneList.Items)
            {
                forælder.TilføjData("Foretrukne", item);
            }
            foreach (string item in slettedeLister)
            {
                forælder.sletVærdierne(item);
            }
            if (Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "1");
                forælder.spørg = false;
            }
            else if (!Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "0");
                forælder.spørg = true;
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            USBenheder units = new USBenheder();
            DialogResult res = units.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                godkendtListe.Items.Add(units.alleEnheder.SelectedItem);
            }
        }

        private void slet_Click(object sender, EventArgs e)
        {
            slettedeLister.Add(godkendtListe.SelectedItem.ToString());
            godkendtListe.Items.Remove(godkendtListe.SelectedItem);
        }

        private void sletKnap_Click(object sender, EventArgs e)
        {
            if (ForetrukneList.SelectedItem != null)
            {
                //forælder.sletVærdierne(ForetrukneList.SelectedItem.ToString());
                slettedeLister.Add(ForetrukneList.SelectedItem.ToString());
                ForetrukneList.Items.Remove(ForetrukneList.SelectedItem);
            }
            else
            {
                MessageBox.Show("Du skal vælge en spilleliste for at ku slette den");
            }
        }

        private void Husk_CheckedChanged(object sender, EventArgs e)
        {
            if (!Husk.Checked)
            {
                Husk.Enabled = false;
            }
        }
        


    }
}
