using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Vector
    {
        public Punt steun;
        public Punt richting;
        public Vector(Punt steun, Punt richting)
        {
            this.steun = steun;
            this.richting = richting;
        }
    }
}
