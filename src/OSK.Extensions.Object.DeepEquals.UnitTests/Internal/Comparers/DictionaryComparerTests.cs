using System;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class DictionaryComparerTests
    {
        #region Variables

        private readonly DictionaryComparer _comparer;

        #endregion

        #region Constructors

        public DictionaryComparerTests()
        {
            _comparer = new DictionaryComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(Dictionary<string, string>), true)]
        [InlineData(typeof(Dictionary<IEnumerable<long>, Dictionary<long, string>>), true)]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(int), false)]
        public void CanCompare_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentKeyCount_ReturnsFalse()
        {
            // Arrange
            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = _comparer.AreDeepEqual(context, dictionaryA, dictionaryB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentKeys_ReturnsFalse()
        {
            // Arrange
            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", null },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "KeyA", "Value1" },
                { "KeyB", "Value2" }
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = _comparer.AreDeepEqual(context, dictionaryA, dictionaryB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveDifferentValues_ReturnsFalse()
        {
            // Arrange
            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "ValueA" },
                { "Key2", "ValueB" }
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = _comparer.AreDeepEqual(context, dictionaryA, dictionaryB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_DictionariesHaveSameKeysAndValues_ReturnsTrue()
        {
            // Arrange
            var dictionaryA = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };
            var dictionaryB = new Dictionary<string, string>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            // Act
            var result = _comparer.AreDeepEqual(context, dictionaryA, dictionaryB);

            // Assert
            Assert.True(result);
        }

        #endregion
    }
}
