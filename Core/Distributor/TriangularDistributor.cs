using Core.Random;
using System;

namespace Core.Distributor
{
    public class TriangularDistributor : IDistributor
    {
        public TriangularDistributor(IRandom random, double a, double b)
        {
            Random = random;
            A = a;
            B = b;
        }

        public IRandom Random { get; }
        public double A { get; }
        public double B { get; }

        public double Next()
        {
            return A + (B - A) * Math.Max(Random.Next(), Random.Next());
        }
    }
}
