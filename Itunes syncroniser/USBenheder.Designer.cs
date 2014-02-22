namespace Itunes_syncroniser
{
    partial class USBenheder
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
            this.alleEnheder = new System.Windows.Forms.ListBox();
            this.vælgEnhed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // alleEnheder
            // 
            this.alleEnheder.FormattingEnabled = true;
            this.alleEnheder.Location = new System.Drawing.Point(13, 13);
            this.alleEnheder.Name = "alleEnheder";
            this.alleEnheder.Size = new System.Drawing.Size(156, 95);
            this.alleEnheder.TabIndex = 0;
            // 
            // vælgEnhed
            // 
            this.vælgEnhed.Location = new System.Drawing.Point(12, 114);
            this.vælgEnhed.Name = "vælgEnhed";
            this.vælgEnhed.Size = new System.Drawing.Size(156, 28);
            this.vælgEnhed.TabIndex = 1;
            this.vælgEnhed.Text = "Vælg enhed";
            this.vælgEnhed.UseVisualStyleBackColor = true;
            this.vælgEnhed.Click += new System.EventHandler(this.vælgEnhed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vises din enhed ikke?";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(126, 151);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(42, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Klik her";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // USBenheder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 176);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vælgEnhed);
            this.Controls.Add(this.alleEnheder);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "USBenheder";
            this.Text = "USBenheder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button vælgEnhed;
        public System.Windows.Forms.ListBox alleEnheder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}