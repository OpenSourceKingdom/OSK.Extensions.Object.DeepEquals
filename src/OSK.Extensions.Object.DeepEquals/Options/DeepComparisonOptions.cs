using System;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Options
{
    public class DeepComparisonOptions
    {
        #region Options

        /// <summary>
        /// The <see cref="Models.PropertyComparison"/> rules to use when comparing properties.
        /// </summary>
        public PropertyComparison? PropertyComparison { get; set; }

        public StringComparison? StringComparison { get; set; }

        /// <summary>
        /// Sets whether enumerable order will fail an enumerable comparison.
        /// <remarks>Slower performance may occur if enumerable order is not enforced.</remarks>
        /// </summary>
        public bool? EnforceEnumerableOrdering { get; set; }

        /// <summary>
        /// The set of equality comparers to in addition to the standard defaults.
        /// Ordering of comparers matters; comparers are used on a first come first used basis when a comparer that can compare a type is found.
        /// </summary>
        public IEnumerable<IDeepEqualityComparer> CustomEqualityComparers { get; set; }

        #endregion
    }
}
