using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using System;

namespace OSK.Extensions.Object.DeepEquals
{
    /// <summary>
    /// A configuration class that can be used to modify, enhance, and ultimately adjust how deep comparison is
    /// handled within equality checks   
    /// </summary>
    public static class DeepEqualsConfiguration
    {
        #region Variables

        private static DeepComparisonContext _comparisonContext;
        private static Action<IDeepComparisonBuilder> _customConfiguration;

        #endregion

        #region Helpers

        /// <summary>
        /// A custom configuration action that will set the global configuration for Deep Equality comparisons
        /// </summary>
        public static Action<IDeepComparisonBuilder> CustomConfiguration
        {
            get => _customConfiguration;
            set
            {
                _customConfiguration = value;
                _comparisonContext = null;
            }
        }

        internal static DeepComparisonContext GetComparisonContext(DeepComparisonOptions optionOverrides = null)
        {
            var deepComparisonBuilder = new DeepComparisonBuilder();
            if (_comparisonContext == null || optionOverrides != null)
            {
                if (_customConfiguration != null)
                {
                    _customConfiguration(deepComparisonBuilder);
                }

                deepComparisonBuilder.ApplyOptionOverrides(optionOverrides);
                _comparisonContext = deepComparisonBuilder.Build(_comparisonContext);
            }
            else
            {
                _comparisonContext = deepComparisonBuilder.Reset(_comparisonContext);
            }
            return _comparisonContext;
        }

        private static void ApplyOptionOverrides(this IDeepComparisonBuilder builder, DeepComparisonOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.PropertyComparison.HasValue)
            {
                builder.WithPropertyComparisonOptions(o =>
                {
                    o.PropertyComparison = options.PropertyComparison.Value;
                });
            }
            if (options.EnforceEnumerableOrdering.HasValue)
            {
                builder.WithEnumerableComparisonOptions(o =>
                {
                    o.EnforceEnumerableOrdering = options.EnforceEnumerableOrdering.Value;
                });
            }
            if (options.StringComparison.HasValue)
            {
                builder.WithStringComparisonOptions(o =>
                {
                    o.StringComparison = options.StringComparison.Value;
                });
            }
        }

        #endregion
    }
}
