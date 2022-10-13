using Core.Random;
using System;

namespace Core.Distributor
{
    public class GaussDistributor : IDistributor
    {
        public GaussDistributor(IRandom random, double m, double q, int n = 6)
        {
            Random = random;
            M = m;
            Q = q;
            N = n;
        }

        public IRandom Random { get; }
        public double M { get; }
        public double Q { get; }
        public int N { get; }

        public double Next()
        {
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += Random.Next();
            }
            return M + Q * Math.Sqrt(12 / N) * (sum - N / 2);
        }
    }
}
