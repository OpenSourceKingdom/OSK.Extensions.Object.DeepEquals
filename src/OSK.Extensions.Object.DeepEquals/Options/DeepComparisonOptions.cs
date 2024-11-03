using System;
using OSK.Extensions.Object.DeepEquals.Models;

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

        #endregion
    }
}
