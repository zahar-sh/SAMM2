namespace Core.Random
{
    public class SystemRandom : IRandom
    {
        private readonly System.Random random;

        public SystemRandom(int seed)
        {
            random = new System.Random(seed);
        }

        public double Next() => random.NextDouble();
    }
}
