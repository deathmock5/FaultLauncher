using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Media;
namespace FaultWraper
{
    public partial class Faultmain : Form
    {
        string DirLocation;
        DateTime CurrTime = DateTime.Now;
        public Faultmain()
        {
            InitializeComponent();
            DirLocation = Path.GetDirectoryName(Application.ExecutablePath);
            timer1.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer1.Interval = (1000) * (1);              // Timer will tick evert second
            timer1.Enabled = true;                       // Enable the timer
            timer1.Start();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrTime = DateTime.Now;
            clock.Text = CurrTime.ToShortTimeString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(DirLocation + "\\Fault.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VirusEdit viredit = new VirusEdit();
            viredit.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (strtmnu.Visible)
            {
                strtmnu.Hide();
            }
            else
            {
                strtmnu.Show();
            }
        }
        /// <summary>
        /// time to play them kickass sounds!
        /// </summary>
        /// <param name="sound">the sound located in the /sounds/ folder</param>
        private void playSimpleSound(string sound)
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(DirLocation + "/sounds/" + sound);
                simpleSound.Play();
            }
            catch
            {
                MessageBox.Show(DirLocation + "/sounds/" + sound + " - 404");
            }
        }

        private void iexplore_Click(object sender, EventArgs e)
        {

        }

        private void tut_Click(object sender, EventArgs e)
        {

        }

        private void mail_Click(object sender, EventArgs e)
        {

        }
    }
}
