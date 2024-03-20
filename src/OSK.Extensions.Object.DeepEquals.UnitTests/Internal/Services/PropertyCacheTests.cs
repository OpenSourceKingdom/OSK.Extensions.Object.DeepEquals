using System;
using System.Linq;
using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class PropertyCacheTests
    {
        #region Variables

        private readonly PropertyCache _cache;

        #endregion

        #region Constructors

        public PropertyCacheTests()
        {
            _cache = new PropertyCache();
        }

        #endregion

        #region GetPropertyInfos

        [Fact]
        public void GetPropertyInfos_NullType_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _cache.GetPropertyInfos(null, PropertyComparison.Default));
        }

        [Fact]
        public void GetPropertyInfos_TypeNotInDictionary_CreatesNewEntryAndReturnsPropertyInfos()
        {
            // Arrange/Act
            var propertyInfos = _cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Assert
            Assert.Equal(4, propertyInfos.Count());
        }

        [Fact]
        public void GetPropertyInfos_TypeInDictionary_ReturnsCachedPropertyInfos()
        {
            // Arrange
            _cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Act
            var propertyInfos = _cache.GetPropertyInfos(typeof(MockStruct), PropertyComparison.Default);

            // Assert
            Assert.Equal(4, propertyInfos.Count());
        }

        [Theory]
        [InlineData(PropertyComparison.Default, 3)]
        [InlineData(PropertyComparison.IncludeStatic, 5)]
        [InlineData(PropertyComparison.IncludeNonPublic, 5)]
        [InlineData(PropertyComparison.IncludeStatic | PropertyComparison.IncludeNonPublic, 7)]
        public void GetPropertyInfos_PropertyComparisons_ReturnsSpecifiedPropertyInfos(PropertyComparison rule, int expectedPropertyCount)
        {
            // Arrange/Act
            var propertyInfos = _cache.GetPropertyInfos(typeof(PropertyCacheTestClass), rule);

            // Assert
            Assert.Equal(expectedPropertyCount, propertyInfos.Count());
        }

        #endregion
    }
}
