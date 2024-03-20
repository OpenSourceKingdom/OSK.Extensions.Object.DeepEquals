using System;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    public interface IDeepEqualityComparerProvider
    {
        public bool TryGetEqualityComparerOrFallback<T>(object comparingObject, 
            out IDeepEqualityComparer comparer);
    }
}
