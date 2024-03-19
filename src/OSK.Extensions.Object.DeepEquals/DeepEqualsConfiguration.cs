using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using System;

namespace OSK.Extensions.Object.DeepEquals
{
    public static class DeepEqualsConfiguration
    {
        private static DeepComparisonContext _comparisonContext;
        private static Action<IDeepComparisonBuilder> _customConfiguration;
        public static Action<IDeepComparisonBuilder> AdditionalConfiguration
        {
            get => _customConfiguration;    
            set
            {
                _comparisonContext = null;
                _customConfiguration = value;
            }
        }
        
        internal static DeepComparisonContext GetComparisonContext(DeepComparisonOptions optionOverrides = null)
        {
            var deepComparisonBuilder = new DeepComparisonBuilder();

            if (_comparisonContext == null || optionOverrides != null)
            {
                deepComparisonBuilder.ApplyOptionOverrides(optionOverrides);
                if (_customConfiguration != null)
                {
                    _customConfiguration(deepComparisonBuilder);
                }
                
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

            foreach (var customComparer in options.CustomEqualityComparers)
            {
               // builder.WithComparer(customComparer);
            }
        }
    }
}
