using System;
using Moq;
using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class DeepComparisonServiceTests
    {
        #region Variables

        private readonly Mock<IDeepEqualityComparerProvider> _mockComparerProvider;
        private readonly DeepComparisonService _service;

        #endregion

        #region Constructors

        public DeepComparisonServiceTests()
        {
            _mockComparerProvider = new Mock<IDeepEqualityComparerProvider>();
            _service = new DeepComparisonService(_mockComparerProvider.Object);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_NullObjects_ReturnsTrue()
        {
            // Arrange/Act
            var result = _service.AreDeepEqual(null, (object) null, (object) null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_OneNullOneNonNullObject_ReturnsFalse()
        {
            // Arrange/Act
            var result = _service.AreDeepEqual(MockComparisonContext.SetupContext(), (object) null, 1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_SameObject_ShortCircuits_ReturnsTrue()
        {
            // Arrange
            var objA = new object();

            // Act
            var result = _service.AreDeepEqual(MockComparisonContext.SetupContext(), objA, objA);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_DifferentObjectTypes_ReturnsFalse()
        {
            // Arrange
            var objA = new object();
            var objB = new MockStruct();

            // Act
            var result = _service.AreDeepEqual(MockComparisonContext.SetupContext(), objA, objB);

            // Assert
            Assert.False(result);
        }


        delegate void ComparerFallbackCallbackDelegate(object o, ref IDeepEqualityComparer callback);
        delegate bool ComparerFallbackReturnDelegate(object o, ref IDeepEqualityComparer returnCallback);
        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_DeepEqualityComparerAvailable_ReturnsTrue()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();

            var mockComparer = new Mock<IDeepEqualityComparer<MockStruct>>();
            mockComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(true);
            mockComparer.Setup(m =>
                    m.AreDeepEqual(It.IsAny<DeepComparisonContext>(), It.IsAny<MockStruct>(), It.IsAny<MockStruct>()))
                .Returns(true);

            _mockComparerProvider.Setup(m => m.TryGetEqualityComparerOrFallback<MockStruct>(
                It.IsAny<Type>(), out It.Ref<IDeepEqualityComparer>.IsAny))
                .Callback(new ComparerFallbackCallbackDelegate((object o, ref IDeepEqualityComparer callBack) =>
                {
                    callBack = mockComparer.Object;
                }))
                .Returns(new ComparerFallbackReturnDelegate(
                    (object o, ref IDeepEqualityComparer returnVar) => returnVar != null));

            // Act
            var result = _service.AreDeepEqual(MockComparisonContext.SetupContext(), objA, objB);

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}
