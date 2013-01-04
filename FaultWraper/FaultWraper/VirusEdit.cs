using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FaultWraper.EasymodeClases;
using System.Reflection;
//System.Diagnostics.Debugger.Log(0, "1","\n OnConnect: Socket has been closed\n")
namespace FaultWraper
{
    public partial class VirusEdit : DevComponents.DotNetBar.Office2007RibbonForm
    {
        PictureBox mypic = new PictureBox();
        List<Baceimg> easypannels = new List<Baceimg>();
        Assembly _assembly;
        string DirLocation = Path.GetDirectoryName(Application.ExecutablePath);
        string curentfile = "";
        /// <summary>
        /// Constructor
        /// </summary>
        public VirusEdit()
        {
            InitializeComponent();
            tabbottom.SelectedTab = hardtab;
            ribbonControl1.SelectedRibbonTabItem = ribbonHardcore;
            _assembly = Assembly.GetExecutingAssembly();
            hardtxtbox.TextChanged += new EventHandler(changedtext);
            EnableDoubleBuffering();
        }
        public void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true. 
            this.SetStyle(ControlStyles.DoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
        }
        /// <summary>
        /// called when the text in the textbox is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void changedtext(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// returns a bitmap from an string.
        /// </summary>
        /// <param name="name">the string to the file local to the programs directory</param>
        /// <returns></returns>
        public Bitmap getImageFromResourcess(string name)
        {
            Stream _imageStream = null;
            
            try
            {
                _imageStream = _assembly.GetManifestResourceStream("FaultWraper.Resources." + name);
            }
            catch
            {

            }
            Bitmap returnimg = new Bitmap(_imageStream);
            returnimg.MakeTransparent();
            return returnimg;
        }
        /// <summary>
        /// check teh spelling
        /// </summary>
        private void spellcheck()
        {

        }
        /// <summary>
        /// checks each line at loadtime
        /// </summary>
        /// <param name="line">the line of code</param>
        private void parseline(string line)
        {
            if (line.Contains("#NAME"))
            {
                line = line.Replace("#NAME ", "");
                ribbon_virname.Text = line;
            }
            else if (line.Contains("#AUTHOR "))
            {
                line = line.Replace("#AUTHOR ", "");
                ribbon_virauth.Text = line;
            }
            else
            {
                hardtxtbox.AppendText(line + "\n");
                //switch (line)
                //{
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void holdicon()
        {

        }
        /// <summary>
        /// instantate the object with the specified name
        /// </summary>
        /// <param name="name">the name fo the control you want.</param>
        private void instantateobject(string name)
        {
            Baceimg mybace = new Baceimg(10, 10);
            if (easypannels.Count == 0)
            {
                mybace = new Baceimg(10, 10);
            }
            else
            {
                mybace = new Baceimg(10, (easypannels.Count * easypannels[0].getSize().Height) + 10); 
            }
            bool wassucsessfull = true;
            try
            {
                mybace.putInContainer(ref easydev);
                mybace.set_name(name);
                mybace.bgimage_set(getImageFromResourcess(name + ".png"));
            }
            catch (Exception)
            {
                wassucsessfull = false;
                System.Diagnostics.Debugger.Log(0, "1", "File '" + name + "' not found");

            }
            finally
            {
                if (wassucsessfull)
                {
                    mybace.giveRef(ref easypannels);
                    mybace.setID(easypannels.Count);
                    easypannels.Add(mybace);
                }
            }
        }
        #region "Buttons"
        //hardmenu
        private void ribbonHardcore_Click(object sender, EventArgs e)
        {
            tabbottom.SelectedTab = hardtab;
        }
        private void ribbionEasy_Click(object sender, EventArgs e)
        {
            tabbottom.SelectedTab = easytab;
        }
        private void strtbtn_new_Click(object sender, EventArgs e)
        {

        }
        private void strtbtn_open_Click(object sender, EventArgs e)
        {
            //open file dialog
            Stream myStream = null;
            opendia.InitialDirectory = DirLocation + "//programs";
            opendia.Filter = "Fault Virus files (*.bcv)|*.bcv";
            opendia.FilterIndex = 2;
            opendia.RestoreDirectory = true;
            if (opendia.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = opendia.OpenFile()) != null)
                    {
                        //erase the prev boxes.
                        ribbon_virauth.Text = "";
                        ribbon_virname.Text = "";
                        hardtxtbox.Text = "";
                        //read the file
                        String[] lines = File.ReadAllLines((opendia.FileName));
                        foreach (string line in lines)
                        {
                            parseline(line);
                        }
                        //addfile to recent documents,
                        // set the curently open file
                        curentfile = opendia.FileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        private void strtbtn_save_Click(object sender, EventArgs e)
        {
            if (curentfile != "")
            {
                System.IO.StreamWriter fs = new System.IO.StreamWriter(curentfile);
                fs.WriteLine("#NAME " + ribbon_virname.Text);
                fs.WriteLine("#AUTHOR " + ribbon_virauth.Text);
                foreach (string line in hardtxtbox.Lines)
                {
                    fs.WriteLine(line);
                }
                fs.Close();
            }
            else
            {
                // Displays a SaveFileDialog so the user can save the virus
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Fault Virus file|*.bcv";
                saveFileDialog1.Title = "Save an virus File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.StreamWriter fs = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    fs.WriteLine("#NAME " + ribbon_virname.Text);
                    fs.WriteLine("#AUTHOR " + ribbon_virauth.Text);
                    foreach (string line in hardtxtbox.Lines)
                    {
                        fs.WriteLine(line);
                    }
                    fs.Close();
                }
            }

        }
        private void strtbtn_close_Click(object sender, EventArgs e)
        {
            //close file
        }
        private void strtbtn_close2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void strtbtn_options_Click(object sender, EventArgs e)
        {

        }
        //easymenu
        private void Movebutton_Click(object sender, EventArgs e)
        {
            instantateobject("move");
        }
        private void Copybuton_Click(object sender, EventArgs e)
        {
            instantateobject("copy");
        }
        private void Coruptbutton_Click(object sender, EventArgs e)
        {
            instantateobject("corupt");
        }
        private void shiftbutton_Click(object sender, EventArgs e)
        {
            instantateobject("shift");
        }
        #endregion

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            mypic.Image = getImageFromResourcess("copy.png");
            mypic.Location = new Point(10, 10);
            mypic.MouseMove += new MouseEventHandler(bla);
            easydev.Controls.Add(mypic);
        }
        private void bla(object sender, MouseEventArgs e)
        {
            mypic.Location = new Point(e.X + mypic.Location.X, e.Y + mypic.Location.Y);
        }
    }
}
