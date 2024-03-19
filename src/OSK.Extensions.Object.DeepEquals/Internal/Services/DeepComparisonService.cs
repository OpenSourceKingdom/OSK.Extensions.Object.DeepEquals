using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Internal.Services
{
    internal class DeepComparisonService : IDeepComparisonService
    {
        #region Variables

        private readonly IDeepEqualityComparerProvider _comparerProvider;

        #endregion

        #region Constructors

        public DeepComparisonService(IDeepEqualityComparerProvider comparerProvider)
        {
            _comparerProvider = comparerProvider;
        }

        #endregion

        #region IDeepComparisonService

        public bool AreDeepEqual<T, U>(DeepComparisonContext context, T objA, U objB)
        {
            if (ReferenceEquals(objA, objB))
            {
                return true;
            }
            if (!(objB is T ut) || !(objA is U _))
            {
                return false;
            }
            if (objA == null || objB == null)
            {
                return false;
            }

            if (_comparerProvider.TryGetEqualityComparerOrFallback<T>(objA, out var comparer))
            {
                return ((IDeepEqualityComparer<T>)comparer).AreDeepEqual(context, objA, ut);
            }

            return comparer.AreDeepEqual(context, objA, ut);
        }

        #endregion
    }
}
