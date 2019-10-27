using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    class ThreeSigma : ICriterian
    {
        public List<bool> getDropList(List<double> dots, double prob = 0)
        {
            //какой брать n, если нет в таблице нужного?
            List<bool> drop = new List<bool>(dots.Count);
            int count = dots.Count - 1;
            double avg;
            double sigm;
            double sum;
            for (int i = 0; i < dots.Count; i++)
            {
                sigm = 0;
                sum = dots.Sum() - dots[i];
                avg = sum / count;
                for (int j = 0; j < dots.Count; j++)
                {
                    if (i != j)
                        sigm += Math.Pow(avg - dots[j], 2);
                }
                sigm = Math.Sqrt(sigm / (count - 1));

                if (Math.Abs(avg - dots[i]) > 3 * sigm) drop.Add(true); //необходимо отбросить
                else drop.Add(false);
            }
            return drop;
        }
    }
}
