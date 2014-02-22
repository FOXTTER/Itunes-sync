namespace Itunes_syncroniser
{
    partial class ItunesExit
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
            this.label1 = new System.Windows.Forms.Label();
            this.Husk = new System.Windows.Forms.CheckBox();
            this.Ja = new System.Windows.Forms.Button();
            this.Nej = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Luk itunes også?";
            // 
            // Husk
            // 
            this.Husk.AutoSize = true;
            this.Husk.Location = new System.Drawing.Point(13, 36);
            this.Husk.Name = "Husk";
            this.Husk.Size = new System.Drawing.Size(261, 17);
            this.Husk.TabIndex = 1;
            this.Husk.Text = "Husk dette svar (kan senere ændres i indstillinger)";
            this.Husk.UseVisualStyleBackColor = true;
            // 
            // Ja
            // 
            this.Ja.Location = new System.Drawing.Point(13, 60);
            this.Ja.Name = "Ja";
            this.Ja.Size = new System.Drawing.Size(75, 23);
            this.Ja.TabIndex = 2;
            this.Ja.Text = "Ja";
            this.Ja.UseVisualStyleBackColor = true;
            this.Ja.Click += new System.EventHandler(this.Ja_Click);
            // 
            // Nej
            // 
            this.Nej.Location = new System.Drawing.Point(95, 60);
            this.Nej.Name = "Nej";
            this.Nej.Size = new System.Drawing.Size(75, 23);
            this.Nej.TabIndex = 3;
            this.Nej.Text = "Nej";
            this.Nej.UseVisualStyleBackColor = true;
            this.Nej.Click += new System.EventHandler(this.Nej_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(177, 60);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ItunesExit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 94);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Nej);
            this.Controls.Add(this.Ja);
            this.Controls.Add(this.Husk);
            this.Controls.Add(this.label1);
            this.Name = "ItunesExit";
            this.Text = "ItunesExit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Husk;
        private System.Windows.Forms.Button Ja;
        private System.Windows.Forms.Button Nej;
        private System.Windows.Forms.Button Cancel;
    }
}