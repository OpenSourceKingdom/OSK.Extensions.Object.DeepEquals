using System;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class EnumComparerTests
    {
        #region Variables

        private readonly EnumComparer _comparer;

        #endregion

        #region Constructors

        public EnumComparerTests()
        {
            _comparer = new EnumComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(MockStruct), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(object), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(MockEnum), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResults(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Theory]
        [InlineData(MockEnum.AwesomeTest, MockEnum.EpicTest, false)]
        [InlineData(MockEnum.Test, MockEnum.AwesomeTest, false)]
        [InlineData(MockEnum.EpicTest, MockEnum.EpicTest, true)]
        [InlineData(MockEnum.Test, MockEnum.Test, true)]
        public void AreDeepEqual_EnumVariations_ReturnsExpectedResult(MockEnum valueA, MockEnum valueB,
            bool shouldEqual)
        {
            // Arrange/Act
            var result = _comparer.AreDeepEqual(valueA, valueB, new DeepComparisonOptions());

            // Assert
            Assert.Equal(shouldEqual, result);
        }

        #endregion
    }
}
