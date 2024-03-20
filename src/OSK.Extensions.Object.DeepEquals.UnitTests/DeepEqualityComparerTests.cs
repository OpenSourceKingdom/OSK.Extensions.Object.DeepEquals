using Moq;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests
{
    public class DeepEqualityComparerTests
    {
        #region IsComparerType

        [Fact]
        public void IsComparerType_TypeNotOfTypedComparer_ReturnsFalse()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            var result = comparer.CanCompare(typeof(float));

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsComparerType_TypeSameAsTypedComparer_ReturnsTrue()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            var result = comparer.CanCompare(typeof(int));

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_ObjectsNotOfComparerType_ReturnsFalse(
            )
        {
            // Arrange
            var comparer = CreateComparer<int>();
            var context = MockComparisonContext.SetupContext();

            // Act
            var result = comparer.AreDeepEqual(context, "Test", "test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_ObjectsComparerType_RunsSuccessfully()
        {
            // Arrange
            var comparer = CreateComparer<int>();

            // Act
            comparer.AreDeepEqual(MockComparisonContext.SetupContext(), 1, 2);

            // Assert
            Assert.True(true);
        }

        #endregion

        #region Helpers

        private DeepEqualityComparer<TType> CreateComparer<TType>()
        {
            var typedEqualityComparer = new Mock<DeepEqualityComparer<TType>>()
            {
                CallBase = true
            };

            return typedEqualityComparer.Object;
        }

        #endregion
    }
}
