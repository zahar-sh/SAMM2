using Core.Random;

namespace Core.Distributor
{
    public class UniformDistributor : IDistributor
    {
        public UniformDistributor(IRandom random, double a, double b)
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
            return A + (B - A) * Random.Next();
        }
    }
}
