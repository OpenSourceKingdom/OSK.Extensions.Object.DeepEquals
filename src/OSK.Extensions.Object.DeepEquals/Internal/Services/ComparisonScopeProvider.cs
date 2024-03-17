﻿using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Internal.Services
{
    internal class ComparisonScopeProvider : IComparisonScopeProvider
    {
        #region IComparisonScopeProvider

        public ScopedComparison CreateScope(object objA, object objB, DeepComparisonOptions options)
        {
            var configuration = new ComparerConfiguration()
            {
                DeepComparisonService = new DeepComparisonService(),
                PropertyCache = new PropertyCache(),
                ObjectCache = new ObjectCache(),
                CircularReferenceMonitor = new CircularReferenceMonitor()
            };

            foreach (var comparer in options.DeepEqualityComparers)
            {
                comparer.SetConfiguration(configuration);
            }

            return new ScopedComparison()
            {
                A = objA,
                B = objB,
                ComparisonOptions = options,
                DeepComparisonService = configuration.DeepComparisonService
            };
        }

    }

    #endregion
}