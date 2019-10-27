using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    class Exponential
    {
        Random r;
        private int numEl;
        public int A { get; set; }
        public int B { get; set; }
        public int NumEl
        {
            get { return numEl; }
            set { numEl = value; }
        }

        private double intens;
        public double Intens
        {
            get { return intens; }
            set { intens = value; }
        }
        public Exponential(int n, double _intens)
        {
            r = new Random();
            NumEl = n;
            Intens = _intens;
        }
        public Exponential(int n, int a, int b)
        {
            r = new Random();
            NumEl = n;
            A = a;
            B = b;
        }

        public List<double> GenerateList()
        {
            List<double> Res_List = new List<double>();
            double DRandValue = 0.0;
            for (int n = 0; n < numEl; n++)
            {
                DRandValue = Math.Round((-1.0) * (1.0 / Intens) * Math.Log(1.0 - r.NextDouble()), 3);
                Res_List.Add(DRandValue);
            }
            return Res_List;
        }
    }
}
