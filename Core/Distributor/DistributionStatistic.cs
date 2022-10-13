using System.Collections.Generic;
using System.Linq;

namespace Core.Distributor
{
    public class DistributionStatistic
    {
        private DistributionStatistic(IDistributor distributor, IList<double> values, double excpectation, double dispersion)
        {
            Distributor = distributor;
            Values = values;
            Expectation = excpectation;
            Dispersion = dispersion;
        }

        public IDistributor Distributor { get; }
        public IList<double> Values { get; }
        public double Expectation { get; }
        public double Dispersion { get; }

        public static DistributionStatistic Analyze(IDistributor distributor, int maxCount)
        {
            var numbers = new List<double>(maxCount);
            for (int i = 0; i < maxCount; i++)
            {
                var number = distributor.Next();
                numbers.Add(number);
            }
            var count = numbers.Count;
            var expectation = numbers.Sum() / count;

            var v = numbers
                .Select(x => x - expectation)
                .Select(x => x * x)
                .Sum();
            var dispersion = v / (count - 1);

            return new DistributionStatistic(distributor, numbers, expectation, dispersion);
        }
    }
}
