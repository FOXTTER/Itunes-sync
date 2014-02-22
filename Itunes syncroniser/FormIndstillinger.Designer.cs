namespace Itunes_syncroniser
{
    partial class FormIndstillinger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.vælgMappe = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.Foretrukne = new System.Windows.Forms.TabControl();
            this.faner = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Husk = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.slet = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.godkendtListe = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.sletKnap = new System.Windows.Forms.Button();
            this.ForetrukneList = new System.Windows.Forms.ListBox();
            this.Foretrukne.SuspendLayout();
            this.faner.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(6, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ændr output mappe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.Location = new System.Drawing.Point(6, 58);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(276, 20);
            this.Output.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(229, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Foretrukne
            // 
            this.Foretrukne.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Foretrukne.Controls.Add(this.faner);
            this.Foretrukne.Controls.Add(this.tabPage2);
            this.Foretrukne.Controls.Add(this.tabPage1);
            this.Foretrukne.Location = new System.Drawing.Point(12, 12);
            this.Foretrukne.Name = "Foretrukne";
            this.Foretrukne.SelectedIndex = 0;
            this.Foretrukne.Size = new System.Drawing.Size(296, 209);
            this.Foretrukne.TabIndex = 8;
            // 
            // faner
            // 
            this.faner.Controls.Add(this.label2);
            this.faner.Controls.Add(this.label3);
            this.faner.Controls.Add(this.Husk);
            this.faner.Controls.Add(this.button1);
            this.faner.Controls.Add(this.Output);
            this.faner.Location = new System.Drawing.Point(4, 22);
            this.faner.Name = "faner";
            this.faner.Padding = new System.Windows.Forms.Padding(3);
            this.faner.Size = new System.Drawing.Size(288, 183);
            this.faner.TabIndex = 0;
            this.faner.Text = "Generelt";
            this.faner.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Luk Itunes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output mappe";
            // 
            // Husk
            // 
            this.Husk.AutoSize = true;
            this.Husk.Location = new System.Drawing.Point(3, 115);
            this.Husk.Name = "Husk";
            this.Husk.Size = new System.Drawing.Size(132, 17);
            this.Husk.TabIndex = 7;
            this.Husk.Text = "Husk svar ved lukning";
            this.Husk.UseVisualStyleBackColor = true;
            this.Husk.CheckedChanged += new System.EventHandler(this.Husk_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.slet);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.godkendtListe);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(288, 183);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Godkendte enheder";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // slet
            // 
            this.slet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slet.Location = new System.Drawing.Point(10, 117);
            this.slet.Name = "slet";
            this.slet.Size = new System.Drawing.Size(272, 23);
            this.slet.TabIndex = 3;
            this.slet.Text = "Slet valgt enhed fra liste";
            this.slet.UseVisualStyleBackColor = true;
            this.slet.Click += new System.EventHandler(this.slet_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(10, 87);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(272, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Tilføj ny usb-enhed";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // godkendtListe
            // 
            this.godkendtListe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.godkendtListe.FormattingEnabled = true;
            this.godkendtListe.Location = new System.Drawing.Point(10, 24);
            this.godkendtListe.Name = "godkendtListe";
            this.godkendtListe.Size = new System.Drawing.Size(272, 56);
            this.godkendtListe.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Liste over enheder der kan synkroniseres";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sletKnap);
            this.tabPage1.Controls.Add(this.ForetrukneList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(288, 183);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Foretrukne spillelister";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // sletKnap
            // 
            this.sletKnap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sletKnap.Location = new System.Drawing.Point(44, 95);
            this.sletKnap.Name = "sletKnap";
            this.sletKnap.Size = new System.Drawing.Size(196, 32);
            this.sletKnap.TabIndex = 1;
            this.sletKnap.Text = "Slet spilleliste fra foretrukne";
            this.sletKnap.UseVisualStyleBackColor = true;
            this.sletKnap.Click += new System.EventHandler(this.sletKnap_Click);
            // 
            // ForetrukneList
            // 
            this.ForetrukneList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ForetrukneList.FormattingEnabled = true;
            this.ForetrukneList.Location = new System.Drawing.Point(7, 7);
            this.ForetrukneList.Name = "ForetrukneList";
            this.ForetrukneList.Size = new System.Drawing.Size(275, 82);
            this.ForetrukneList.TabIndex = 0;
            // 
            // FormIndstillinger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 262);
            this.Controls.Add(this.Foretrukne);
            this.Controls.Add(this.button2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(336, 300);
            this.Name = "FormIndstillinger";
            this.Text = "FormIndstillinger";
            this.TopMost = true;
            this.Foretrukne.ResumeLayout(false);
            this.faner.ResumeLayout(false);
            this.faner.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog vælgMappe;
        public System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl Foretrukne;
        private System.Windows.Forms.TabPage faner;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox godkendtListe;
        private System.Windows.Forms.Button slet;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.ListBox ForetrukneList;
        private System.Windows.Forms.Button sletKnap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Husk;
    }
}