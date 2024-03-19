using System;
using Moq;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public static class MockComparisonContext
    {
        public static DeepComparisonContext SetupContext()
        {
            return SetupContext((DeepComparisonContext _, object _, object _) => true, out _, out _);
        }

        public static DeepComparisonContext SetupContext(Func<DeepComparisonContext, object, object, bool> comparisonFunction)
        {
            return SetupContext(comparisonFunction, out _, out _);
        }

        public static DeepComparisonContext SetupContext(Func<DeepComparisonContext, object, object, bool> comparisonFunction, 
            out Mock<IObjectCache> mockObjectCache,
            out Mock<ICircularReferenceMonitor> mockCircularRefMonitor)
        {
            var mockComparisonService = new Mock<IDeepComparisonService>();
            mockComparisonService.Setup(
                    m => m.AreDeepEqual(It.IsAny<DeepComparisonContext>(), It.IsAny<object>(), It.IsAny<object>()))
                .Returns(comparisonFunction);
            var mockPropertyCache = new Mock<IPropertyCache>();
            mockPropertyCache.Setup(m => m.GetPropertyInfos(It.IsAny<Type>(), It.IsAny<PropertyComparison>()))
                .Returns((Type type, PropertyComparison _) => type.GetProperties());

            mockObjectCache = new Mock<IObjectCache>();
            mockCircularRefMonitor = new Mock<ICircularReferenceMonitor>();

            return new DeepComparisonContext(mockPropertyCache.Object, mockObjectCache.Object,
                mockCircularRefMonitor.Object, mockComparisonService.Object,
                new StringComparisonOptions(), new EnumerableComparisonOptions(),
                new PropertyComparisonOptions(), new ExecutionOptions());
        }

    }
}
