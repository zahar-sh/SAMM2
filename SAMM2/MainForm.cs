using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core.Distributor;
using Core.Histogram;
using Core.Random;

namespace SAMM1
{
    public partial class MainForm : Form
    {
        private const string DISTRIBUTION_SERIES_NAME = "DistributorSeries";

        public MainForm()
        {
            InitializeComponent();
            DistributorSeries = chart.Series.FindByName(DISTRIBUTION_SERIES_NAME);
            DistributorChanged(this, EventArgs.Empty);
        }

        private Series DistributorSeries { get; }

        private void DistributorChanged(object sender, EventArgs e)
        {
            var index = distributionsPanel.SelectedIndex;
            if (index < 0)
                distributionsPanel.SelectedIndex = index = 0;

            var methods = new Action<object, EventArgs>[]{
                ExponentialChanged,
                GammaChanged,
                GaussChanged,
                SimpsonChanged,
                TriangularChanged,
                UniformChanged
            };
            var currentMethod = methods[index];
            currentMethod(sender, e);
        }

        private void ExponentialChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var lambda = decimal.ToDouble(expLambdaInput.Value);
            var distributor = new ExponentialDistributor(random, lambda);
            UpdateDistributorInfo(distributor);
        }

        private void GammaChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var lambda = decimal.ToDouble(gammaLambdaInput.Value);
            var eta = decimal.ToDouble(gammaEtaInput.Value);
            var distributor = new GammaDistributor(random, lambda, eta);
            UpdateDistributorInfo(distributor);
        }

        private void GaussChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var m = decimal.ToDouble(gaussMInput.Value);
            var q = decimal.ToDouble(gaussQInput.Value);
            var n = decimal.ToInt32(gaussNInput.Value);
            var distributor = new GaussDistributor(random, m, q, n);
            UpdateDistributorInfo(distributor);
        }

        private void SimpsonChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var a = decimal.ToDouble(simpsonAInputa.Value);
            var b = decimal.ToDouble(simpsonBInput.Value);
            var distributor = new SimpsonDistributor(random, a, b);
            UpdateDistributorInfo(distributor);
        }

        private void TriangularChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var a = decimal.ToDouble(triangularAInput.Value);
            var b = decimal.ToDouble(triangularBInput.Value);
            var distributor = new TriangularDistributor(random, a, b);
            UpdateDistributorInfo(distributor);
        }

        private void UniformChanged(object sender, EventArgs e)
        {
            var random = GetRandom();
            var a = decimal.ToDouble(uniformAInput.Value);
            var b = decimal.ToDouble(uniformBInput.Value);
            var distributor = new UniformDistributor(random, a, b);
            UpdateDistributorInfo(distributor);
        }

        private IRandom GetRandom()
        {
            var index = randomsPanel.SelectedIndex;
            if (index == 0)
            {
                var a = long.Parse(lehmerAInput.Text);
                var m = long.Parse(lehmerMInput.Text);
                var r = BigInteger.Parse(lehmerRInput.Text);
                return new LehmerRandom(a, m, r);
            }
            var seed = decimal.ToInt32(srSeedInput.Value);
            return new SystemRandom(seed);
        }

        private void UpdateDistributorInfo(IDistributor distributor)
        {
            var maxCount = decimal.ToInt32(distributionCountInput.Value);
            var statistic = DistributionStatistic.Analyze(distributor, maxCount);
            if (statistic == null)
                return;
            UpdateChart(DistributorSeries, statistic.Values);
            expectationValueLabel.Text = statistic.Expectation.ToString();
            dispersionValueLabel.Text = statistic.Dispersion.ToString();
            msdValueLabel.Text = Math.Sqrt(statistic.Dispersion).ToString();
        }

        private void UpdateChart(Series series, IList<double> numbers)
        {
            var interval = decimal.ToDouble(intervalInput.Value);
            var histogram = Histogram.BuildHistogram(numbers, interval);
            if (histogram is null)
                return;
            series.Points.Clear();
            foreach (var pair in histogram)
            {
                series.Points.AddXY(pair.Value, Math.Round(pair.Key, 3));
            }
            chart.ResetAutoValues();
        }
    }
}
