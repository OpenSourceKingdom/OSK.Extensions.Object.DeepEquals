using System;
using System.Collections.Generic;
using Moq;
using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class DeepComparisonServiceTests
    {
        #region Variables

        private readonly DeepComparisonService _service;

        #endregion

        #region Constructors

        public DeepComparisonServiceTests()
        {
            _service = new DeepComparisonService();
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_NullObjects_ReturnsTrue()
        {
            // Arrange/Act
            var result = _service.AreDeepEqual(null, null, null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_OneNullOneNonNullObject_ReturnsFalse()
        {
            // Arrange/Act
            var result = _service.AreDeepEqual(new object(), null, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_SameObject_ShortCircuits_ReturnsTrue()
        {
            // Arrange
            var objA = new object();

            // Act
            var result = _service.AreDeepEqual(objA, objA, null);

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
            var result = _service.AreDeepEqual(objA, objB, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_NullComparisonOptions_ThrowsArgumentNullException()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => _service.AreDeepEqual(objA, objB, null));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_ComparisonOptionsNullComparers_ThrowsInvalidOperationException()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();
            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = null
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_ComparisonOptionsEmptyComparers_ThrowsInvalidOperationException()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();
            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_NoDeepEqualityComparersOfTheGivenObjectType_ThrowsInvalidOperationException()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();

            var mockComparer = new Mock<IDeepEqualityComparer>();
            mockComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(false);

            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
                {
                    mockComparer.Object
                }
            };

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => _service.AreDeepEqual(objA, objB, options));
        }

        [Fact]
        public void AreDeepEqual_SameObjectTypes_DifferentObjects_DeepEqualityComparerAvailable_ReturnsTrue()
        {
            // Arrange
            var objA = new MockStruct();
            var objB = new MockStruct();

            var mockComparer = new Mock<IDeepEqualityComparer>();
            mockComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(true);
            mockComparer.Setup(m =>
                    m.AreDeepEqual(It.IsAny<object>(), It.IsAny<object>(), It.IsAny<DeepComparisonOptions>()))
                .Returns(true);

            var options = new DeepComparisonOptions()
            {
                DeepEqualityComparers = new List<IDeepEqualityComparer>()
                {
                    mockComparer.Object
                }
            };

            // Act
            var result = _service.AreDeepEqual(objA, objB, options);

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}
