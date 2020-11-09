using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics
{
    class Scherm : UserControl
    {
        Bitmap bm;
        Camera camera;
        const int max = 100;
        const int maxAfstand = 1000;
        const double range = 0.01;
        List<IVoorwerp> objecten;
        public Scherm(Size s)
        {
            objecten = new List<IVoorwerp>();
            objecten.Add(new Vlak(0));
            objecten.Add(new Bol(new Punt(8,0,4), 3));
            objecten.Add(new Bol(new Punt(14,8,10), 4));
            objecten.Add(new Bol(new Punt(6,4,3), 0.7));
            objecten.Add(new Bol(new Punt(22,-6,12), 8));

            this.ClientSize = s;
            bm = new Bitmap(s.Width, s.Height);
            camera = new Camera(new Punt(0,0,5), this.ClientSize, new SizeF(2,((float)(2*this.ClientSize.Height))/this.ClientSize.Width),1);

            this.SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            this.tekenBitmap();
            this.Paint += tekenScherm;
        }

        public void tekenBitmap()
        {
            for (int x = 0; x < this.ClientSize.Width; x++)
                for (int y = 0; y < this.ClientSize.Height; y++)
                    bm.SetPixel(x, y,this.berekenPixel(x,y));
        }

        public Color berekenPixel(int x, int y)
        {
            Vector licht = camera.Licht(x - this.ClientSize.Width/2, y - this.ClientSize.Height/2);
            for(int n = 0; n < max; n++)
            {
                double afstand = maxAfstand;
                foreach(IVoorwerp o in objecten)
                {
                    double nieuw = this.berekenAfstand(licht,o);
                    if (afstand > nieuw)
                        afstand = nieuw;
                    if (nieuw < range)
                    {
                        if (!o.reflexief())
                            return this.bepaalSchaduw(licht.steun, o.kleur(licht.steun));
                        this.kaats(licht, o);
                    }  
                }
                if (afstand == maxAfstand)
                    return this.OutofRange(licht);
                licht.steun = licht.steun + licht.richting * afstand;
            }
            foreach (IVoorwerp o in objecten)
            {
                if (o.afstand(licht.steun) < range * 3)
                    return o.kleur(licht.steun);
            }
            return this.OutofRange(licht);
        }

        public double berekenAfstand(Vector licht, IVoorwerp o)
        {
            Punt normaal = o.normaal(licht.steun);
            if (normaal * licht.richting > 0)
                return maxAfstand;
            return o.afstand(licht.steun);
        }

        public void kaats(Vector licht, IVoorwerp o)
        {
            Punt normaal = o.normaal(licht.steun);
            licht.richting = licht.richting - (2 * (licht.richting * normaal) * normaal);
        }

        public Color OutofRange(Vector v)
        {
            Punt up = new Punt(0, 0, 1);
            double hoek = Math.Acos(up * v.richting);
            int tint = 255 - (int)(hoek * 140);
            if (tint < 25)
                tint = 25;
            return Color.FromArgb(tint, tint, tint);
        }

        public Color bepaalSchaduw(Punt p, Color c)
        {
            Vector licht = new Vector(p, new Punt(0, 0, 1));
            for (int n = 0; n < max; n++)
            {
                double afstand = maxAfstand;
                foreach (IVoorwerp o in objecten)
                {
                    double nieuw = this.berekenAfstand(licht, o);
                    if (afstand > nieuw)
                        afstand = nieuw;
                    if (nieuw < range)
                    {
                        return Color.FromArgb(c.R / 2, c.G / 2, c.B / 2);
                    }
                }
                if (afstand == maxAfstand)
                    return c;
                licht.steun = licht.steun + licht.richting * afstand;
            }
            return c;
        }

        public void tekenScherm(object o, PaintEventArgs pea)
        {
            pea.Graphics.DrawImage(bm,0,0);
        }
    }
}
