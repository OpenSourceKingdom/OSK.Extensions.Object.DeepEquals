using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using System;
using System.Collections.Generic;
using StringComparer = OSK.Extensions.Object.DeepEquals.Internal.Comparers.StringComparer;

namespace OSK.Extensions.Object.DeepEquals.Internal.Services
{
    internal class DeepComparisonBuilder : IDeepComparisonBuilder
    {
        #region Variables

        private Action<StringComparisonOptions> _customStringOptions;
        private Action<EnumerableComparisonOptions> _customEnumerableOptions;
        private Action<PropertyComparisonOptions> _customPropertyOptions;
        private Action<ExecutionOptions> _customExecutionOptions;
        private Dictionary<Type, IDeepEqualityComparer> _comparers;

        #endregion

        #region Constructors

        public DeepComparisonBuilder()
        {
            _comparers = new Dictionary<Type, IDeepEqualityComparer>();
        }

        #endregion

        #region IDeepComparisonBuilder

        public IDeepComparisonBuilder WithComparer<T>(IDeepEqualityComparer<T> comparer)
        {
            var comparerType = typeof(T);
            if (!_comparers.TryGetValue(comparerType, out _))
            {
                _comparers.Add(comparerType, comparer);
            }
            return this;
        }

        public IDeepComparisonBuilder WithStringComparisonOptions(Action<StringComparisonOptions> options)
        {
            _customStringOptions = options;
            return this;
        }

        public IDeepComparisonBuilder WithEnumerableComparisonOptions(Action<EnumerableComparisonOptions> options)
        {
            _customEnumerableOptions = options;
            return this;
        }

        public IDeepComparisonBuilder WithPropertyComparisonOptions(Action<PropertyComparisonOptions> options)
        {
            _customPropertyOptions = options;
            return this;
        }

        public IDeepComparisonBuilder WithExecutionOptions(Action<ExecutionOptions> options)
        {
            _customExecutionOptions = options;
            return this;
        }

        #endregion

        #region Helpers

        internal DeepComparisonContext Reset(DeepComparisonContext context)
        {
            return new DeepComparisonContext(context.PropertyCache, context.ObjectCache,
                new CircularReferenceMonitor(), context.DeepComparisonService,
                context.StringComparisonOptions, context.EnumerableComparisonOptions,
                context.PropertyComparisonOptions, context.ExecutionOptions);
        }

        internal DeepComparisonContext Build(DeepComparisonContext previousContext)
        {
            AddDefaultComparers();

            var executionOptions = GetExecutionOptions();
            var preserveCache = previousContext != null && executionOptions.PreserveCacheBetweenConfigurationChanges;
            var propertyCache = preserveCache
                ? previousContext.PropertyCache
                : new PropertyCache();
            var objectCache = preserveCache
                ? previousContext.ObjectCache
                : new ObjectCache();
            var comparisonService = preserveCache
                ? previousContext.DeepComparisonService
                : new DeepComparisonService(new DeepEqualityComparerProvider(_comparers));

            return new DeepComparisonContext(
                propertyCache,
                objectCache,
                new CircularReferenceMonitor(),
                comparisonService,
                GetStringComparisonOptions(),
                GetEnumerableComparisonOptions(),
                GetPropertyComparisonOptions(),
                executionOptions
                );
        }

        private ExecutionOptions GetExecutionOptions()
        {
            var validationOptions = new ExecutionOptions()
            {
                ThrowOnFailure = false
            };
            if (_customExecutionOptions != null )
            {
                _customExecutionOptions(validationOptions);
            }

            return validationOptions;
        }

        private PropertyComparisonOptions GetPropertyComparisonOptions()
        {
            var propertyComparisonOptions = new PropertyComparisonOptions()
            {
                PropertyComparison = PropertyComparison.Default
            };
            if (_customPropertyOptions != null)
            {
                _customPropertyOptions(propertyComparisonOptions);
            }

            return propertyComparisonOptions;
        }

        private EnumerableComparisonOptions GetEnumerableComparisonOptions()
        {
            var enumerableOptions = new EnumerableComparisonOptions()
            {
                EnforceEnumerableOrdering = false
            };
            if (_customEnumerableOptions != null)
            {
                _customEnumerableOptions(enumerableOptions);
            }

            return enumerableOptions;
        }

        private StringComparisonOptions GetStringComparisonOptions()
        {
            var stringComparisonOptions = new StringComparisonOptions()
            {
                StringComparison = StringComparison.InvariantCultureIgnoreCase
            };
            if (_customStringOptions != null)
            {
                _customStringOptions(stringComparisonOptions);
            }

            return stringComparisonOptions;
        }

        private void AddDefaultComparers()
        {
            WithComparer(new BooleanComparer());
            WithComparer(new DateTimeComparer());
            WithComparer(new StringComparer());
            WithComparer(new EnumerableComparer());
            WithComparer(new DictionaryComparer());

            _comparers.Add(typeof(object), new GenericComparer(new PropertyComparer()));
        }

        #endregion
    }
}
