using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    
    class Camera
    {
        Punt plaats;
        Size grootte;
        SizeF grootteF;
        double afstand;

        public Camera(Punt plaats, Size grootte, SizeF grootteF, double afstand)
        {
            this.plaats = plaats;
            this.grootte = grootte;
            this.afstand = afstand;
            this.grootteF = grootteF;
        }

        public Vector Licht(int x, int y)
        {
            Punt steun = plaats + new Punt(this.afstand, (x * grootteF.Width) / grootte.Width, -1 * (y * grootteF.Width) / grootte.Width);
            Punt result = steun - plaats;
            result = result / result.Length;
            return new Vector(steun, result);
        }
    }
}
