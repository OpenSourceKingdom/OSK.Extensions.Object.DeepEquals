using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests
{
    public class ObjectExtensionsTests
    {
        #region DeepEquals

        [Fact]
        public void DeepEquals_UsesDefaultOptions_RunsSuccessfully()
        {
            // Arrange
            var obj = new object();

            var mockComparisonService = new Mock<IDeepComparisonService>();
            mockComparisonService.Setup(m =>
                    m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns((object objA, object objB, DeepComparisonOptions options) =>
                    options.IgnoreCaseSensitivity == false && options.EnforceEnumerableOrdering
                                                           && options.DeepEqualityComparers.SequenceEqual(
                                                               options.DeepEqualityComparers));

            var mockScopeProvider = new Mock<IComparisonScopeProvider>();
            mockScopeProvider.Setup(m => m.CreateScope(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns((object o, object o2, DeepComparisonOptions options) => new ScopedComparison()
                {
                    ComparisonOptions = options,
                    DeepComparisonService = mockComparisonService.Object
                });

            ObjectExtensions.ComparisonScopeProvider = mockScopeProvider.Object;

            // Act
            var result = obj.DeepEquals(new object());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeepEquals_NullOptionsProvided_ThrowsArgumentNullException()
        {
            // Arrange
            var obj = new object();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => obj.DeepEquals(new object(), null));
        }

        [Fact]
        public void DeepEquals_OptionsProvided_RunsSuccessfully()
        {
            // Arrange
            var obj = new object();
            var comparisonOptions = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
                {
                    new BooleanComparer(),
                    new PropertyComparer()
                },
                IgnoreCaseSensitivity = true,
                EnforceEnumerableOrdering = false
            };

            var mockComparisonService = new Mock<IDeepComparisonService>();
            mockComparisonService.Setup(m =>
                    m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns((object objA, object objB, DeepComparisonOptions options) =>
                    options.IgnoreCaseSensitivity == comparisonOptions.IgnoreCaseSensitivity
                    && options.EnforceEnumerableOrdering == comparisonOptions.EnforceEnumerableOrdering
                    && options.DeepEqualityComparers.SequenceEqual(comparisonOptions.DeepEqualityComparers));

            var mockScopeProvider = new Mock<IComparisonScopeProvider>();
            mockScopeProvider.Setup(m => m.CreateScope(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns((object o, object o2, DeepComparisonOptions options) => new ScopedComparison()
                {
                    ComparisonOptions = options,
                    DeepComparisonService = mockComparisonService.Object
                });

            ObjectExtensions.ComparisonScopeProvider = mockScopeProvider.Object;

            // Act
            var result = obj.DeepEquals(new object(), comparisonOptions);

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}
