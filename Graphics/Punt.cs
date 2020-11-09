using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    struct Punt
    {
        private double[] info;
        public double Length { get { return this.length(); } }

        public double this[int index] { get { return info[index]; } }
        public double X { get { return info[0]; } }
        public double Y { get { return info[1]; } }
        public double Z { get { return info[2]; } }
        public Punt(double x, double y, double z)
        {
            info = new double[3];
            info[0] = x;
            info[1] = y;
            info[2] = z;
        }

        public static Punt operator -(Punt a, Punt b) => new Punt(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Punt operator +(Punt a, Punt b) => new Punt(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static double operator *(Punt a, Punt b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        public static Punt operator /(Punt a, double b) => new Punt(a.X / b, a.Y / b, a.Z / b);
        public static Punt operator *(Punt a, double b) => new Punt(a.X * b, a.Y * b, a.Z * b);
        public static Punt operator *(double b, Punt a) => a * b;
        private double length() => Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
    }
}
