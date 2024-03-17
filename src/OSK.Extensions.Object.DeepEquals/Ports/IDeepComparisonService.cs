using OSK.Extensions.Object.DeepEquals.Options;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface IDeepComparisonService
    {
        bool AreDeepEqual(object objA, object objB, DeepComparisonOptions options);
    }
}
