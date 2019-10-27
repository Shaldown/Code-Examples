using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Metrology
{
    class ResultProcessing
    {
        private static double getAverage(List<double> values)
        {
            return values.Average();
        }
        private static double getSigma(List<double> measurements)
        {
            int k = measurements.Count;
            double expValue = getAverage(measurements);
            double sumOfSquares = 0;
            for (int i = 0; i < k; i++)
            {
                sumOfSquares += Math.Pow(measurements[i] - expValue, 2);
            }
            return Math.Sqrt(sumOfSquares / (k * (k - 1)));
        }
        private static double getAbsoluteError(List<double> measurements)
        {
            double avg = getAverage(measurements);
            double sumOfAbs = 0;
            foreach (double m in measurements)
            {
                sumOfAbs += Math.Abs(m - avg);
            }
            return sumOfAbs / measurements.Count;
        }
        public static double getConfidenceInterval(
            List<double> measurements,
            double confidenceProbability = 0.95)
        {
            double average = getAverage(measurements);
            double significanceLevel = 1 - confidenceProbability; //уровень значимости
            int degreesOfFreedom = measurements.Count - 1; //степени свободы
            double t = new Chart().DataManipulator.Statistics.InverseTDistribution(significanceLevel, degreesOfFreedom);
            double absError = t * getSigma(measurements);
            double relError = absError / average;
            return Math.Round(absError,3);
        }
    }
}
