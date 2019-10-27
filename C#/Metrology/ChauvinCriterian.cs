using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    class ChauvinCriterian : ICriterian
    {
        Dictionary<int, int> correctN = new Dictionary<int, int>();
        public ChauvinCriterian()
        {
            correctN.Add(9,10);
            correctN.Add(7, 8);
            correctN.Add(5, 6);
            correctN.Add(3, 4);
            correctN.Add(2, 4);
            correctN.Add(1, 4);

        }
        private bool needDrop(double abs, double sigm, int n)
        {
            double mult = Double.MaxValue;
            //сделать что-то, если n не подходит под нижележащие значения
            switch (n)
            {
                case 4:
                    mult = 1.6 * sigm;
                    break;
                case 6:
                    mult = 1.7 * sigm;
                    break;
                case 8:
                    mult = 1.9 * sigm;
                    break;
                case 10:
                    mult = 2 * sigm;
                    break;
                default:
                    return false;
            }
            return abs > mult;
        }
        public List<bool> getDropList(List<double> distr, double prob = 0)
        {
            //какой брать n, если нет в таблице нужного?
            List<bool> drop = new List<bool>(distr.Count);
            int n = distr.Count;
            double x_mid;
            double sigm;
            double sum;
            if (n <= 10)
            {
                for (int i = 0; i < distr.Count; i++)
                {
                    sigm = 0;
                    sum = distr.Sum();
                    x_mid = sum / n;
                    for (int j = 0; j < distr.Count; j++)
                    {
                        sigm += Math.Pow(x_mid - distr[j], 2);
                    }
                    sigm = Math.Sqrt(sigm / (n - 1));
                    double abs = Math.Abs(x_mid - distr[i]);
                    if (correctN.ContainsKey(n)) n = correctN[n]; //меняем n, чтобы можно было получить результат
                    if (needDrop(abs, sigm, n)) drop.Add(true); //необходимо отбросить
                    else drop.Add(false);
                }
            }
            return drop;
        }
    }
}
