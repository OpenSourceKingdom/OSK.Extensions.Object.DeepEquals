using System;
using OSK.Extensions.Object.DeepEquals.Internal.Services;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class ObjectCacheTests
    {
        #region Variables

        private readonly ObjectCache _cache;

        #endregion

        #region Constructors

        public ObjectCacheTests()
        {
            _cache = new ObjectCache();
        }

        #endregion

        #region Add

        [Fact]
        public void Add_NullKey_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _cache.Add(null, new object()));
        }

        [Fact]
        public void Add_ValidKey_KeyNotInDictionary_ReturnsSuccessfully()
        {
            // Arrange/Act
            _cache.Add(new object(), new object());

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Add_ValidKey_KeyInDictionary_OverwritesOriginal_ReturnsSuccessfully()
        {
            // Arrange
            var key = new object();
            _cache.Add(key, new object());

            // Act
            _cache.Add(key, new object());

            // Assert
            Assert.True(true);
        }

        #endregion

        #region TryGet

        [Fact]
        public void TryGet_NullKey_ThrowsArgumentNullException()
        {
            // Arrange/Act/Assert
            Assert.Throws<ArgumentNullException>(() => _cache.TryGet(null, out _));
        }

        [Fact]
        public void TryGet_ValidKey_KeyNotInDictionary_ReturnsFalseAndNullValue()
        {
            // Arrange
            var key = new object();

            // Act
            var result = _cache.TryGet(key, out var cachedValue);

            // Assert
            Assert.False(result);
            Assert.Null(cachedValue);
        }

        [Fact]
        public void TryGet_ValidKey_KeyInDictionary_ReturnsTrueAndOriginalValue()
        {
            // Arrange
            var key = new object();
            var value = new object();

            _cache.Add(key, value);

            // Act
            var result = _cache.TryGet(key, out var cachedValue);

            // Assert
            Assert.True(result);
            Assert.NotNull(cachedValue);
            Assert.Equal(value, cachedValue);
        }

        #endregion
    }
}
