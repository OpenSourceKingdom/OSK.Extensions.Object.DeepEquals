using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.Ports;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class ComparisonScopeProviderTests
    {
        #region CreateScope

        [Fact]
        public void CreateScope_ValidComparisonService_ReturnsScopeComparisonWithObjects()
        {
            // Arrange
            var provider = CreateProvider();
            var objA = new object();
            var objB = 2;
            var options = new DeepComparisonOptions();

            // Act
            var result = provider.CreateScope(objA, objB, options);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(objA, result.A);
            Assert.Equal(objB, result.B);
            Assert.NotNull(result.DeepComparisonService);
            Assert.Equal(options, result.ComparisonOptions);
        }

        #endregion

        #region Helpers

        private IComparisonScopeProvider CreateProvider()
        {
            return new ComparisonScopeProvider();
        }

        #endregion
    }
}
