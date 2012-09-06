using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace FaultWraper.EasymodeClases
{
    class Baceimg
    {
        private Panel bacepannel;
        private TextBox textBox1;
        private Label label1;
        private Point start;
        public Baceimg(int xPos, int yPos)
        {
            bacepannel = new Panel();
            textBox1 = new TextBox();
            label1 = new Label();
            bacepannel.Location = new Point(xPos, yPos);
            bacepannel.Size = new Size(100, 50);
            bacepannel.BackColor = Color.Transparent;
            bacepannel.BorderStyle = BorderStyle.None;

            // Initialize the Label and TextBox controls.
            label1.Location = new Point(5, 11);
            label1.Text = "";
            label1.Size = new Size(47, 25);
            label1.BackColor = Color.Transparent;
            textBox1.Location = new Point(59, 11);
            textBox1.Text = "";
            textBox1.Size = new Size(30, 33);

            // Add the Panel control to the form. 
            // Add the Label and TextBox controls to the Panel.
            bacepannel.Controls.Add(label1);
            bacepannel.Controls.Add(textBox1);
            bacepannel.MouseDown += new MouseEventHandler(baceimg_MouseDown);
            label1.MouseDown += new MouseEventHandler(label1_MouseDown);
        }
        public void setpos(ref Panel item)
        {
            item.Controls.Add(bacepannel);
        }
        public void set_name(string name)
        {
            label1.Text = name;
        }
        public void bgimage_set(Image img)
        {
            
            bacepannel.BackgroundImage = img;
            bacepannel.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public bool isnear()
        {

            return false;
        }
        public void snaptonear()
        {

        }
        //
        /// <summary>
        /// setMouseDown events
        /// </summary>
        /// <param name="item"></param>
        private void setmsdown(ref Panel item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start = e.Location;
                item.MouseUp += new MouseEventHandler(baceimg_MouseUp);
                item.MouseMove += new MouseEventHandler(baceimg_MouseMove);
            }
        }
        private void setmsdown(ref Label item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start = e.Location;
                item.MouseUp += new MouseEventHandler(label1_MouseUp);
                item.MouseMove += new MouseEventHandler(label1_MouseMove);
            }
        }

        //events
        void baceimg_MouseDown(object sender, MouseEventArgs e)
        {
            setmsdown(ref bacepannel, e);
        }
        void baceimg_MouseUp(object sender, MouseEventArgs e)
        {
            bacepannel.MouseMove -= new MouseEventHandler(baceimg_MouseMove);
            bacepannel.MouseUp -= new MouseEventHandler(baceimg_MouseUp);
        }
        void baceimg_MouseMove(object sender, MouseEventArgs e)
        {
            bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
        }
        void label1_MouseDown(object sender, MouseEventArgs e)
        {
            setmsdown(ref label1, e);
        }
        void label1_MouseUp(object sender, MouseEventArgs e)
        {
            label1.MouseMove -= new MouseEventHandler(label1_MouseMove);
            label1.MouseUp -= new MouseEventHandler(label1_MouseUp);
        }
        void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isnear())
            {
                snaptonear();
            }
            else
            {
                bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
            }
        }
    }
}
