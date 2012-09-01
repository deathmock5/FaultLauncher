namespace FaultWraper
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            this.Lodingtext1 = new System.Windows.Forms.Label();
            this.Gamenamelabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Lodingtext2 = new System.Windows.Forms.Label();
            this.Lodingtext3 = new System.Windows.Forms.Label();
            this.Lodingtext4 = new System.Windows.Forms.Label();
            this.Lodingtext5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lodingtext1
            // 
            this.Lodingtext1.AutoSize = true;
            this.Lodingtext1.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lodingtext1.ForeColor = System.Drawing.Color.White;
            this.Lodingtext1.Location = new System.Drawing.Point(12, 9);
            this.Lodingtext1.Name = "Lodingtext1";
            this.Lodingtext1.Size = new System.Drawing.Size(132, 25);
            this.Lodingtext1.TabIndex = 0;
            this.Lodingtext1.Text = "initializing";
            // 
            // Gamenamelabel
            // 
            this.Gamenamelabel.AutoSize = true;
            this.Gamenamelabel.Font = new System.Drawing.Font("Quartz MS", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gamenamelabel.ForeColor = System.Drawing.Color.White;
            this.Gamenamelabel.Location = new System.Drawing.Point(332, 176);
            this.Gamenamelabel.Name = "Gamenamelabel";
            this.Gamenamelabel.Size = new System.Drawing.Size(228, 77);
            this.Gamenamelabel.TabIndex = 1;
            this.Gamenamelabel.Text = "fault";
            // 
            // Lodingtext2
            // 
            this.Lodingtext2.AutoSize = true;
            this.Lodingtext2.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lodingtext2.ForeColor = System.Drawing.Color.White;
            this.Lodingtext2.Location = new System.Drawing.Point(12, 34);
            this.Lodingtext2.Name = "Lodingtext2";
            this.Lodingtext2.Size = new System.Drawing.Size(132, 25);
            this.Lodingtext2.TabIndex = 2;
            this.Lodingtext2.Text = "initializing";
            // 
            // Lodingtext3
            // 
            this.Lodingtext3.AutoSize = true;
            this.Lodingtext3.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lodingtext3.ForeColor = System.Drawing.Color.White;
            this.Lodingtext3.Location = new System.Drawing.Point(12, 59);
            this.Lodingtext3.Name = "Lodingtext3";
            this.Lodingtext3.Size = new System.Drawing.Size(132, 25);
            this.Lodingtext3.TabIndex = 3;
            this.Lodingtext3.Text = "initializing";
            // 
            // Lodingtext4
            // 
            this.Lodingtext4.AutoSize = true;
            this.Lodingtext4.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lodingtext4.ForeColor = System.Drawing.Color.White;
            this.Lodingtext4.Location = new System.Drawing.Point(12, 84);
            this.Lodingtext4.Name = "Lodingtext4";
            this.Lodingtext4.Size = new System.Drawing.Size(132, 25);
            this.Lodingtext4.TabIndex = 4;
            this.Lodingtext4.Text = "initializing";
            // 
            // Lodingtext5
            // 
            this.Lodingtext5.AutoSize = true;
            this.Lodingtext5.Font = new System.Drawing.Font("Quartz MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lodingtext5.ForeColor = System.Drawing.Color.White;
            this.Lodingtext5.Location = new System.Drawing.Point(12, 109);
            this.Lodingtext5.Name = "Lodingtext5";
            this.Lodingtext5.Size = new System.Drawing.Size(132, 25);
            this.Lodingtext5.TabIndex = 5;
            this.Lodingtext5.Text = "initializing";
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(572, 262);
            this.Controls.Add(this.Lodingtext5);
            this.Controls.Add(this.Lodingtext4);
            this.Controls.Add(this.Lodingtext3);
            this.Controls.Add(this.Lodingtext2);
            this.Controls.Add(this.Gamenamelabel);
            this.Controls.Add(this.Lodingtext1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            this.Text = "Splash";
            this.TransparencyKey = System.Drawing.Color.DarkRed;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lodingtext1;
        private System.Windows.Forms.Label Gamenamelabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label Lodingtext2;
        private System.Windows.Forms.Label Lodingtext3;
        private System.Windows.Forms.Label Lodingtext4;
        private System.Windows.Forms.Label Lodingtext5;
    }
}