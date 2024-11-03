using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IDeepComparisonService
    {
        bool AreDeepEqual<T, U>(DeepComparisonContext context, T objA, U objB);
    }
}
