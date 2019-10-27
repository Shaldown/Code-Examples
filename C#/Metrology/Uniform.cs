using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    class Uniform
    {
        Random r;
        private int numEl;
        public int NumEl
        {
            get { return numEl; }
            set { numEl = value; }
        }

        private int min;
        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        private int max;
        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public Uniform(int n, int _min, int _max)
        {
            r = new Random();
            NumEl = n;
            Min = _min;
            Max = _max;
        }

        public List<double> GenerateList()
        {
            List<double> Res_List = new List<double>();
            double DRandValue = 0.0;
            for (int n = 0; n < numEl; n++)
            {
                DRandValue = Math.Round(r.NextDouble() * (max - min) + min,3);
                Res_List.Add(DRandValue);
            }
            return Res_List;
        }
    }
}
