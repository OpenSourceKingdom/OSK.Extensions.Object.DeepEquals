using OSK.Extensions.Object.DeepEquals.Models;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    public interface IDeepComparisonService
    {
        bool AreDeepEqual<T, U>(DeepComparisonContext context, T objA, U objB);
    }
}
