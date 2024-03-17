using System;
using System.Collections;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class StringComparerTests
    {
        #region Variables

        private readonly IDeepEqualityComparer _comparer;

        #endregion

        #region Constructors

        public StringComparerTests()
        {
            _comparer = new DeepEquals.Internal.Comparers.StringComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), true)]
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
        [InlineData("Test", "Test", true)]
        [InlineData("", "", true)]
        [InlineData("  Test  ", "  Test  ", true)]
        [InlineData("TEst", "Test", false)]
        [InlineData("  A w E SO M3 tEsT  ", "  a W e so m3 TeSt  ", false)]
        [InlineData("Unknown", "Test", false)]
        public void AreDeepEqual_CaseSensitive_StringVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var options = new DeepComparisonOptions()
            {
                IgnoreCaseSensitivity = false
            };

            // Act
            var result = _comparer.AreDeepEqual(a, b, options);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Test", "Test", true)]
        [InlineData("", "", true)]
        [InlineData("  Test  ", "  Test  ", true)]
        [InlineData("TEst", "Test", true)]
        [InlineData("  A w E SO M3 tEsT  ", "  a W e so m3 TeSt  ", true)]
        [InlineData("Unknown", "Test", false)]
        public void AreDeepEqual_CaseInsensitive_StringVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var options = new DeepComparisonOptions()
            {
                IgnoreCaseSensitivity = true
            };

            // Act
            var result = _comparer.AreDeepEqual(a, b, options);

            // Assert
            Assert.Equal(expectedResult, result);
        }


        #endregion
    }
}
