using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace numericMethodsProgram
{
    public partial class Form1 : Form
    {

        private IconButton currentBttn;
        private Panel leftBorderBttn;
        private Form currentChildForm;

        public Form1()
        {
            //Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            leftBorderBttn = new Panel();
            leftBorderBttn.Size = new Size(7,70);
            panelMenu.Controls.Add(leftBorderBttn);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void reset()
        {
            bttnNotActive();
            leftBorderBttn.Visible = false;
            iconCurrentChild.IconChar = IconChar.House;
            iconCurrentChild.IconColor = Color.Thistle;
            lblCurrentChild.Text = "Home Menu";
        }

        private void bttnActive(object senderBttn, Color color)
        {

            if (senderBttn != null)
            {
                Console.WriteLine("done");
                bttnNotActive();
                currentBttn = (IconButton)senderBttn;
                currentBttn.BackColor = Color.FromArgb(43, 39, 59);
                currentBttn.ForeColor = color;
                currentBttn.TextAlign = ContentAlignment.MiddleCenter;
                currentBttn.IconColor = color;
                currentBttn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBttn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBttn.BackColor = color;
                leftBorderBttn.Location = new Point(0, currentBttn.Location.Y);
                leftBorderBttn.Visible = true;
                leftBorderBttn.BringToFront();

                iconCurrentChild.IconChar = currentBttn.IconChar;
                iconCurrentChild.IconColor = color;
                lblCurrentChild.Text = currentBttn.Text;
                lblCurrentChild.ForeColor = currentBttn.ForeColor;
            }
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")] private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")] private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_mouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void bttnNotActive()
        {
            if (currentBttn != null)
            {
                currentBttn.BackColor = Color.FromArgb(43, 39, 59);
                currentBttn.ForeColor = Color.Gainsboro;
                currentBttn.TextAlign = ContentAlignment.MiddleLeft;
                currentBttn.IconColor = Color.Gainsboro;
                currentBttn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBttn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        struct localColours
        {
            public static Color colour1 = Color.FromArgb(119, 230, 123);  // #77e67b
            public static Color colour2 = Color.FromArgb(242, 215, 238);  // #F2D7EE
            public static Color colour3 = Color.FromArgb(98, 191, 237);   // #62BFED
            public static Color colour4 = Color.FromArgb(140, 255, 218);  // #8CFFDA
            public static Color colour5 = Color.FromArgb(240, 101, 67);   // #F06543
            public static Color colour6 = Color.FromArgb(242, 212, 146);  // #F2D492
            public static Color colour7 = Color.FromArgb(247, 214, 224);  // #F7D6E0
            public static Color colour8 = Color.FromArgb(247, 214, 224);  // #F7D6E0
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour1);
            openChildForm(new Matrix());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour2);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour3);
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour4);
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour5);
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour6);
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour7);
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            bttnActive(sender, localColours.colour8);
        }

        void openChildForm (Form child)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(child);
            panelChildForm.Tag = child;
            child.BringToFront();
            child.Show();
        }
        
    }
}
