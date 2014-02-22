namespace Itunes_syncroniser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hentPlayliste = new System.Windows.Forms.OpenFileDialog();
            this.Synkroniser = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.vælgMappe = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTekst = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.minimer = new System.Windows.Forms.NotifyIcon(this.components);
            this.Foretrukket = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indstillingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.omToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canceler = new System.Windows.Forms.Button();
            this.kopieretData = new System.Windows.Forms.Label();
            this.Spillelisten = new System.Windows.Forms.RichTextBox();
            this.valgAfListe = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.Forløbet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tilbage = new System.Windows.Forms.Label();
            this.Opdater = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Hast = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hentPlayliste
            // 
            this.hentPlayliste.FileName = "Playliste";
            this.hentPlayliste.Filter = "Itunes playlist|*.m3u";
            // 
            // Synkroniser
            // 
            this.Synkroniser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Synkroniser.Location = new System.Drawing.Point(13, 232);
            this.Synkroniser.Name = "Synkroniser";
            this.Synkroniser.Size = new System.Drawing.Size(176, 23);
            this.Synkroniser.TabIndex = 2;
            this.Synkroniser.Text = "Vælg enhed og synkroniser!";
            this.Synkroniser.UseVisualStyleBackColor = true;
            this.Synkroniser.Click += new System.EventHandler(this.Synkroniser_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(13, 264);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(651, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Vælg den ønskede spilleliste fra listen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Log:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTekst});
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(676, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusTekst
            // 
            this.StatusTekst.Name = "StatusTekst";
            this.StatusTekst.Size = new System.Drawing.Size(39, 17);
            this.StatusTekst.Text = "Status";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(101, 17);
            this.Status.Text = "Ikke synkroniseret";
            // 
            // minimer
            // 
            this.minimer.Text = "Itunes Syncronizer!";
            this.minimer.Visible = true;
            this.minimer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.minimer_MouseClick);
            // 
            // Foretrukket
            // 
            this.Foretrukket.Location = new System.Drawing.Point(12, 27);
            this.Foretrukket.Name = "Foretrukket";
            this.Foretrukket.Size = new System.Drawing.Size(149, 23);
            this.Foretrukket.TabIndex = 10;
            this.Foretrukket.Text = "Tilføj spilleliste til foretrukne";
            this.Foretrukket.UseVisualStyleBackColor = true;
            this.Foretrukket.Click += new System.EventHandler(this.Foretrukket_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filerToolStripMenuItem,
            this.omToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(676, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filerToolStripMenuItem
            // 
            this.filerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indstillingerToolStripMenuItem});
            this.filerToolStripMenuItem.Name = "filerToolStripMenuItem";
            this.filerToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.filerToolStripMenuItem.Text = "Filer";
            // 
            // indstillingerToolStripMenuItem
            // 
            this.indstillingerToolStripMenuItem.Name = "indstillingerToolStripMenuItem";
            this.indstillingerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.indstillingerToolStripMenuItem.Text = "Indstillinger";
            this.indstillingerToolStripMenuItem.Click += new System.EventHandler(this.indstillingerToolStripMenuItem_Click);
            // 
            // omToolStripMenuItem
            // 
            this.omToolStripMenuItem.Name = "omToolStripMenuItem";
            this.omToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.omToolStripMenuItem.Text = "Om";
            // 
            // canceler
            // 
            this.canceler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.canceler.Enabled = false;
            this.canceler.Location = new System.Drawing.Point(195, 232);
            this.canceler.Name = "canceler";
            this.canceler.Size = new System.Drawing.Size(75, 23);
            this.canceler.TabIndex = 12;
            this.canceler.Text = "Cancel!";
            this.canceler.UseVisualStyleBackColor = true;
            this.canceler.Click += new System.EventHandler(this.canceler_Click);
            // 
            // kopieretData
            // 
            this.kopieretData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.kopieretData.AutoSize = true;
            this.kopieretData.Location = new System.Drawing.Point(281, 241);
            this.kopieretData.Name = "kopieretData";
            this.kopieretData.Size = new System.Drawing.Size(0, 13);
            this.kopieretData.TabIndex = 13;
            // 
            // Spillelisten
            // 
            this.Spillelisten.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Spillelisten.Location = new System.Drawing.Point(276, 79);
            this.Spillelisten.Name = "Spillelisten";
            this.Spillelisten.Size = new System.Drawing.Size(261, 147);
            this.Spillelisten.TabIndex = 14;
            this.Spillelisten.Text = "";
            // 
            // valgAfListe
            // 
            this.valgAfListe.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.valgAfListe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.valgAfListe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.valgAfListe.GridLines = true;
            this.valgAfListe.Location = new System.Drawing.Point(16, 79);
            this.valgAfListe.MultiSelect = false;
            this.valgAfListe.Name = "valgAfListe";
            this.valgAfListe.Size = new System.Drawing.Size(254, 147);
            this.valgAfListe.TabIndex = 15;
            this.valgAfListe.UseCompatibleStateImageBehavior = false;
            this.valgAfListe.View = System.Windows.Forms.View.Details;
            this.valgAfListe.MouseEnter += new System.EventHandler(this.valgAfListe_MouseEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Spillelisterne";
            this.columnHeader1.Width = 127;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Hukommelse krævet";
            this.columnHeader2.Width = 116;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Tid forløbet:";
            this.label3.Visible = false;
            // 
            // Forløbet
            // 
            this.Forløbet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Forløbet.AutoSize = true;
            this.Forløbet.Location = new System.Drawing.Point(545, 96);
            this.Forløbet.Name = "Forløbet";
            this.Forløbet.Size = new System.Drawing.Size(0, 13);
            this.Forløbet.TabIndex = 17;
            this.Forløbet.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Tid tilbage:";
            this.label4.Visible = false;
            // 
            // tilbage
            // 
            this.tilbage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tilbage.AutoSize = true;
            this.tilbage.Location = new System.Drawing.Point(547, 149);
            this.tilbage.Name = "tilbage";
            this.tilbage.Size = new System.Drawing.Size(0, 13);
            this.tilbage.TabIndex = 19;
            this.tilbage.Visible = false;
            // 
            // Opdater
            // 
            this.Opdater.Location = new System.Drawing.Point(168, 28);
            this.Opdater.Name = "Opdater";
            this.Opdater.Size = new System.Drawing.Size(133, 23);
            this.Opdater.TabIndex = 20;
            this.Opdater.Text = "Opdater spillelisterne";
            this.Opdater.UseVisualStyleBackColor = true;
            this.Opdater.Click += new System.EventHandler(this.Opdater_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(547, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Overførselshastighed";
            this.label5.Visible = false;
            // 
            // Hast
            // 
            this.Hast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Hast.AutoSize = true;
            this.Hast.Location = new System.Drawing.Point(548, 186);
            this.Hast.Name = "Hast";
            this.Hast.Size = new System.Drawing.Size(0, 13);
            this.Hast.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 325);
            this.Controls.Add(this.Hast);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Opdater);
            this.Controls.Add(this.tilbage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Forløbet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.valgAfListe);
            this.Controls.Add(this.Spillelisten);
            this.Controls.Add(this.kopieretData);
            this.Controls.Add(this.canceler);
            this.Controls.Add(this.Foretrukket);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Synkroniser);
            this.Icon = global::Itunes_syncroniser.Properties.Resources.death_star;
            this.MinimumSize = new System.Drawing.Size(692, 363);
            this.Name = "Form1";
            this.Text = "Itunes Syncronizer";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog hentPlayliste;
        private System.Windows.Forms.Button Synkroniser;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FolderBrowserDialog vælgMappe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.NotifyIcon minimer;
        private System.Windows.Forms.Button Foretrukket;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indstillingerToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusTekst;
        private System.Windows.Forms.Button canceler;
        private System.Windows.Forms.Label kopieretData;
        private System.Windows.Forms.ToolStripMenuItem omToolStripMenuItem;
        private System.Windows.Forms.RichTextBox Spillelisten;
        private System.Windows.Forms.ListView valgAfListe;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Forløbet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label tilbage;
        private System.Windows.Forms.Button Opdater;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Hast;

    }
}

