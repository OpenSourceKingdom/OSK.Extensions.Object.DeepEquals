using System;
using System.Collections;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class BooleanComparerTests
    {
        #region Variables

        private readonly BooleanComparer _comparer;

        #endregion

        #region Constructors

        public BooleanComparerTests()
        {
            _comparer = new BooleanComparer();
        }

        #endregion

        #region CanCompare

        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(bool), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void AreDeepEqual_BooleanVariations_ReturnsExpectedResult(bool a, bool b)
        {
            // Arrange/Act
            var result = _comparer.AreDeepEqual(MockComparisonContext.SetupContext(), a, b);

            // Assert
            Assert.Equal(a == b, result);
        }

        #endregion
    }
}
