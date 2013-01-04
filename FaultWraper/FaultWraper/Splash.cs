/*
 * Author:Deathmock5
 * Date Created: Sep 1 2012
 * Purpose:The launcher for FAULT, a game created by xeology
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Diagnostics;
using Ionic.Zip;
namespace FaultWraper
{
    public partial class Splash : Form
    {
        //https://dl.dropbox.com/u/66573922/faultversion.txt
        //https://dl.dropbox.com/u/66573922/Fault.zip
        Label location;
        string DirLocation;
        string dlLink = "http://tempestgamers.com/fault-ftp/current/";
        string latestBuild = null;
        string curentfiledownloading = "null";
        string faultname = null;
        string versionString = "0.0.0.0";
        int curentticks = 0;
        int curentstep = 0;
        bool downloading;
        bool justupdated = false;
        /// <summary>
        /// Splash Screen Constructor.
        /// </summary>
        public Splash()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer1.Interval = (500) * (1);              // Timer will tick evert second
            timer1.Enabled = true;                       // Enable the timer
            timer1.Start();
            location = Lodingtext1;
            Lodingtext1.Text = "";
            Lodingtext2.Text = "";
            Lodingtext3.Text = "";
            Lodingtext4.Text = "";
            Lodingtext5.Text = "";
            DirLocation = Path.GetDirectoryName(Application.ExecutablePath);
            updateText("Checking for update.", ref location);
            pushText();
            
        }
        /// <summary>
        /// called to take the next step
        /// </summary>
        void nextstep()
        {
            switch (curentstep)
            {
                case 0:
                    {
                        //Downloadfile("faultversion.txt", dropboxlink);
                        //Downloadfile("faultname.txt", dropboxlink);
                        break;
                    }
                case 1:
                    {
                        if (!versionCurrent())
                        {
                            updateText("Version Not Current! Updateing!", ref location);
                            pushText();
                            updateVersion();
                        }
                        else
                        {
                            updateText("version is Current!", ref location);
                            pushText();
                        }
                        break;
                    }
                case 2:
                    {
                        if (File.Exists(DirLocation + "\\.key"))
                        {
                            updateText("keyfile found.", ref location);
                            pushText();
                        }
                        else
                        {
                            updateText("Keyfile not found,", ref location);
                            pushText();
                        };
                        break;
                    }
                case 3:
                    {
                        if (justupdated)
                        {
                            updateText("unpacking fault", ref location);
                            File.Delete(DirLocation + "Fault.exe");
                            unzipfile(DirLocation + "\\" + getFileNameFromURL(latestBuild),DirLocation);
                            pushText();
                            Process.Start(DirLocation + "\\Fault.exe", "unpack");
                        }
                        break;
                    }
                case 4:
                    {
                        updateText("ALLSET Launching game!", ref location);
                        pushText();
                        File.Delete(DirLocation + "\\faultname.txt");
                        break;
                    }
                case 5:
                    {
                        //Process.Start(DirLocation + "\\Fault.exe");
                        Faultmain main = new Faultmain();
                        main.Show();
                        this.Hide();
                        break;
                    }
            }
            curentstep++;
        }
        /// <summary>
        /// if the versent issent curent lets update it!
        /// </summary>
        private void updateVersion()
        {
            File.Delete(DirLocation + "//" + "Fault.exe");
            File.Delete(DirLocation + "//" + "Fault-Version.txt");
            File.Delete(DirLocation + "//" + "Fault-Readme.txt");
            Downloadfile(dlLink);
            justupdated = true;
        }
        /// <summary>
        /// Checks the versions of curentfault.txt and faultversion.txt if there diffrent, it downloads new version.
        /// </summary>
        private bool versionCurrent()
        {
            try
            {
                try
                {
                    StreamReader sr = new StreamReader(DirLocation + "\\" + "Fault-Version.txt");
                    versionString = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception)
                {
                    versionString = "0.0.0.0";
                }
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("http://tempestgamers.com/fault-ftp/versionCurrent.php?pid=1&myVers=" + versionString);
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                Debugger.Log(0, null, "Myversion:" + versionString + "\n");
                switch (content)
                {
                    case "0":
                        Debugger.Log(0, null, "Product id was not found" + "\n");
                        return true;
                    case "1":
                        Debugger.Log(0, null, "Version is current" + "\n");
                        return true;
                    default:
                        latestBuild = content;
                        Debugger.Log(0, null,"New version found:" + content + "\n");
                        return false;
                }
            }
            catch (System.Net.WebException)
            {
                Debugger.Log(0, null, "Internet Unavailiable" + "\n");
                return true;
            }
        }
        /// <summary>
        /// converts 1.1.1.1 to 1111 for version checking
        /// </summary>
        /// <param name="stirng">string of charicters in x.x.x.x format</param>
        /// <returns></returns>
        private int versionStringToInt(String stirng)
        {
            return (Convert.ToInt32(stirng.Replace(".", "")));
        }
        /// <summary>
        /// Pushes text up lines if new  text is inserted., shouled be called at the end of a thread.
        /// </summary>
        /// <param name="text">the text to be pushed</param>
        private void pushText()
        {
            switch (location.Name)
            {
                case "Lodingtext1":
                    {
                        location = Lodingtext2;
                        break;
                    }
                case "Lodingtext2":
                    {
                        location = Lodingtext3;
                        break;
                    }
                case "Lodingtext3":
                    {
                        location = Lodingtext4;
                        break;
                    }
                case "Lodingtext4":
                    {
                        location = Lodingtext5;
                        break;
                    }
                case "Lodingtext5":
                    {
                        Lodingtext1.Text = Lodingtext2.Text;
                        Lodingtext2.Text = Lodingtext3.Text;
                        Lodingtext3.Text = Lodingtext4.Text;
                        Lodingtext4.Text = Lodingtext5.Text;
                        Lodingtext5.Text = "";
                        break;
                    }
                default:
                    System.Windows.Forms.MessageBox.Show(location.Name);
                    break;
            }
        }
        /// <summary>
        /// Updates the text displayed on the gui
        /// </summary>
        /// <param name="text">the text to replace the original</param>
        /// <param name="location">byref the label curently being printed to</param>
        private void updateText(string text, ref Label location)
        {
            location.Text = text;
        }
        /// <summary>
        /// The timers tick event, 
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/a</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            curentticks++;
            if (!downloading)
            {
                nextstep();
            }
            blinkCursor(ref location); //must be at end
        }
        /// <summary>
        /// Creates a cursor at the end of the lines, MUST be called at the end of an update tick
        /// </summary>
        /// <param name="location">BYREF location of label</param>
        private void blinkCursor(ref Label location)
        {
            if (location.Text.Contains("|"))
            {
                location.Text = location.Text.Replace("|", "");
            }
            else
            {
                location.Text = location.Text + "|";
            }
        }
        /// <summary>
        /// Input the location of a zip file and it will unzip the files into the same folder, then delete the zip.
        /// </summary>
        /// <param name="location">Location of the zip file</param>
        private void unzipfile(string zipToUnpack, string unpackDirectory)
        {
            using (ZipFile zip1 = ZipFile.Read(zipToUnpack))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry e in zip1)
                {
                    updateText("decompresing file: " + e.FileName, ref location);
                    pushText();
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                    updateText("decompressed.", ref location);
                    pushText();
                }
            }
        }
        private string getFileNameFromURL(string url)
        {
            return url.Substring(url.LastIndexOf("/") + 1);
        }
        private void Downloadfile(string fileloc)
        {
            downloading = true;
            string filename = getFileNameFromURL(latestBuild);
            curentfiledownloading = filename;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(latestBuild), DirLocation + "\\" + filename);
        }
        /// <summary>
        /// Downloads a spesified file, only ment to download the zip/txt file in xeos dropbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="filename">the name of the file to download +extension</param>
        /// <param name="fileloc">the location of the file on the remote.</param>
        private void Downloadfile(string filename, string fileloc)
        {
            downloading = true;
            curentfiledownloading = filename;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(fileloc + filename), DirLocation + "\\" + filename);
        }
        /// <summary>
        /// Event called when the status of a download has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            updateText("Downloading File " + curentfiledownloading +" "+ e.ProgressPercentage + "%", ref location);
        }
        /// <summary>
        /// event called when a download has been compleated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            updateText("Download completed!",ref location);
            pushText();
            downloading = false;
        }
    }
}
