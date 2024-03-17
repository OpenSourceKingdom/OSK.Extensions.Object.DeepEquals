using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    internal interface IComparisonScopeProvider
    {
        ScopedComparison CreateScope(object objA, object objB, DeepComparisonOptions options);
    }
}
