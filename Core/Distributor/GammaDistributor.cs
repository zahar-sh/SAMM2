using Core.Random;
using System;

namespace Core.Distributor
{
    public class GammaDistributor : IDistributor
    {
        public GammaDistributor(IRandom random, double lambda, double eta)
        {
            Random = random;
            Lambda = lambda;
            Eta = eta;
        }

        public IRandom Random { get; }
        public double Lambda { get; }
        public double Eta { get; }

        public double Next()
        {
            double r = 1;
            for (int i = 0; i < Eta; i++)
            {
                r *= Random.Next();
            }
            return -Math.Log(r) / Lambda;
        }
    }
}
