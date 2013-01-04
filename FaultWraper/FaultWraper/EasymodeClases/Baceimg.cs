using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using FaultWraper.EasymodeClases;
namespace FaultWraper.EasymodeClases
{
    internal class Baceimg
    {
        public Point mypos;
        private PictureBox bacepannel;
        private Label label1;
        private Point start;
        private TextBox textBox1;
        private List<Baceimg> referedList;
        private int width = 86;
        private int height = 24;
        private bool canBeSnapedTo;
        private int snapedPannelId;
        private int myID;
        private int imSnapedTo;
        private bool snapAble;
        private bool snaping;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xPos">the x position of the control</param>
        /// <param name="yPos">the y position of the control</param>
        public Baceimg(int xPos, int yPos)
        {
            bacepannel = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            bacepannel.Location = new Point(xPos, yPos);
            mypos = new Point(xPos, yPos);
            bacepannel.Size = new Size(width, height);
            //bacepannel.BackColor = Color.Transparent;
            //bacepannel.BorderStyle = BorderStyle.None;
            
            snapAble = true;
            canBeSnapedTo = true;
            snaping = false;
            snapedPannelId = -1;
            imSnapedTo = -1;

            // Initialize the Label and TextBox controls.
            label1.Location = new Point(5, 11);
            label1.Text = "";
            label1.Size = new Size(47, 25);
            label1.BackColor = Color.Transparent;
            textBox1.Location = new Point(width - 20, 0);
            textBox1.Text = "";
            textBox1.Size = new Size(30, 33);
            textBox1.MaxLength = 3;

            // Add the Panel control to the form.
            // Add the Label and TextBox controls to the Panel.
            bacepannel.Controls.Add(label1);
            bacepannel.Controls.Add(textBox1);
            bacepannel.MouseDown += new MouseEventHandler(baceimg_MouseDown);
            label1.MouseDown += new MouseEventHandler(label1_MouseDown);
        }
        public void putInContainer(ref DoubleBufferPanel item)
        {
            item.Controls.Add(bacepannel);
        }
        public void giveRef(ref List<Baceimg> mylist)
        {
            referedList = mylist;
        }

        public void setID(int val)
        {
            myID = val;
        }
        public int getID()
        {
            return myID;
        }

        public Point getPos()
        {
            return mypos;
        }
        public Size getSize()
        {
            return new Size(width, height);
        }

        public void bgimage_set(Image img)
        {
            bacepannel.Image= img;
            //bacepannel.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public void set_name(string name)
        {
            //label1.Text = name;
        }

        
        public bool canHasSnapAble()
        {
            return canBeSnapedTo;
        }
        public void giveSnapedPannel(int panelid)
        {
            snapedPannelId = panelid;
            canBeSnapedTo = false;
        }
        public void takeSnapedPannel()
        {
            snapedPannelId = -1;
            canBeSnapedTo = true;
        }
        private bool snapToNear(MouseEventArgs e)
        {
            for (int i = 0; i < referedList.Count; i++)
            {
                if (referedList[i].getID() != myID)//skip if self
                {
                    if (referedList[i].canHasSnapAble())//can have a snapable
                    {
                        if (inSnapRange(referedList[i],e) && !isSnaping())
                        {
                            snapTo(referedList[i]);
                            referedList[i].giveSnapedPannel(getID());
                            if (!canHasSnapAble())
                            {
                                moveMyChildren(mypos);
                            }
                            imSnapedTo = i;
                            Debugger.Log(0, null, "I Snaped\n");
                            return true;
                        }
                        else
                        {
                            if (isSnaping())
                            {
                                if (i == imSnapedTo)
                                {
                                    Debugger.Log(0, null, ", I " + myID + " am snaped to " + i);
                                    if (e.X < 0 ||
                                        e.X > width ||
                                        e.Y < 0 ||
                                        e.Y > height)
                                    {
                                        unsnap();
                                        referedList[i].takeSnapedPannel();
                                        Debugger.Log(0, null, ", I " + myID + " Unsnaped from " + i);
                                        return false;
                                    }
                                    Debugger.Log(0, null, ", I Stay Snaped \n");
                                    return true;
                                }
                            }
                            else
                            {
                                Debugger.Log(0, null, "Im Not Snaped");
                            }
                        }
                    }
                    else //cant have a snapable
                    {
                        Debugger.Log(0, null, "Cannot have a snapable\n");
                        if (isSnaping())
                        {
                            if (i == imSnapedTo)
                            {
                                Debugger.Log(0, null, ", I " + myID + " am snaped to " + i);
                                if (e.X < 0 ||
                                    e.X > width ||
                                    e.Y < 0 ||
                                    e.Y > height)
                                {
                                    unsnap();
                                    referedList[i].takeSnapedPannel();
                                    Debugger.Log(0, null, ", I " + myID + " Unsnaped from " + i);
                                    return false;
                                }
                                Debugger.Log(0, null, ", I Stay Snaped \n");
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool isSnapAble()
        {
            return snapAble;
        }
        public bool isSnaping()
        {
            return snaping;
        }
        public void unsnap()
        {
            snaping = false;
        }
        public bool inSnapRange(Baceimg target,MouseEventArgs e)
        {
            if (mypos.X + e.X > target.getPos().X + target.getSize().Width &&
                mypos.Y + e.Y < target.getPos().Y + target.getSize().Height &&
                mypos.Y + e.Y > target.getPos().Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void snapTo(Baceimg target)
        {
            mypos = new Point(target.getPos().X + target.getSize().Width, target.getPos().Y);
            snaping = true;
        }
        public void setPos(Point position)
        {
            mypos = position;
            bacepannel.Location = mypos;
        }
        public void moveMyChildren(Point myPosition)
        {
            if (!canHasSnapAble())
            {
                Point addedpos = new Point(myPosition.X + width, myPosition.Y);
                referedList[snapedPannelId].moveMyChildren(addedpos);
                referedList[snapedPannelId].setPos(addedpos);
                Debugger.Log(0, null, myID + " moveing child pannel:" + snapedPannelId + "\n");
            }
        }

        //Events
        private void baceimg_MouseDown(object sender, MouseEventArgs e)
        {
            setmsdown(ref bacepannel, e);
        }
        private void baceimg_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSnapAble())
            {
                if (snapToNear(e))
                {

                    bacepannel.Location = mypos;
                }
                else
                {
                    
                    bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
                    mypos = bacepannel.Location;
                    moveMyChildren(mypos);
                }
                Debugger.Log(0, null, e.X + "," + e.Y + "\n");
            }
            else
            {
                bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
                mypos = bacepannel.Location;
            }
            bacepannel.Refresh();
        }
        private void baceimg_MouseUp(object sender, MouseEventArgs e)
        {
            bacepannel.MouseMove -= new MouseEventHandler(baceimg_MouseMove);
            bacepannel.MouseUp -= new MouseEventHandler(baceimg_MouseUp);
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            setmsdown(ref label1, e);
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSnapAble())
            {
                if (snapToNear(e))
                {

                    bacepannel.Location = mypos;
                }
                else
                {

                    bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
                    mypos = bacepannel.Location;
                    moveMyChildren(mypos);
                }
                Debugger.Log(0, null, e.X + "," + e.Y + "\n");
            }
            else
            {
                bacepannel.Location = new Point(bacepannel.Location.X - (start.X - e.X), bacepannel.Location.Y - (start.Y - e.Y));
                mypos = bacepannel.Location;
            }
            bacepannel.Refresh();
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            label1.MouseMove -= new MouseEventHandler(label1_MouseMove);
            label1.MouseUp -= new MouseEventHandler(label1_MouseUp);
        }

        /// <summary>
        /// setMouseDown events
        /// </summary>
        /// <param name="item"></param>
        private void setmsdown(ref PictureBox item, MouseEventArgs e)
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
    }
}