using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    interface IVoorwerp
    {
        Color kleur(Punt p);
        double afstand(Punt p);
        bool reflexief();
        Punt normaal(Punt p);
    }

    class Vlak : IVoorwerp
    {
        double hoogte;
        const double grootte = 2;
        public Vlak(int h)
        {
            hoogte = h;
        }
    //let's see if this works
        public double afstand(Punt p)
        {
            return Math.Abs(p.Z - hoogte);
        }

        public bool reflexief() { return false; }
        public Punt normaal(Punt p) => new Punt(0, 0, 1);
        public Color kleur(Punt p)
        {
            p += new Punt(grootte / 2, grootte / 2, 0);
            int x = (int)(p.X / grootte);
            int y = (int)(p.Y / grootte);
            if (p.X * p.Y < 0)
                x++;
            if ((x + y) % 2 == 0)
                return Color.FromArgb(50,50,50);
            else
                return Color.LightGray;
        }
    }

    class Bol : IVoorwerp
    {
        double radius;
        Punt plek;

        public Bol(Punt p, double r)
        {
            plek = p;
            radius = r;
        }

        public double afstand(Punt p)
        {
            double lengte = (p - plek).Length;
            if (lengte < radius)
                return 0.001;

            return lengte - radius;
        }

        public bool reflexief() => true;

        public Punt normaal(Punt p) => (p - plek)/(p - plek).Length;
        public Color kleur(Punt p) => Color.Orange;
    }
}
