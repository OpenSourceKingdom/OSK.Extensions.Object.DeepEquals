using System.Linq;
using OSK.Extensions.Object.DeepEquals.Options;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Options
{
    public class DeepComparisonOptionsTests
    {
        #region DefaultComparers

        [Fact]
        public void DefaultComparers_RetrievesNewListOfComparers()
        {
            // Arrange
            var listA = DeepComparisonOptions.DefaultComparers;
            var listB = DeepComparisonOptions.DefaultComparers;

            // Act/Assert
            Assert.False(listA.SequenceEqual(listB));
        }

        #endregion
    }
}
