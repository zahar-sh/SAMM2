using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Histogram
{
    public class Histogram
    {
        public static double[] BuildHistogram(IList<double> numbers)
        {
            if (numbers.Count == 0)
            {
                throw new ArgumentException(nameof(numbers));
            }

            var rMin = numbers.Min();
            var rMax = numbers.Max();
            var levelCountArray = new int[20];

            double delta = (rMax - rMin) / levelCountArray.Length;

            for (int i = 0; i < levelCountArray.Length; i++)
            {
                double a = rMin + i * delta;
                double b = rMin + (i + 1) * delta;

                foreach (var number in numbers)
                {
                    if (number > a && number < b)
                    {
                        levelCountArray[i]++;
                    }
                }
            }

            return levelCountArray
                .Select(count => (double)count / numbers.Count)
                .ToArray();
        }
    }
}
