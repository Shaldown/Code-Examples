using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{

    class RomanovCriterian : ICriterian
    {
        Dictionary<Tuple<double, int>, double> table;
        Dictionary<int, int> correctN = new Dictionary<int, int>();
        public RomanovCriterian()
        {
            table = new Dictionary<Tuple<double, int>, double>();
            table.Add(new Tuple<double, int>(0.01, 4), 1.73);
            table.Add(new Tuple<double, int>(0.01, 6), 2.16);
            table.Add(new Tuple<double, int>(0.01, 8), 2.43);
            table.Add(new Tuple<double, int>(0.01, 10), 2.62);
            table.Add(new Tuple<double, int>(0.01, 12), 2.75);
            table.Add(new Tuple<double, int>(0.01, 15), 2.9);
            table.Add(new Tuple<double, int>(0.01, 20), 3.08);

            table.Add(new Tuple<double, int>(0.02, 4), 1.72);
            table.Add(new Tuple<double, int>(0.02, 6), 2.13);
            table.Add(new Tuple<double, int>(0.02, 8), 2.37);
            table.Add(new Tuple<double, int>(0.02, 10), 2.54);
            table.Add(new Tuple<double, int>(0.02, 12), 2.66);
            table.Add(new Tuple<double, int>(0.02, 15), 2.8);
            table.Add(new Tuple<double, int>(0.02, 20), 2.96);

            table.Add(new Tuple<double, int>(0.05, 4), 1.71);
            table.Add(new Tuple<double, int>(0.05, 6), 2.1);
            table.Add(new Tuple<double, int>(0.05, 8), 2.27);
            table.Add(new Tuple<double, int>(0.05, 10), 2.41);
            table.Add(new Tuple<double, int>(0.05, 12), 2.52);
            table.Add(new Tuple<double, int>(0.05, 15), 2.64);
            table.Add(new Tuple<double, int>(0.05, 20), 2.78);

            table.Add(new Tuple<double, int>(0.1, 4), 1.69);
            table.Add(new Tuple<double, int>(0.1, 6), 2);
            table.Add(new Tuple<double, int>(0.1, 8), 2.17);
            table.Add(new Tuple<double, int>(0.1, 10), 2.29);
            table.Add(new Tuple<double, int>(0.1, 12), 2.39);
            table.Add(new Tuple<double, int>(0.1, 15), 2.49);
            table.Add(new Tuple<double, int>(0.1, 20), 2.62);

            correctN.Add(19, 20);
            correctN.Add(18, 20);
            correctN.Add(17, 15);
            correctN.Add(16, 15);
            correctN.Add(14, 15);
            correctN.Add(13, 12);
            correctN.Add(11, 12);
            correctN.Add(9, 10);
            correctN.Add(7, 8);
            correctN.Add(5, 6);
            correctN.Add(3, 4);
            correctN.Add(2, 4);
            correctN.Add(1, 4);
        }
        public List<bool> getDropList(List<double> distr, double prob = 0.01)
        {
            List<bool> drop = new List<bool>();
            //какой брать n, если нет в таблице нужного?
            if (distr.Count - 1 <= 20)
            {
                drop = new List<bool>(distr.Count);
                int n = distr.Count - 1;
                double x_mid;
                double sigm;
                double sum;
                for (int i = 0; i < distr.Count; i++)
                {
                    sigm = 0;
                    sum = distr.Sum() - distr[i];
                    x_mid = sum / n;
                    for (int j = 0; j < distr.Count; j++)
                    {
                        if (i != j)
                            sigm += Math.Pow(x_mid - distr[j], 2);
                    }
                    sigm = Math.Sqrt(sigm /( n - 1));
                    double b = Math.Abs(x_mid - distr[i]) / sigm;
                    if (correctN.ContainsKey(n)) n = correctN[n]; //меняем n, чтобы можно было получить результат
                    double bt = table[new Tuple<double, int>(prob, n)];
                    if (b >= bt) drop.Add(true); //необходимо отбросить
                    else drop.Add(false);
                }
            }
            return drop;
        }
    }
}
