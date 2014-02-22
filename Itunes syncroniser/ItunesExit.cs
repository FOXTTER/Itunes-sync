using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Itunes_syncroniser
{
    public partial class ItunesExit : Form
    {
        Form1 forælder;
        public ItunesExit(Form1 frm)
        {
            forælder = frm;
            InitializeComponent();
        }

        private void Ja_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            if (Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "1");
            }
            else if (!Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "0");
            }
            forælder.SkrivTilData("ExitItunes", "1");
            this.Close();
        }

        private void Nej_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            if (Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "1");
            }
            else if (!Husk.Checked)
            {
                forælder.SkrivTilData("Husk", "0");
            }
            forælder.SkrivTilData("ExitItunes", "0");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
