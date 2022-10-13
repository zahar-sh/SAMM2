using Core.Random;

namespace Core.Distributor
{
    public class SimpsonDistributor : IDistributor
    {
        public SimpsonDistributor(IRandom random, double a, double b)
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
            double v = (B / 2) - (A / 2);
            return v * Random.Next() + v * Random.Next() + A;
        }
    }
}
