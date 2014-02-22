using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using iTunesLib; // Itunes biblioteket
using System.Resources;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;

namespace Itunes_syncroniser
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            // Jeg skjuler hoved formen så længe programmet er i gang med at indlæse.
            this.Visible = false;
            // Tilgengæld opretter jeg en splashscreen til at vise status på indlæsningen
            // TO DO: indlæse værdierne på en anden tråd, sådan at brugerfladen forbliver responsiv
            Splashscreen splash = new Splashscreen();
            splash.Show();
            splash.label2.Text = "Loader itunes...";
            // Åbner en instans af Itunes, hvis Itunes ikke er installeret bliver en Exception kastet
            try
            {
                itunes = new iTunesApp();
            }
            catch (Exception e)
            {
                MessageBox.Show("Itunes er ikke installeret, Lukker programmet", e.Message);
                this.Dispose();
            }
            splash.label2.Text = "Opretter/læser database...";
            //Sørger for itunes bliver åbnet i baggrunden
            try
            {
                itunes.BrowserWindow.Minimized = true;
            }
            catch (Exception)
            {

            }
            Prefer.Enabled = false;
            //VÆR OPMÆRKSOM HER på crossthread
            Form1.CheckForIllegalCrossThreadCalls = true;
            // Undersøger om datafilen er til stede i programmets mappe.
            //Hvis datafilen findes læses værdierne fra denne, ellers oprettes der en ny datafil med
            // standard indstillingerne.
            FileInfo fi = new FileInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data.xml"));
            if (!fi.Exists)
            {
                OpretDataFil();
            }
            else if (fi.Exists)
            {
                try
                {
                    // Datafilen bliver læst, og en lokal instans bliver oprette.t
                    xmldoc.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data.xml"));
                    // Hvis der findes foretrukne lister bliver disse tilføjet
                    foreach (string værdi in LæsFraData("Foretrukne"))
                    {
                        addForetrukket(værdi, false);
                    }
                    if (LæsFraData("Foretrukne").Length != 0)
                    {
                        Prefer.Enabled = true;
                    }
                    //Det læses om det ønsket at itunes bliver lukket sammen med programmet
                    if (LæsFraData("ExitItunes")[0] == "1")
                    {
                        exit = true;
                    }
                    else if (LæsFraData("ExitItunes")[0] == "0")
                    {
                        exit = false;
                    }
                    // Der bliver læst om der skal spørges hvad man vil gøre når man lukker programmet
                    if (LæsFraData("Husk")[0] == "1")
                    {
                        spørg = false;
                    }
                    else
                    {
                        spørg = true;
                    }
                    outputDir = LæsFraData("Output")[0];
            
                }
                // Hvis der bliver kastet en exception under dette betyder det at dataene i datafilen bliver korrupt
                // I det tilfælde bliver den gamle slettet, og en ny datafil med standardværdierne oprettet.
                catch (Exception)
                {
                    MessageBox.Show("Datafil korrupt, opretter ny");
                    File.Delete(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data.xml"));
                    OpretDataFil();
                }


            }
            // De forskellige grafiske komponenter bliver herefter indlæst
            splash.label2.Text = "Opretter komponenter";

            minimer.ContextMenu = miniMenu;
            miniMenu.MenuItems.Add("Vis", Vis_Click);
            Prefer.Text = "Foretrukne";
            miniMenu.MenuItems.Add(Prefer);
            miniMenu.MenuItems.Add("Exit", Exit_Click);
            minimer.Icon = Itunes_syncroniser.Properties.Resources.death_star;
            // De spillelister der er tilgængelige bliver herefter indlæst
            // og fremstillet for brugeren.
            splash.label2.Text = "Loader Playlister";
            foreach (IITPlaylist pl in itunes.LibrarySource.Playlists)
            {
                if (pl.Size != 0)
                {
                    spillelister.Add(pl);
                    valgAfListe.Items.Add(pl.Name).SubItems.Add((int)(pl.Size / 1024 / 1024) + " Mb");
                }    
            }            
            alleLister = spillelister.ToArray();
            // Den form der reprpræsenterer indstillinger bliver oprettet men ikke vist
            indstillinger = new FormIndstillinger(this);
            indstillinger.Owner = this;
            splash.label2.Text = "Loader hovedvinduet";
            this.Visible = true;
            splash.label2.Text = "Program Loadet!";
            splash.Close();
        }

        //De forskellige globale variabler bliver deklareret
        bool quitting = false;
        FormIndstillinger indstillinger;
        List<DriveInfo> temp = new List<DriveInfo>();
        XmlDocument dokument = new XmlDocument();
        XmlDocument xmldoc = new XmlDocument();
        XmlElement xmlelem;
        XmlElement xmlelem2;
        XmlElement xmlelem3;
        XmlElement xmlelem4;
        XmlText xmltext;
        public bool spørg = true;
        bool exit;
        int tal;
        MenuItem Prefer = new MenuItem();
        string outputDir;
        iTunesApp itunes;
        List<IITPlaylist> spillelister = new List<IITPlaylist>();
        IITPlaylist[] alleLister;
        IITPlaylist valgtSpilleliste;
        IITFileOrCDTrack[] songFile;
        ContextMenu miniMenu = new ContextMenu();
        BackgroundWorker wk = new BackgroundWorker();        
        Timer timer = new Timer();
        
        /// <summary>
        /// Funktionen der opretter en data fil, i tilfælde af at den ikke findes, eller er blevet korrupt.
        /// </summary>
        void OpretDataFil()
        {
            xmlelem = xmldoc.CreateElement("", "Data", "");
            xmltext = xmldoc.CreateTextNode("Dataene");
            xmlelem.AppendChild(xmltext);
            xmldoc.AppendChild(xmlelem);
            xmlelem2 = xmldoc.CreateElement("", "Output", "");
            xmldoc.ChildNodes.Item(0).AppendChild(xmlelem2);
            xmldoc["Data"]["Output"].InnerText = @"media\musik";
            xmlelem3 = xmldoc.CreateElement("", "ExitItunes", "");
            xmldoc.ChildNodes.Item(0).AppendChild(xmlelem3);
            xmldoc["Data"]["ExitItunes"].InnerText = "0";
            xmlelem4 = xmldoc.CreateElement("", "Husk", "");
            xmldoc.ChildNodes.Item(0).AppendChild(xmlelem4);
            xmldoc["Data"]["Husk"].InnerText = "0";
            // Funktion der gemmer de forskellige elementer i xmldoc til data.xml bliver kaldt
            Save();
            xmldoc.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data.xml"));
            MessageBox.Show("Datafil oprettet");
        }
        private void Synkroniser_Click(object sender, EventArgs e)
        {
            // I tilfælde af at baggrundarbejderen er i gang med en synkronisering bliver forespørgelsen afbrudt
            if (wk.IsBusy)
            {
                MessageBox.Show("Vent på at der bliver synkroniseret færdig, eller afbryd synkroniseringen!");
                return;
            }
            // Mens de indledende undersøgelser foretages skal progressbaren bare indikere at der sker noget
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 10;
            outputDir = LæsFraData("Output")[0];
            label3.Visible = true;
            label4.Visible = true;
            Forløbet.Visible = true;
            tilbage.Visible = true;
            label5.Visible = true;
            Hast.Visible = true;
            // Brugeren bliver bedt om at vælge det drev der skal synkroniseres til, og funktionen der synkroniserer bliver kaldt
            USBenheder USBvalg = new USBenheder();
            DialogResult dr = USBvalg.ShowDialog();
            if (dr == DialogResult.OK)
            {
                canceler.Enabled = true;

                try
                {
                    SynkroniserFunction(valgAfListe.SelectedIndices[0], false, USBvalg.returnVærdi);
                }
                catch (Exception)
                {
                    
                MessageBox.Show("Du skal vælge en spilleliste");
                progressBar1.Style = ProgressBarStyle.Blocks;

                return;
                }
                
                
            }
            else
            {
                MessageBox.Show("Du skal vælge et drev");
                progressBar1.Style = ProgressBarStyle.Blocks;

                return;

            }
        }
        /// <summary>
        /// Programmet gemmer den aktuelle instans af datafilen
        /// </summary>
        public void Save()
        {
            xmldoc.Save(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "data.xml"));
        }
        /// <summary>
        /// Tilføjer værdier til datafilen
        /// </summary>
        /// <param name="Navn"> Navnet på den etiket som der skal skrives til</param>
        /// <param name="Værdi"> Den værdi som der skal skrives til datafilen</param>
        public void TilføjData(string Navn, string Værdi)
        {
            XmlNodeList xnList = xmldoc.SelectNodes("/Data/" + Navn);
            // Hvis værdien findes i forvejen bliver denne ikke tilføjet til datafilen
            foreach (XmlNode xn in xnList)
            {
                if (xn.InnerText == Værdi)
                {
                    return;
                }
            }
            // Hvis den ikke findes bliver der skrevet en ny "node" i xmlfilen med denne værdi.
            xmlelem2 = xmldoc.CreateElement("", Navn, "");
            xmlelem2.InnerText = Værdi;
            xmldoc["Data"].AppendChild(xmlelem2);
            Save();
            
        }
        /// <summary>
        /// De forskellige værdier som baggrundsarbejderen skal bruge bliver deklareret
        /// </summary>
        public class BagrundArgs
        {
            public string drevLetterClass { get; set; }
            public string OutputClass { get; set; }
            public IITFileOrCDTrack[] songFileClass { get; set; }
            public IITPlaylist valgtSpillelisteClass { get; set; }
            public bool minimerEfterSyncClass { get; set; }
            public long totalSizeClass { get; set; }
            public List<IITFileOrCDTrack> AddedSongsClass { get; set; }
            public List<string> DeletedSongsClass { get; set; }
            public List<Songs> UpdatedSongsClass { get; set; }

        }
        /// <summary>
        /// Selve funktionen der initialiserer synkroniseringen
        /// </summary>
        /// <param name="spillelisteNummer"> Indexnummeret på den spilleliste som skal synkroniseres</param>
        /// <param name="minimerEfterSync"> Hvis 'true' bliver hovedvinduet minimeret efter synkroniseringen</param>
        /// <param name="output">Information om det drev som der skal synkronseres til</param>
        void SynkroniserFunction(int spillelisteNummer, bool minimerEfterSync, DriveInfo output)
        {
            this.Show();
            // Mens den indledende information indsamles indikerer progressbaren bare at der sker noget.
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 10;
            label3.Visible = true;
            label4.Visible = true;
            Forløbet.Visible = true;
            tilbage.Visible = true;
            label5.Visible = true;
            Hast.Visible = true;
            this.WindowState = FormWindowState.Normal;
            tal = 0;
            IITFileOrCDTrack file = null;
            // En timer oprettes til at håndtere opdateringen af teksten på statusbaren
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Start();
            DriveInfo drevLetter = output;
            long totalfilesize = 0;
            try
            {
                int L = 0;
                // Ud fra spillelistens indeks på listen indlæser jeg en instans af denne
                valgtSpilleliste = alleLister[spillelisteNummer];
                if (valgtSpilleliste.Size > (500 * 1024 * 1024))
                {
                    MessageBox.Show("Ved store spillelister kan programmet \"Hænge\" i et kort stykke tid, før det svarer igen.");
                }
                // Jeg opretter en liste med alle sangene fra denne spilleliste
                List<IITTrack> tracks = new List<IITTrack>();

                foreach (IITTrack item in valgtSpilleliste.Tracks)
                {
                    tracks.Add(item);
                }
                // Sangene bliver som standard sorteret alfabetisk
                tracks.Sort(new Comparison<IITTrack>((x, y) => String.Compare(x.Name, y.Name)));
                // Der bliver oprettet et array til alle de elementer på listen der er en fil,
                // det vil sige at elementer som online radiostationer ikke bliver tilføjet.
                songFile = new IITFileOrCDTrack[tracks.Count];
                // Jeg tjekker herefter om elementerne på spillelisten er en fil,
                // hvis det er tilfældet tilføjer jeg dem til listen med de endelige sange der skal overføres
                foreach (IITTrack tr in tracks)
                {
                    if (tr.Kind == ITTrackKind.ITTrackKindFile)
                    {
                        file = (IITFileOrCDTrack)tr;
                        if (file.Location != null)
                        {
                            FileInfo fi = new FileInfo(file.Location);
                            if (fi.Exists)
                            {
                                songFile[L] = file;
                                L++;
                                // Til oversigten har jeg en variabel der holder styr på størrelsen af hele spillelisten
                                totalfilesize += fi.Length;
                            }
                        }
                    }

                }
                // Jeg tjekker om der er plads til spillelisten på enheden
                if (totalfilesize > drevLetter.AvailableFreeSpace)
                    {
                        MessageBox.Show("Ikke nok plads på mediet!" + Environment.NewLine + "Synkronisering annulleret!");
                        canceler.Enabled = false;
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        timer.Stop();
                        StatusTekst.Text = "Status";
                        return;
                    }
                Spillelisten.Text = "";
            }
                // Hvis ikke der er valgt en spilleliste bliver en exception kastet
            catch (Exception ex)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                MessageBox.Show(ex.Message);
                return;
            }
            // Her defineres de forskellige argumenter som backgroundworkeren skal bruge til synkroniseringen.
            BagrundArgs arguments = new BagrundArgs
            {
                drevLetterClass = drevLetter.Name,
                songFileClass = songFile,
                minimerEfterSyncClass = minimerEfterSync,
                OutputClass = outputDir,
                valgtSpillelisteClass = valgtSpilleliste,
                totalSizeClass = totalfilesize/1024,
            };
            // En baggrundsarbejder bliver oprettet, med tilhørende eventhandlers
            wk = new BackgroundWorker();
            wk.DoWork +=new DoWorkEventHandler(wk_DoWork);
            wk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(wk_RunWorkerCompleted);
            wk.ProgressChanged += new ProgressChangedEventHandler(wk_ProgressChanged);
            wk.WorkerReportsProgress = true;
            wk.WorkerSupportsCancellation = true;
            // Baggrundsarbejderen bliver startet med de tidligere definerede argumenter
            // Alt hvad der skal bruges af variabler skal med i disse argumenter for at,
            // undgå at referere på tværs af tråde og dermed gøre programmer "uforudsigelig"
            wk.RunWorkerAsync(arguments);
        }

        void wk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Der oprettes en midlertidig class med de argumenter
            // som blev sendt med da der blev rapporteret en fremgang af baggrundsarbejderen
            værdierTilProgress tempVal = e.UserState as værdierTilProgress;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = e.ProgressPercentage;
            string filIBrug = tempVal.location;
            // Status over hvor langt synkroniseringen er sådan at brugeren kan bevare oversigten
            label2.Text = "Kopierer fil:";
            kopieretData.Text = "Behandlet " + tempVal.filesize/1024 + " MB af "
                + tempVal.totalsize/1024 + " MB " + "(" +((tempVal.filesize*100)/tempVal.totalsize) + "%)";
            Spillelisten.Text = filIBrug;
            Forløbet.Text = "";
            tilbage.Text = "";
            if (tempVal.tidForløbetMinute > 0)
            {
                Forløbet.Text = ((int)tempVal.tidForløbetMinute + "Minut(ter) og ");
            }
            Forløbet.Text += ((int)tempVal.tidForløbetSecond%60 + " Sekunder!");
            int minutter = 0;
            minutter = ((tempVal.tidTilbage - tempVal.tidTilbage % 60) / 60);
            if (minutter > 0)
            {
                tilbage.Text = (minutter + "Minut(ter) og ");
            }
            tilbage.Text += tempVal.tidTilbage%60 + " Sekunder";
            // Hastigheden af overførslen.
            try
            {
                Hast.Text = (tempVal.filesize / (int)tempVal.tidForløbetSecond) / 1024 + "MB/s";
            }
            catch (Exception)
            {
                //Ignorer
            }

        }

        void wk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // De forskellige labels til oversigten under synkroniseringen skjules igen sådan de ikke står der hele tiden
            label3.Visible = false;
            label4.Visible = false;
            Forløbet.Visible = false;
            tilbage.Visible = false;
            label5.Visible = false;
            Hast.Visible = false;
            Forløbet.Text = "";
            tilbage.Text = "";
            Hast.Text = "";
            timer.Stop();
            canceler.Enabled = false;
            progressBar1.Value = 0;
            kopieretData.Text = "";
            // Hvis operationen ikke blev annulleret
            if (!e.Cancelled)
            {
                BagrundArgs argumenter = e.Result as BagrundArgs;
                // Opretter midlertidige værdier til at bestemme de områder der skal farves i loggen
                int tempLængde1;
                int tempLængde2;
                int tempLængde3;
                int tempLængde4;
                label2.Text = "De sange der er blevet synkroniseret";
                Spillelisten.Text = ("Nye sange:" + Environment.NewLine);
                tempLængde1 = Spillelisten.Text.Length;
                // Først bliver de nye sange tilføjet
                foreach (var item in argumenter.AddedSongsClass)
                {
                    Spillelisten.Text += (item.Artist + " - " + item.Name + Environment.NewLine);
                }
                tempLængde2 = Spillelisten.Text.Length;
                // Så de opdaterede
                Spillelisten.Text += (Environment.NewLine + "Opdaterede sange: " + Environment.NewLine);
                foreach (var item in argumenter.UpdatedSongsClass)
                {
                    Spillelisten.Text += (item.Artist + " - " + item.Name + Environment.NewLine);
                }
                // Og til sidst de fjernede sange
                Spillelisten.Text += (Environment.NewLine + "Fjernet fra spillelisten:" + Environment.NewLine);
                tempLængde3 = Spillelisten.Text.Length;
                foreach (string item in argumenter.DeletedSongsClass)
                {
                    Spillelisten.Text += (Path.GetFileNameWithoutExtension(@item) + Environment.NewLine);
                }
                tempLængde4 = Spillelisten.Text.Length;

                // Ud fra længderne på de givne afsnit, bliver de farvet som de skal for at danne visuelt overblik
                Spillelisten.Select(tempLængde1, (tempLængde2 - tempLængde1));
                Spillelisten.SelectionColor = Color.Green;
                Spillelisten.Select(tempLængde3, (tempLængde4 - tempLængde3));
                Spillelisten.Select();
                Spillelisten.SelectionColor = Color.Red;
                StatusTekst.Text = "Synkroniseringen lykkedes!";
                MessageBox.Show("Synkronisering fuldført");
                if (argumenter.minimerEfterSyncClass)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }
                // Hvis synkroniseringen bliver annulleret skal der gives besked
            else if (e.Cancelled)
            {
                StatusTekst.Text = "Synkroniseringen lykkedes ikke!";
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Value = 0;
                MessageBox.Show("Synkroniseringen blev annulleret");
                label2.Text = "Log:";
                Spillelisten.Text = "Synkroniseringen blev annulleret af brugeren!";
                // Hvis synkroniseringen blev annulleret under lukning af programmet fortsætter lukningen
                if (quitting)
                {
                    Dispose(true);
                }
            }
        }
        /// <summary>
        /// Den class der indeholder værdierne til at opdatere status
        /// </summary>
        public class værdierTilProgress
        {
            public long filesize { get; set; }
            public long totalsize { get; set; }
            public double tidForløbetSecond {get; set;}
            public int tidForløbetMinute { get; set; }
            public string location { get; set; }
            public int tidTilbage { get; set; }
        }
        /// <summary>
        /// Værdier fra de forskellige sange 
        /// </summary>
        public class Songs
        {
            public string Name { get; set; }
            public string Artist { get; set; }
            public string fileName { get; set; }
        }
        void wk_DoWork(object sender, DoWorkEventArgs e)
        {
            bool fejlOverskriv = false;
            // Et stopur oprettes til at holde styr på
            // hvor lang tid der er gået siden synkroniseringen startede.
            Stopwatch st = new Stopwatch();
            st.Start();
            List<int> milliSekunder = new List<int>();
            // De argumenter der blev sendt med da arbejderen blev oprettet
            // bliver oprettet i en lokal instans for at undgå kald på tværs af trådene
            BagrundArgs værdierTilDoWork = e.Argument as BagrundArgs;
            List<Songs> existingSongs = new List<Songs>();
            int runderTilbage = værdierTilDoWork.songFileClass.Length;
            string nyMappe = null;
            string spilLok = null;
            if (værdierTilDoWork.OutputClass != null)
            {
                try
                {
                    nyMappe = Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass);
                    spilLok = Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass, "Spillelister");
                    // Hvis det er en ny enhed vil der ikke eksistere en mappe til filerne, og denne skal oprettes først
                    if (!Directory.Exists(nyMappe))
                    {
                        Directory.CreateDirectory(nyMappe);
                    }
                }
                catch (Exception)
                {
                    DialogResult ds = MessageBox.Show("Den valgte output mappe er korrupt, synkroniserer til standardmappen", "Fejl", MessageBoxButtons.OKCancel);
                    if (ds == DialogResult.OK)
                    {
                        værdierTilDoWork.OutputClass = @"media\musik";
                        SkrivTilData("Output", værdierTilDoWork.OutputClass);
                        nyMappe = Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass);
                        spilLok = Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass, "Spillelister");
                    }
                    else if (ds == DialogResult.Cancel)
                    {
                        wk.CancelAsync();
                        e.Cancel = true;
                        return;
                    }
                    
                
                
                }
                if (!Directory.Exists(spilLok))
                {
                    Directory.CreateDirectory(spilLok);
                }
                string androidPlayliste = "";
                værdierTilProgress sizeValues = new værdierTilProgress
                {
                    filesize = 0,
                    totalsize = værdierTilDoWork.totalSizeClass
                };

                værdierTilDoWork.AddedSongsClass = new List<IITFileOrCDTrack>();
                værdierTilDoWork.DeletedSongsClass = new List<string>();
                værdierTilDoWork.UpdatedSongsClass = new List<Songs>();
                int i = 0;
                string existingPlaylists = "";
                // Til den videre behandling oprettes den spilleliste der skal overføres som en string, med titel, artist og lokation
                foreach (var sang in værdierTilDoWork.songFileClass)
                {
                    androidPlayliste += (Path.GetFileName(sang.Location) + Environment.NewLine
                        + "#Artist: " + sang.Artist + "#" + Environment.NewLine
                        + "#Title: " + sang.Name + "#" + Environment.NewLine);
                }
                //
                // I tilfælde af at der findes en spilleliste med navnet på den der skal synkroniseres
                // læser jeg denne fil. Hvis spillelisten er "original" tilføjer jeg sangene til en liste.
                // Dette gør jeg for at jeg senere kan sammenligne den nye spilleliste med den gamle, og så finde frem til om
                // sangene skal opdateres med henhold til titel eller kunstner.
                foreach (var item in Directory.GetFiles(@spilLok))
                {
                    if (Path.GetFileNameWithoutExtension(@item) == værdierTilDoWork.valgtSpillelisteClass.Name)
                    {
                        using (TextReader tr = new StreamReader(@item))
                        {
                            while (true)
                            {
                                try
                                {
                                    Songs Temp = new Songs();
                                    Temp.fileName = tr.ReadLine();
                                    Temp.Artist = tr.ReadLine();
                                    if (!Temp.Artist.StartsWith("#Artist:"))
                                    {
                                        fejlOverskriv = true;
                                        MessageBox.Show("Fejl i spillelisten");
                                        break;
                                    }
                                    Temp.Artist = Temp.Artist.Replace("#Artist: ", "");
                                    Temp.Artist = Temp.Artist.Replace("#", "");
                                    Temp.Name = tr.ReadLine();
                                    if (!Temp.Name.StartsWith("#Title:"))
                                    {
                                        fejlOverskriv = true;
                                        MessageBox.Show("Fejl i spillelisten");
                                        break;
                                    }
                                    Temp.Name = Temp.Name.Replace("#Title: ", "");
                                    Temp.Name = Temp.Name.Replace("#", "");
                                    existingSongs.Add(Temp);
                                }
                                catch (Exception)
                                {
                                    break;
                                }
                            }
                        }
                        // Hvis der er en fejl på den eksisterende liste slettes denne,
                        // sådan at den nye bliver ordentligt opdateret.
                        if (fejlOverskriv)
                        {
                            File.Delete(@item);
                        }
                    }
                }
                List<string> opdateret = new List<string>();
                foreach (var item in existingSongs)
                {
                    // Det bliver tjekker om der er sange som er blevet 
                    if ((!androidPlayliste.Contains("#Title: " + item.Name +"#") || !androidPlayliste.Contains("#Artist: " + item.Artist + "#"))&& androidPlayliste.Contains(item.fileName))
                    {
                        opdateret.Add(item.fileName);
                        //File.Delete(@Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass, item.fileName));
                        værdierTilDoWork.UpdatedSongsClass.Add(item);
                    }
                }
                // Den nye spilleliste bliver skrevet til enheden
                using (TextWriter tw = new StreamWriter(Path.Combine(@spilLok, værdierTilDoWork.valgtSpillelisteClass.Name + ".m3u")))
                {
                    tw.Write(androidPlayliste);
                }
                foreach (var item in Directory.GetFiles(@spilLok))
                {
                    using (TextReader tr = new StreamReader(@item))
                    {
                        existingPlaylists += tr.ReadToEnd();
                    }
                }
                bool once = true;
                // I tilfælde af at der findes sange som ikke eksisterer på en af spillelisterne på enheden
                // bliver brugeren spurgt om disse skal slettets.
                foreach (string item in Directory.GetFiles(@Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass)))
                {
                    if (!existingPlaylists.Contains(Path.GetFileName(item)))
                    {
                        if (once && MessageBox.Show("Der findes sange der ikke er på nogen Spilleliste," + Environment.NewLine + "skal disse slettes?", "Slette filer?", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            // 'once' tjener til at brugeren kun bliver spurgt om hvorvidt han ønsker at slette
                            // de overskydende sange en gang under synkroniseringen
                            once = false;
                            break;
                        }
                        else
                        {
                            once = false;
                        }
                        File.Delete(@Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass, Path.GetFileName(item)));
                        værdierTilDoWork.DeletedSongsClass.Add(Path.GetFileNameWithoutExtension(item));
                    }
                }
                foreach (var sang in værdierTilDoWork.songFileClass)
                {
                    // Et stopur der skal bruges til at holde styr på hvor lang tid tager bliver startet
                    Stopwatch rundeTimer = new Stopwatch();
                    rundeTimer.Start();
                    string nyPlacering = Path.Combine(værdierTilDoWork.drevLetterClass, værdierTilDoWork.OutputClass, Path.GetFileName(sang.Location));
                    // 
                    if (sang.Location != string.Empty)
                    {
                        FileInfo info = new FileInfo(@sang.Location);
                        try
                        {
                            // Hvis filen er på listen over opdaterede filer skal den overskrives på enheden for at denne bliver opdateret
                            if (opdateret.Contains(info.Name))
                            {
                                File.Copy(@sang.Location, @nyPlacering, true); 
                            }
                            // Ellers skal filen kun overskrives hvis der var en fejl på den spilleliste der lå på enheden
                            else
                            {
                                File.Copy(@sang.Location, @nyPlacering, fejlOverskriv);
                                værdierTilDoWork.AddedSongsClass.Add(sang);
                            }
                            sizeValues.location = sang.Location;

                            
                        }
                        catch (IOException)
                        {

                        }
                        sizeValues.filesize += info.Length / (1024);
                        if (wk.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        i++;
                        decimal procent = ((decimal)i / (decimal)værdierTilDoWork.songFileClass.Length) * 100;
                        sizeValues.tidForløbetSecond = st.Elapsed.TotalSeconds;
                        sizeValues.tidForløbetMinute = st.Elapsed.Minutes;
                        runderTilbage--;
                        milliSekunder.Add(rundeTimer.Elapsed.Milliseconds);
                        //sizeValues.tidTilbage = TidTilbage(milliSekunder,runderTilbage);
                        // Til oversigten bliver den estimerede tid der er tilbage udregnet
                        sizeValues.tidTilbage = TidTilbage(sizeValues.filesize, sizeValues.totalsize, milliSekunder);
                        wk.ReportProgress((int)procent, sizeValues); // For hver fil der bliver forsøgt synkroniseret bliver fremskridtet rapporteret
                    }
                }
                // Når alle sangene på listen er blevet synkroniseret bliver værdierne 
                // sendt videre med signal om at arbejderen er færdig
                e.Result = værdierTilDoWork;
            }
            else
            {
                MessageBox.Show("Du skal vælge en mappe");
            }
        }
        /// <summary>
        /// Udregner den resterende tid ud fra det udførte antal runder
        /// </summary>
        /// <param name="tidIMilli"> Er den tid som den seneste runde tog i millisekunder</param>
        /// <param name="runderTilbage"> Er det resterende antal af runder i synkroniseringen</param>
        /// <returns>Tiden der er tilbage i sekunder</returns>
        public int TidTilbage(List<int> tidIMilli, int runderTilbage)
        {
            int gnsTidPerRunde = 0;
            int tempEnum = tidIMilli.Count;
            int totalTid = 0;
            foreach (int time in tidIMilli)
            {
                totalTid += time;
            }
            gnsTidPerRunde = (totalTid / tempEnum);
            return (gnsTidPerRunde * runderTilbage)/1000;
        }
        /// <summary>
        /// Udregner den resterende tid ud fra den behandlede mængde data
        /// </summary>
        /// <param name="behandlet">De behandlede data i byte</param>
        /// <param name="total">Den totale mængde af data der skal behandles</param>
        /// <param name="tidIMilli">Tiden som det har taget at behandle sangene</param>
        /// <returns>Returnerer den tid der er tilbage i sekunder</returns>
        public int TidTilbage(long behandlet, long total, List<int> tidIMilli)
        {
            float totalTid = 0;
            foreach (int time in tidIMilli)
            {
                totalTid += time;
            }

            float tidPerByte = (totalTid / behandlet);
            float ByteTilbage = (total-behandlet);
            float tidTilbage = (tidPerByte * ByteTilbage)/1000;
            return (int)tidTilbage;
        }
        /// <summary>
        /// Opdaterer statussen
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            switch (tal)
            {
                case 0:
                    StatusTekst.Text = "Synkroniserer";
                    tal++;
                    break;
                case 1:
                    StatusTekst.Text = "Synkroniserer.";
                    tal++;
                    break;
                case 2:
                    StatusTekst.Text = "Synkroniserer..";
                    tal++;
                    break;
                case 3:
                    StatusTekst.Text = "Synkroniserer...";
                    tal = 0;
                    break;
                default:
                    break;
                
            }
        }
        /// <summary>
        /// Læser data fra data filen
        /// </summary>
        /// <param name="etikette">Navnet på den etikette der skal læses fra</param>
        /// <returns>Returnerer de læste værdier i et array</returns>
        public string[] LæsFraData(string etikette)
        {         
            XmlNodeList xnList = xmldoc.SelectNodes("/Data/" + etikette);
            int i = 0;
            string[] returnVærdi = new string[xnList.Count];
            // Under nogen af etiketterne er der flere værdier, disse opstilles i et array
            foreach (XmlNode xn in xnList)
            {
                returnVærdi[i] = xn.InnerText;
                i++;
            }
            return returnVærdi;
        }
        /// <summary>
        /// Skriver værdier til en eksisterende post i data filen
        /// </summary>
        /// <param name="etikette">Den etikette der skal skrives under</param>
        /// <param name="Værdi">Den værdi der skal skrives</param>
        public void SkrivTilData(String etikette, string Værdi)
        {
            // Prøver først at skrive til en eksisterende værdi
            // Findes der ikke det skrives der en ny
            try
            {
                xmldoc["Data"][etikette].InnerText = Værdi;
                Save();
            }
            catch (Exception)
            {
                xmlelem2 = xmldoc.CreateElement("", etikette, "");
                xmlelem2.InnerText = Værdi;
                xmldoc["Data"].AppendChild(xmlelem2);
                Save();
            }            
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            minimer.BalloonTipTitle = "Itunes Synchronizer er blevet minimeret";
            minimer.BalloonTipText = "Itunes Synchronizer bliver ved med at køre i baggrunden.";
            // Hvis formen minimeres skal den skjules, samtidigt skal den blive kørende
            // i baggrunden for at tjekke om en af de godkendte enheder bliver tilsluttet.
            if (FormWindowState.Minimized == this.WindowState)
            {
                minimer.ShowBalloonTip(250);
                this.Hide();
                this.WindowState = FormWindowState.Normal;   
            }
        }

        private void minimer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;         
            }
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Vis_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Sørger for at de komponenter der er blevet oprettet også bliver slettet fra hukommelsen når programmet lukker
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // I tilfældet af at der er en synkronisering i gang bliver brugeren
                // spurgt om denne skal annulleres, svares der ja, venter systemet på
                // at baggrundsarbejderen er blevet annulleret
                if (wk.IsBusy) 
                {
                    DialogResult dr = MessageBox.Show("Der er en synkronisering i gang, skal denne annulleres?", null, MessageBoxButtons.YesNoCancel);
                    if (dr == DialogResult.Yes)
                    {
                        wk.CancelAsync();
                        quitting = true;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                // Afhængig af brugeres preferencer bliver denne spurgt om Itunes skal lukkes sammen med programmet
                if (spørg)
                {
                    ItunesExit IE = new ItunesExit(this);
                    DialogResult diares = IE.ShowDialog();
                    if (diares == DialogResult.Yes)
                    {
                        itunes.Quit();
                    }
                    else if (diares == DialogResult.Cancel)
                    {
                        return;
                    }
                    
                }
                    // I tilfælde af at brugeren ønsker at Itunes bliver lukket sammen med lukkes denne
                    // medmindre der er en afspilning i gang
                else if (exit)
                {
                    if (itunes.PlayerState == ITPlayerState.ITPlayerStatePlaying)
                    {
                        DialogResult dr = MessageBox.Show("Itunes spiller musik, vil du stadig lukke det?", "Luk Itunes?", MessageBoxButtons.YesNoCancel);
                        if (dr == DialogResult.Yes)
                        {
                            itunes.Quit();
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                this.minimer.Dispose(); // Ikonet på proceslinjen bliver frigjort fra hukommelsen
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void valgAfListe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Status.Text = "Ikke synkroniseret!";
        }

        private void Foretrukket_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in valgAfListe.SelectedItems)
	        {
		        addForetrukket(item.Text, true);
	        }
            Prefer.Enabled = true;
        }
        /// <summary>
        /// Tilføjer en spilleliste til listen med foretrukne
        /// </summary>
        /// <param name="foretrukketListe">Den spilleliste der skal tilføjes</param>
        /// <param name="write">Hvis 'true' bliver værdien også skrevet til datafilen</param>
        public void addForetrukket(string foretrukketListe, bool write )
        {
            MenuItem preferListe = new MenuItem();
            preferListe.Text = foretrukketListe;
            preferListe.Click += new EventHandler(preferListeAction);
            Prefer.MenuItems.Add(preferListe);
            if (write)
            {
                TilføjData("Foretrukne", preferListe.Text);
            }
        }

        /// <summary>
        /// Sletter værdier fra menuen
        /// </summary>
        /// <param name="slettes">Teksten på det MenuItem der skal slettes</param>
        public void sletVærdierne(string slettes)
        {
            // Bliver kaldt fra formen 'FormIndstillinger' sådan
            // at de slettede værdier også bliver slettet fra menuen og datafilen
            for (int i = 0; i < Prefer.MenuItems.Count; i++)
            {
                if (Prefer.MenuItems[i].Text == slettes)
                {
                    Prefer.MenuItems[i].Dispose();
                }
            }
            SletData("Enheder", slettes);
            SletData("Foretrukne", slettes);
        }
        /// <summary>
        /// Sletter data fra datafilen
        /// </summary>
        /// <param name="etiket">Den etiket hvorfra der skal slettes</param>
        /// <param name="værdi">Den værdi som der skal slettes</param>
        public void SletData(string etiket, string værdi)
        {
            XmlNodeList xnList = xmldoc.SelectNodes("/Data/" + etiket);
            foreach (XmlNode xn in xnList)
            {
                if (xn.InnerText == værdi)
                {
                    xmldoc["Data"].RemoveChild(xn);
                }
            }
            Save();
        }
        
        /// <summary>
        /// Den funktion som bliver kaldt når en af de foretrukne elementer bliver aktiveret.
        /// Starter synkroniseringen af den foretrukne spilleliste der blev trykket.
        /// </summary>
        public void preferListeAction(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            USBenheder USBvalg = new USBenheder();
            for (int i = 0; i < alleLister.Length; i++)
            {
                if (alleLister[i].Name == mi.Text)
                {
                    USBvalg.ShowDialog();
                    if (USBvalg.DialogResult == DialogResult.OK)
                    {
                        canceler.Enabled = true;
                        SynkroniserFunction(i, true, USBvalg.returnVærdi);
                    }
                    else
                    {
                        MessageBox.Show("Du skal vælge et drev");
                    }
                }
            }
        }
        private void indstillingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            indstillinger = new FormIndstillinger(this);
            indstillinger.Owner = this;
            indstillinger.ShowDialog(this);
        }

        private const int usbAction = 0x0219; // Konstant der angiver at der sker noget på en USB port
        private const int usbAnkommer = 0x8000; // Konstant der angiver at en enhed er blevet tilsluttet systemet
        private const int usbFjernet = 0x8004; // Konstant der angiver at en enhed er blevet afbrudt fra systemet
        private const int usbTypeVolume = 0x00000002; // Konstant der er et udtryk for typen af enheden

        public struct VolumeEnhed
        {
            public int dbcv_size;
            public int dbcv_devicetype;
            public int dbcv_reserved;
            public int dbcv_unitmask;
        }

        /// <summary>
        /// Funktion der behandler beskeder fra windows.
        /// Bruges til at fange beskeden om at der er blevet sluttet en usbenhed til systemet.
        /// Hvis enheden står på den godkendte liste skal der spørges om en synkronisering skal foretages
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            int enhedsType;
            switch (m.Msg)
            {
                case usbAction:
                    switch (m.WParam.ToInt32())
                    {
                                        
                        //
                        // En enhed er blevet tilsluttet til/afbrudt fra systemet
                        //
                        case usbAnkommer:
                            {
                                //
                                // usbeenheden er blevet tilsluttet systemet
                                //
                                enhedsType = Marshal.ReadInt32(m.LParam, 4);

                                if (enhedsType == usbTypeVolume)
                                {
                                    VolumeEnhed vol;
                                    vol = (VolumeEnhed)Marshal.PtrToStructure(m.LParam, typeof(VolumeEnhed));

                                    // Trækker enheds bogstavet ud, og opretter informationer om drevet
                                    char driveLetter = (char)(Math.Log((double)vol.dbcv_unitmask, 2.0) + 65);
                                    DriveInfo drive = new DriveInfo(driveLetter + ":");

                                    //
                                    // Grundet den måde androids filstyring er opbygget på er det nødvendigt at tjekke om drevet er klar
                                    // Når en adroidenhed bliver tilsluttet systemet bliver den tilsluttet systemet som en volumenenhed.
                                    // Men hukommelsen er ikke klar, så funktionen bliver kaldt med meddelelsen om at en enhed er tilsluttet,
                                    // men idet drevet ikke er 'ready' er det ikke muligt at foretage en synkronisering.
                                    // Brugeren skal aktivt gå ind på android enheden og tilslutte drevet før at hukommelsen bliver klar.
                                    // Så hvis drevet ikke er klart, venter programmet derfor i 1 minut på at den bliver klar
                                    // Hvis dette ikke sker bliver enheden ignoreret
                                    // 
                                    if (drive.IsReady)
                                    {
                                        // Hvis drevet er klart fra starten af og står på den godkendte liste bliver
                                        // brugeren spurgt om han vil synkronisere
                                        if (indstillinger.godkendtListe.Items.Contains(drive.VolumeLabel))
                                        {
                                            minimer.ShowBalloonTip(300, "Godkendt enhed tilsluttet", drive.VolumeLabel, ToolTipIcon.Info);
                                            BackgroundWorker andenTrådArbejder = new BackgroundWorker();
                                            andenTrådArbejder.DoWork += new DoWorkEventHandler(andenTrådArbejder_DoWork);
                                            andenTrådArbejder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(andenTrådArbejder_RunWorkerCompleted);
                                            andenTrådArbejder.RunWorkerAsync(drive);
                                        }

                                    }

                                    else if (!drive.IsReady)
                                    {
                                        // Hvis ikke enheden var klar skal der ventes og se om den bliver det.
                                        // Dette gøres ved hjælp af en baggrunds arbejder, for at undgå at fryse hovedtråden,
                                        // og dermed den grafiske brugerflade.
                                        BackgroundWorker tempArbejder = new BackgroundWorker();
                                        tempArbejder.DoWork += new DoWorkEventHandler(tempArbejder_DoWork);
                                        tempArbejder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tempArbejder_RunWorkerCompleted);
                                        tempArbejder.RunWorkerAsync(drive);
                                    }
                                }

                            }

                            break;
                        
                         // Hvis enheden bliver fjernet nulstilles programmets brugerflade
                        
                        case usbFjernet:

                            Spillelisten.Text = "";
                            label2.Text = "Log:";
                            StatusTekst.Text = "Status";
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        void andenTrådArbejder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SynkroniserSpørgsmål(e.Result as DriveInfo, true);
        }

        void andenTrådArbejder_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = e.Argument as DriveInfo;
        }

        // Bliver kaldt når programmet er færdigt med at vente på enheden
        void tempArbejder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isTheAndroidThere resultat = e.Result as isTheAndroidThere;
            if (!resultat.Ja)
            {
                return;
            }
            DriveInfo drev = resultat.androiden;
            // I det tilfælde at enheden var der bliver der spurgt om der skal synkroniseres
            if (indstillinger.godkendtListe.Items.Contains(drev.VolumeLabel))
            {
                SynkroniserSpørgsmål(drev, true);
            }
            else if (!indstillinger.godkendtListe.Items.Contains(drev.VolumeLabel))
            {
                if (MessageBox.Show("Vil du godkende og synkronisere drevet: " + drev.VolumeLabel + "?"
                    , "Ingen godkendte drev tilsluttet", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // enheden bliver tilføjet til den godkendte liste og synkroniseret
                    TilføjData("Enheder", drev.VolumeLabel);
                    indstillinger.godkendtListe.Items.Add(drev.VolumeLabel);
                    SynkroniserSpørgsmål(drev, false);
                }

            }
        }

        public class isTheAndroidThere
        {
            //
            // Værdier der skal bruges til at 
            // angive om en enhed er blevet tilsluttet
            //
            public bool Ja { get; set; }
            public DriveInfo androiden { get; set; }
        }
        void tempArbejder_DoWork(object sender, DoWorkEventArgs e)
        {
            DriveInfo tempDrive = e.Argument as DriveInfo;
            bool fundet = false;
            // Der bliver tjekket gennem 30 runder af 2 sekunder, hvis drevet bliver klar
            // inden for intervallet bliver dette retuneret
            for (int i = 0; i < 30; i++)
            {
                if (tempDrive.IsReady)
                {
                    e.Result = tempDrive;
                    fundet = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(2000);
                } 
            }
            isTheAndroidThere resultat = new isTheAndroidThere
            {
                Ja = fundet,
                androiden = tempDrive
            };
            e.Result = resultat;
        }

        /// <summary>
        /// Igangsætter synkroniseringen
        /// </summary>
        /// <param name="drev">Det drev det er muligt at synkronisere</param>
        /// <param name="spørg">Værdi der angiver om brugeren skal spørges om han vil synkronisere</param>
        void SynkroniserSpørgsmål(DriveInfo drev, bool spørg)
        {
            DialogResult ds = DialogResult.No;
            string[] existListe;
            try
            {
                existListe = Directory.GetFiles(@Path.Combine(drev.Name, outputDir, "Spillelister"));
            }
            catch (Exception)
            {
                return;
            }
            // Det undersøges om der ligger eksisterende spillelister på enheden
            // Hvis der gør bliver brugeren spurgt om han vil opdatere dem
            foreach (var item in existListe)
            {
                string spilLok = Path.GetFileNameWithoutExtension(item);

                if (spørg)
                {
                    ds = MessageBox.Show("Opdater spillelisten " + spilLok + " på enheden " + drev.VolumeLabel + "?",
                                    "Synkronisering", MessageBoxButtons.YesNo); 
                }

                if (ds == DialogResult.Yes || !spørg)
                {

                    for (int i = 0; i < alleLister.Length; i++)
                    {
                        if (alleLister[i].Name == spilLok)
                        {
                            SynkroniserFunction(i, true, drev);
                        }
                    }
                    // TO DO: understøt at opdatere mere end en spilleliste på enheden
                    break; 
                }
            }
            
        }

        private void canceler_Click(object sender, EventArgs e)
        {
            wk.CancelAsync();
        }

        private void valgAfListe_MouseEnter(object sender, EventArgs e)
        {
            //
            // Sørger for at listen med spillelister bliver aktiv når en mus
            // bliver holdt over den
            //
            valgAfListe.Select();
        }

        private void Opdater_Click(object sender, EventArgs e)
        {
            OpdaterListe();
        }

        /// <summary>
        /// Opdaterer listen med spillelister
        /// </summary>
        void OpdaterListe()
        {
            valgAfListe.Clear();
            valgAfListe.Columns.Add(columnHeader1);
            valgAfListe.Columns.Add(columnHeader2);
            foreach (IITPlaylist liste in alleLister)
            {
                valgAfListe.Items.Add(liste.Name).SubItems.Add((int)(liste.Size / 1024 / 1024) + " Mb");
            }
        }

    }
}
