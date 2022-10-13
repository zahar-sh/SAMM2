using Core.Random;

namespace Core.Distributor
{
    public interface IDistributor
    {
        IRandom Random { get; }

        double Next();
    }
}
