using System;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Models
{
    internal class ScopedComparison
    {
        #region Variables

        public IDeepComparisonService DeepComparisonService { get; set; }

        public object A { get; set; }

        public object B { get; set; }

        public DeepComparisonOptions ComparisonOptions { get; set; }

        #endregion

        #region ScopedComparison

        public bool Compare()
        {
            return DeepComparisonService?.AreDeepEqual(A, B, ComparisonOptions) ?? throw new ArgumentNullException(nameof(DeepComparisonService));
        }

        #endregion
    }
}
