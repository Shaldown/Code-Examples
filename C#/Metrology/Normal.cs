using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    class Normal
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

        private double mathExp;
        public double MathExp
        {
            get { return mathExp; }
            set { mathExp = value; }
        }

        private double sigma;
        public double Sigma
        {
            get { return sigma; }
            set { sigma = value; }
        }

        public Normal(int n, double me, double s)
        {
            NumEl = n;
            MathExp = me;
            Sigma = s;
            r = new Random();
        }
        public Normal(int n,int a,int b)
        {
            NumEl = n;
            A = a;
            B = b;
            MathExp = (A + B) / 2.0;
            Sigma = (B - A) / 6.0; //из правила трёх сигм
            r = new Random();
        }
        public List<double> GenerateList()
        {
            List<double> Res_List = new List<double>();
            double DSumm = 0.0;
            double DRandValue = 0.0;
            for (int n = 0; n < numEl; n++)
            {
                DSumm = 0;
                for (int i = 0; i <= 12; i++)
                {
                    double R = r.NextDouble();
                   
                    DSumm = DSumm + R;
                }
                DRandValue = Math.Round((MathExp + Sigma * (DSumm - 6)), 3);
                Res_List.Add(DRandValue);
            }
            return Res_List;
        }

    }
}
