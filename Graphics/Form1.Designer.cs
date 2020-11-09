using System.Drawing;
using System.Windows.Forms;

namespace Graphics
{
    partial class Form1 : Form
    {
        Scherm scherm;
        public Form1() 
        {
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.Paint += teken;
            this.Text = "Graphics";
            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);

            scherm = new Scherm(this.ClientSize);
            this.Controls.Add(scherm);
            this.MaximumSize = this.Size;
        }
        
        public void teken(object o, PaintEventArgs pea)
        {
            
        }
    }
}

