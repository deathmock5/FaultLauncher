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

namespace FaultWraper
{
    public partial class Splash : Form
    {
        //https://dl.dropbox.com/u/66573922/faultversion.txt
        //https://dl.dropbox.com/u/66573922/Fault.zip
        Label location;
        string DirLocation;
        string curentfiledownloading = "null";
        int curentticks = 0;
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
        void main()
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
            updateText("ALLSET Launching game!", ref location);
            pushText();
            //Application.Run(new Faultmain());
        }
        /// <summary>
        /// if the versent issent curent lets update it!
        /// </summary>
        private void updateVersion()
        {
            Downloadfile("Fault.zip", "https://dl.dropbox.com/u/66573922/");
            unzipfile(DirLocation);
        }
        /// <summary>
        /// Checks the versions of curentfault.txt and faultversion.txt if there diffrent, it downloads new version.
        /// </summary>
        private bool versionCurrent()
        {
            int onlineversion = 0;//faultversion.txt
            int curentversion = 0;//Fault-Version.txt
            Downloadfile("faultversion.txt", "https://dl.dropbox.com/u/66573922/");
            try
            {
                using (StreamReader sr = new StreamReader(DirLocation + "Fault-Version.txt"))
                {
                    curentversion = versionStringToInt(sr.ReadToEnd());
                }
            }
            catch (Exception e)
                {
                    curentversion = 0;
                }
            try
            {
                using (StreamReader sr = new StreamReader(DirLocation + "faultversion.txt"))
                {
                    onlineversion = versionStringToInt(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                onlineversion = 0;
            }
            if (curentversion < onlineversion)
                {
                    return false;
                }
            else
                {
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
            if (curentticks == 5)
            {
                main();
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
        private void unzipfile(string flocation)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(flocation);

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.zip"))
            {
                updateText("decompresing file: " + fileToDecompress.Name, ref location);
                pushText();
                Decompress(fileToDecompress);
                updateText("decompressed.", ref location);
                pushText();
            }
        }
        /// <summary>
        /// Decompresses a specified file, is only called from the unzipfile method.
        /// </summary>
        /// <param name="fileToDecompress"></param>
        private static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
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
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(fileloc + filename), DirLocation + filename);
        }
        /// <summary>
        /// Event called when the status of a download has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            updateText("Downloading File " + curentfiledownloading + e.ProgressPercentage + "%", ref location);
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
        }
    }
}
