using Core.Random;
using System;

namespace Core.Distributor
{
    public class ExponentialDistributor : IDistributor
    {
        public ExponentialDistributor(IRandom random, double lambda)
        {
            Random = random;
            Lambda = lambda;
        }

        public IRandom Random { get; }
        public double Lambda { get; }

        public double Next()
        {
            return -Math.Log(Random.Next()) / Lambda;
        }
    }
}
