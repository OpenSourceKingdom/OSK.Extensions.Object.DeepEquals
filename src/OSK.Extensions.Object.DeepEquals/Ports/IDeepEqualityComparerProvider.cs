using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IDeepEqualityComparerProvider
    {
        public bool TryGetEqualityComparerOrFallback<T>(object comparingObject, 
            out IDeepEqualityComparer comparer);
    }
}
