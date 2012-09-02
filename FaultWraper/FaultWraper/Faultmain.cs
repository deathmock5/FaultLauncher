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
namespace FaultWraper
{
    public partial class Faultmain : Form
    {
        string DirLocation;
        public Faultmain()
        {
            InitializeComponent();
            DirLocation = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(DirLocation + "\\Fault.exe");
        }
    }
}
