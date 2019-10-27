using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrology
{
    interface ICriterian
    {
        List<bool> getDropList(List<double> distr, double prob = 0.01);
    }
}
