using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IDeepEqualityComparerProvider
    {
        public bool TryGetEqualityComparerOrFallback<T>(object comparingObject, 
            out IDeepEqualityComparer comparer);
    }
}
