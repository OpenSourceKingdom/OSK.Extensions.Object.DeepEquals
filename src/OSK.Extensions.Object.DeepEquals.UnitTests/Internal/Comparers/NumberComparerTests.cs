using System;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class NumberComparerTests
    {
        #region Variables

        private readonly NumberComparer _comparer;

        #endregion

        #region Constructors

        public NumberComparerTests()
        {
            _comparer = new NumberComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(byte), true)]
        [InlineData(typeof(sbyte), true)]
        [InlineData(typeof(char), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(double), true)]
        [InlineData(typeof(float), true)]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(uint), true)]
        [InlineData(typeof(long), true)]
        [InlineData(typeof(ulong), true)]
        [InlineData(typeof(short), true)]
        [InlineData(typeof(ushort), true)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(MockEnum), false)]
        public void IsComparerType_DifferentTypes_ReturnsExpectedResult(Type type, bool shouldAccept)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(shouldAccept, result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_TypeIsNotNumeric_ThrowsInvalidOperationException()
        {
            // Arrange/Act/Assert
            Assert.Throws<InvalidOperationException>(() =>
                _comparer.AreDeepEqual("Test", "test", new DeepComparisonOptions()));
        }

        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(10, 10, true)]
        [InlineData(1d, 1d, true)]
        [InlineData(-2L, -1L, false)]
        [InlineData(20.12, 20.12, true)]
        [InlineData(20.12, 20.121, false)]
        public void AreDeepEqual_NumericVariations_ReturnsExpectedResult(object a, object b, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.AreDeepEqual(a, b, new DeepComparisonOptions());

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion
    }
}
