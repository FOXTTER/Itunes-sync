using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;

namespace Itunes_syncroniser
{
    /// <summary>
    /// Bliver oprettet for at man kan vælge en usbEnhed at synkronisere med
    /// </summary>
    public partial class USBenheder : Form
    {
        public USBenheder()
        {
            InitializeComponent();
            // Når formen bliver oprettet tjekker den alle de USBenheder af typen 'Volume' 
            // og skriver dem til en liste
            var usbEnheder = from d in DriveInfo.GetDrives()
                      where d.DriveType == DriveType.Removable select d;
            try
            {
                foreach (DriveInfo information in usbEnheder)
                {
                    if (information.IsReady && information.VolumeLabel != "")
                    {
                        alleEnheder.Items.Add(information.VolumeLabel);
                        listeOverEnheder.Add(information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hovsa igen");
            }
        }
        List<DriveInfo> listeOverEnheder = new List<DriveInfo>();
        public DriveInfo returnVærdi;
        private void vælgEnhed_Click(object sender, EventArgs e)
        {
            if (alleEnheder.SelectedItem != null)
            {
                this.DialogResult = DialogResult.OK;
                foreach (var item in listeOverEnheder )
                {
                    if (item.VolumeLabel == alleEnheder.SelectedItem.ToString())
                    {
                        returnVærdi = item;
                    }
                }
            }
            else this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Hvis du ikke kan se din mobil på listen kan det skyldes at den ikke har noget navn." +
                Environment.NewLine + "Alternativt kan det skyldes at mobil ikke er tilsluttet computeren som diskdrev." + Environment.NewLine
                + "Undersøg først om mobilen er tilsluttet som diskdrev og herefter om der er angivet et navn for disken under denne computer.");
        }
    }
}
