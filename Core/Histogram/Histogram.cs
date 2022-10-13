using System;
using System.Collections.Generic;

namespace Core.Histogram
{
    public class Histogram
    {
        public static IDictionary<double, int> BuildHistogram(IList<double> numbers, double interval)
        {
            if (numbers.Count == 0)
            {
                throw new ArgumentException(nameof(numbers));
            }

            var min = double.MaxValue;
            var max = double.MinValue;
            foreach (var number in numbers)
            {
                min = Math.Min(min, number);
                max = Math.Max(max, number);
            }

            var levelsSize = GetLevel(max, min, interval) + 1;
            var levels = new int[levelsSize];
            foreach (var number in numbers)
            {
                var level = GetLevel(number, min, interval);
                levels[level]++;
            }
            var historgam = new Dictionary<double, int>();
            for (int level = 0; level < levelsSize; level++)
            {
                var count = levels[level];
                if (count != 0)
                {
                    historgam.Add(level * interval, count);
                }
            }
            return historgam;
        }

        private static int GetLevel(double number, double min, double interval)
        {
            return (int)((number - min) / interval);
        }
    }
}
