using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Models
{
    public class DeepComparisonContext
    {
        #region Variables

        public StringComparisonOptions StringComparisonOptions { get; }

        public EnumerableComparisonOptions EnumerableComparisonOptions { get; }

        public PropertyComparisonOptions PropertyComparisonOptions { get; }

        public ExecutionOptions ExecutionOptions { get; }

        public IPropertyCache PropertyCache { get; }

        public IObjectCache ObjectCache { get; }

        public ICircularReferenceMonitor CircularReferenceMonitor { get; }

        public IDeepComparisonService DeepComparisonService { get; }

        #endregion

        #region Constructors

        public DeepComparisonContext(IPropertyCache propertyCache, IObjectCache objectCache,
            ICircularReferenceMonitor circularReferenceMonitor, IDeepComparisonService deepComparisonService,
            StringComparisonOptions stringComparisonOptions, EnumerableComparisonOptions enumerableComparisonOptions,
            PropertyComparisonOptions propertyComparisonOptions, ExecutionOptions validationOptions)
        {
            PropertyCache = propertyCache;
            ObjectCache = objectCache;
            CircularReferenceMonitor = circularReferenceMonitor;
            DeepComparisonService = deepComparisonService;
            StringComparisonOptions = stringComparisonOptions;
            EnumerableComparisonOptions = enumerableComparisonOptions;
            PropertyComparisonOptions = propertyComparisonOptions;
            ExecutionOptions = validationOptions;
        }

        #endregion

        #region Helpers

        public void Fail(string message)
        {
            if (ExecutionOptions.ThrowOnFailure)
            {
                throw new DeepEqualityComparisonFailedException(message);
            }
        }

        #endregion
    }
}
