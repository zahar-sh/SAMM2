using System.Numerics;

namespace Core.Random
{
    public class LehmerRandom : IRandom
    {
        public LehmerRandom(long a, long m, BigInteger r)
        {
            A = a;
            M = m;
            R = r;
        }

        public long A { get; }
        public long M { get; }
        public BigInteger R { get; private set; }

        public virtual double Next()
        {
            R = (A * R) % M;
            return (double)R / M;
        }
    }
}
