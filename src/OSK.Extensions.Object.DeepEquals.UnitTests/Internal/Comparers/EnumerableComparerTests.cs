using System;
using System.Collections;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class EnumerableComparerTests
    {
        #region Variables

        private readonly EnumerableComparer _comparer;

        #endregion

        #region Constructors

        public EnumerableComparerTests()
        {
            _comparer = new EnumerableComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), true)]
        [InlineData(typeof(ArrayList), true)]
        [InlineData(typeof(object[]), true)]
        [InlineData(typeof(int), false)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_List_OrderEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = true;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderEnforced_Unordered_SameValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "World",
                "Amazing"
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = true;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_Array_OrderEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                3,
                7
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = true;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_Array_OrderEnforced_Unordered_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                7,
                3
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = true;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = false;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Unordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "Hello",
                "Test",
                "World",
                "Amazing"
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = false;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_List_OrderNotEnforced_Unordered_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var testSet1 = new List<string>()
            {
                "Test",
                "Hello",
                "Amazing",
                "World"
            };
            var testSet2 = new List<string>()
            {
                "A",
                "New",
                "Trick",
                "Awaits"
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var s1 = (string)objA;
                var s2 = (string)objB;

                return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = false;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void AreDeepEqual_Array_OrderNotEnforced_Ordered_SameValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                12,
                3,
                7
            };
            var testSet2 = new[]
            {
                1,
                12,
                3,
                7
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = false;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_Array_OrderNotEnforced_Unordered_DifferentValues_ReturnsTrue()
        {
            // Arrange
            var testSet1 = new[]
            {
                1,
                3,
                7,
                12
            };
            var testSet2 = new[]
            {
                1,
                12,
                7,
                3
            };

            var context = MockComparisonContext.SetupContext((_, objA, objB) =>
            {
                var int1 = (int)objA;
                var int2 = (int)objB;

                return int1 == int2;
            });

            context.EnumerableComparisonOptions.EnforceEnumerableOrdering = false;

            // Act
            var result = _comparer.AreDeepEqual(context, testSet1, testSet2);

            // Assert
            Assert.True(result);
        }


        #endregion
    }
}
