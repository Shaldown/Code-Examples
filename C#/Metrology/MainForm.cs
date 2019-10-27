using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metrology
{

    /*
         1. Генерация значений
         2. Сгруппировать значения по интервалам
         3. Проверить "промахи" на каждом интервале
    */

    public partial class MainForm : Form
    {
        const double MAX_VALUE=999999999;
        const double MIN_VALUE = -999999999;

        private BindingSource bindingSource = new BindingSource();

        private List<Double> _dots = new List<double>();
        private List<double> withoutMiss = new List<double>();
        private List<Tuple<double, bool>> res = new List<Tuple<double, bool>>();
        private Dictionary<double, int> grouped = new Dictionary<double, int>();

        private const string ROMANOV_CRITERIAN = "Романовского";
        private const string CHAUVIN_CRITERIAN = "Шовине";
        private const string THREE_SIGMA_CRITERIAN = "Три сигмы";
        private string[] criterians = new string[] { ROMANOV_CRITERIAN, CHAUVIN_CRITERIAN, THREE_SIGMA_CRITERIAN };

        private ICriterian criterian;

        enum Distributions
        {
            [Description("Нет")]
            None,
            [Description("Нормальное")]
            Normal,
            [Description("Экспоненциальное")]
            Exponential,
            [Description("Равномерное")]
            Uniform
        }

        private void fillComboBox()
        {
            comboBoxDistribution.DataSource = Enum.GetValues(typeof(Distributions))
               .Cast<Enum>()
               .Select(value => new
               {
                   (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                   value
               })
               .OrderBy(item => item.value)
               .Where(value => (Distributions)value.value != Distributions.None)
               .ToList();
            comboBoxDistribution.DisplayMember = "Description";
            comboBoxDistribution.ValueMember = "value";
        }

        private void fillCriterianCombobox()
        {
            comboBoxCriterian.Items.AddRange(criterians);
            comboBoxCriterian.SelectedItem = comboBoxCriterian.Items[0];
        }

        public MainForm()
        {
            InitializeComponent();
            fillComboBox();
            fillCriterianCombobox();
            chart1.Series[0].IsVisibleInLegend = false;
            bindingSource.DataSource = _dots;
            dotsDataGrid.DataSource = bindingSource;
            //tabPage1.Parent = null;
            tabPageExponential.Parent = null;
            tabPageUniform.Parent = null;
        }

        private List<List<double>> groupDots(List<double> sortedDots, int intervalsCount)
        {
            List<List<double>> result = new List<List<double>>();

            double delta = (sortedDots[sortedDots.Count - 1] - sortedDots[0]) / intervalsCount;

            int j = 0;

            for (int i = 0; i < intervalsCount; i++)
            {
                List<double> group = new List<double>();

                while (j < sortedDots.Count && sortedDots[j] <= sortedDots[0] + delta * (i + 1))
                {
                    group.Add(sortedDots[j]);
                    j++;
                }

                if (group.Count > 0) result.Add(group);
            }

            return result;
        }

        private double randomFromGroup(List<double> dots)
        {
            if (dots.Count == 0) return 0;
            Random random = new Random();
            return dots[random.Next(dots.Count - 1)];
        }

        private List<T> shuffle<T>(List<T> list)
        {
            var result = new List<T>();
            int count = list.Count;
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int item = rand.Next(list.Count - 1);
                result.Add(list[item]);
                list.RemoveAt(item);
            }

            return result;
        }

        private List<double> randomFromAll(List<List<double>> dots)
        {
            var result = new List<double>();

            foreach (var list in dots)
            {
                result.Add(randomFromGroup(list));
            }

            return result;
        }
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            switch (comboBoxDistribution.SelectedValue)
            {
                case Distributions.Normal:
                    _dots = new Normal((int)numericUpDownN.Value,
                        (double)numericUpDownExpectedValue.Value,
                        (double)numericUpDownDispersion.Value).GenerateList();
                    break;
                case Distributions.Exponential:
                    _dots = new Exponential((int)numericUpDownN.Value,
                      (double)numericUpDownIntens.Value).GenerateList();
                    break;
                case Distributions.Uniform:
                    int a = (int)numericUpDownUniformA.Value;
                    int b = (int)numericUpDownUniformB.Value;
                    if (a < b)
                        _dots = new Uniform((int)numericUpDownN.Value,
                            a, b).GenerateList();
                    break;
            }
            outputDots();
        }
        private void outputDots()
        {
            _dots.Sort();

            chart1.Series[0].Points.Clear();

            if (_dots.Min() > 0) chart1.ChartAreas[0].AxisY.Minimum = 0;
            else chart1.ChartAreas[0].AxisY.Minimum = _dots.Min();
            chart1.ChartAreas[0].AxisY.Maximum = _dots.Max();
            chart1.Series[0].Points.DataBindY(_dots);

            decimal min = (decimal)_dots.Min();
            decimal max = (decimal)_dots.Max();
            numericUpDownA.Minimum = min;
            numericUpDownA.Maximum = max;
            numericUpDownB.Minimum = min;
            numericUpDownB.Maximum = max;

            numericUpDownA.Value = min;
            numericUpDownB.Value = max;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int lowerIndex;
            int upperIndex;
            double lower = (double)numericUpDownA.Value;
            double upper = (double)numericUpDownB.Value;
            int ind = 0;
            while (_dots[ind] < lower) ind++;
            lowerIndex = ind;
            while (ind < _dots.Count() && _dots[ind] <= upper) ind++;
            ind--;
            upperIndex = ind;

            List<double> rangeList = _dots.GetRange(lowerIndex, upperIndex - lowerIndex + 1);

            switch (comboBoxCriterian.SelectedText)
            {
                case ROMANOV_CRITERIAN:
                    if (rangeList.Count > 10)
                    {
                        MessageBox.Show("Для критерия Романовского должно быть не более 10 точек");
                    }
                    break;
                case CHAUVIN_CRITERIAN:
                    if (rangeList.Count > 10)
                    {
                        MessageBox.Show("Для критерия Шовине должно быть не более 21 точки");
                    }
                        break;
            }

            List<bool> b = criterian.getDropList(rangeList);
            List<bool> bb = new List<bool>();
            withoutMiss.Clear();
            res.Clear();

            for (int i = 0; i < rangeList.Count; i++)
            {
                res.Add(new Tuple<double, bool>(rangeList[i], b[i]));
                if (!b[i]) withoutMiss.Add(rangeList[i]);
            }

            dotsDataGrid.DataSource = res.Select(x => new { dot = x.Item1, check = x.Item2 }).ToList();
        }

        private void comboBoxCriterian_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBoxCriterian.SelectedText)
            {
                case ROMANOV_CRITERIAN:
                    criterian = new RomanovCriterian();
                    break;
                case CHAUVIN_CRITERIAN:
                    criterian = new ChauvinCriterian();
                    break;
                case THREE_SIGMA_CRITERIAN:
                    criterian = new ThreeSigma();
                    break;
                default:
                    criterian = new ThreeSigma();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double confidenceInterval = ResultProcessing.getConfidenceInterval(withoutMiss);
            double average = Math.Round(withoutMiss.Average(), 3);
            textBoxResult.Text = "x = " + average.ToString() + " +- " + confidenceInterval.ToString();
        }

        private void comboBoxDistribution_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (TabPage page in tabControl1.TabPages)
                page.Parent = null;
            switch (comboBoxDistribution.SelectedValue)
            {
                case Distributions.Normal:
                    tabPageNormal.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPageNormal;
                    break;
                case Distributions.Exponential:
                    tabPageExponential.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPageExponential;
                    break;
                case Distributions.Uniform:
                    tabPageUniform.Parent = tabControl1;
                    tabControl1.SelectedTab = tabPageUniform;
                    break;
            }
        }

        private void buttonManual_Click(object sender, EventArgs e)
        {
            string[] mas = textBoxManual.Text.Split(' ');
            List<double> dots = new List<double>();
            double d;
            bool b = true;
            foreach (string s in mas)
            {
                if (!String.IsNullOrWhiteSpace((s)))
                {
                    b = Double.TryParse(s, out d);
                    if (b)
                        dots.Add(d);
                    else
                    {
                        MessageBox.Show("Проверьте введённые данные");
                        break;
                    }
                    //dots.Add(Double.Parse(s));
                }
            }
            if (b && dots.Count>0)
            {
                _dots = dots;
                outputDots();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            int ind = ((TextBox)sender).SelectionStart;
            //Ввод только цифр, backspace, запятых, минусов и пробелов
            if (!Char.IsDigit(number) && number != 8 && number != 32
                && number != 44 && number != 45)
            {
                e.Handled = true;
            }
            else
            {
                string s = textBoxManual.Text;
                string newS = "";
                int newInd = 0;
                string[] mas = s.Split(' ');
                int start = -1;
                int end = -1;
                if (ind > 0)
                {
                    start = s.LastIndexOf(' ', ind - 1);
                    start++;
                }
                else start = 0;
                end = s.IndexOf(' ', ind);
                if (end == -1) end = s.Length - 1;

                if (start > -1 && end > -1)
                {
                    newS += s.Substring(start, end - start + 1);
                    newInd = ind - start;
                }
                
                //buttonManual.Text = newS;
                double d = 0;

                //запрет повторного ввода точки в одном числе
                if (number == 44 && newS != "" && newS.Contains(","))
                {
                    e.Handled = true;
                }
                //запрет ввода нескольких пробелов подряд
                else if (number == 32 && s.Length > 0 && number == s[s.Length - 1])
                {
                    e.Handled = true;
                }
                //- только в начале числа
                else if (number == 45 && ind > 0 && s[ind - 1] != ' ')
                {
                    e.Handled = true;
                }
                //только один минус
                else if (number == 45 && newS != "" && newS.Contains("-"))
                {
                    e.Handled = true;
                }
                //проверка, будет ли находиться число в границах 
                else
                {
                    newS = newS.Insert(newInd, number.ToString());
                    if (Double.TryParse(newS, out d) && Char.IsDigit(number) && 
                        (d > MAX_VALUE || d< MIN_VALUE))
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
