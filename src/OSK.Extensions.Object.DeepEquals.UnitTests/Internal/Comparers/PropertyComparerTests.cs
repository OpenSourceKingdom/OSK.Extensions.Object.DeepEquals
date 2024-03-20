using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class PropertyComparerTests
    {
        #region Variables

        private readonly PropertyComparer _propertyComparer;

        #endregion

        #region Constructors

        public PropertyComparerTests() 
        {
            _propertyComparer = new PropertyComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(IEnumerable<>), false)]
        [InlineData(typeof(object), true)]
        [InlineData(typeof(Dictionary<string, string>), true)]
        [InlineData(typeof(MockStruct), true)]
        [InlineData(typeof(MockEnum), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _propertyComparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Fact]
        public void AreDeepEqual_ClassesWithEqualProperties_ReturnsTrue()
        {
            // Arrange
            var context = MockComparisonContext.SetupContext((c, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(c, testClass, b);
                    default:
                        return false;
                }
            });

            var testA = new TestClass()
            {
                A = "Hello",
                B = 117,
                Ints = new List<int>()
                {
                    5,
                    7,
                    8
                },
                SubClass = new TestClass()
                {
                    A = "I",
                    B = 8,
                    Ints = new List<int>()
                }
            };
            var testB = new TestClass()
            {
                A = "Hello",
                B = 117,
                Ints = new List<int>()
                {
                    5,
                    7,
                    8
                },
                SubClass = new TestClass()
                {
                    A = "I",
                    B = 8,
                    Ints = new List<int>()
                }
            };

            // Act
            var result = _propertyComparer.AreDeepEqual(context, testA, testB);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_ClassesWithDifferentPropertyValues_ReturnsFalse()
        {
            // Arrange
            var context = MockComparisonContext.SetupContext((c, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(c, testClass, b);
                    default:
                        return false;
                }
            });

            var testA = new TestClass()
            {
                A = "Hello",
                B = 117,
                Ints = new List<int>()
                {
                    5,
                    6,
                    8
                },
                SubClass = new TestClass()
                {
                    A = "I",
                    B = 8,
                    Ints = new List<int>()
                }
            };
            var testB = new TestClass()
            {
                A = "Hello",
                B = 117,
                Ints = new List<int>()
                {
                    5,
                    7,
                    8
                },
                SubClass = new TestClass()
                {
                    A = "I",
                    B = 8,
                    Ints = new List<int>()
                }
            };

            // Act
            var result = _propertyComparer.AreDeepEqual(context, testA, testB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_StructsWithEqualProperties_ReturnsTrue()
        {
            // Arrange
            var context = MockComparisonContext.SetupContext((con, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<long> list:
                        return list.SequenceEqual((List<long>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(con, testClass, b);
                    case MockStruct mockStruct:
                        var c = (MockStruct)objB;
                        return _propertyComparer.AreDeepEqual(con, mockStruct, c);
                    default:
                        return false;
                }
            });

            var testA = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 8,
                    SubClass = new TestClass()
                }
            };
            var testB = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 8,
                    SubClass = new TestClass()
                }
            };

            // Act
            var result = _propertyComparer.AreDeepEqual(MockComparisonContext.SetupContext(), testA, testB);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreDeepEqual_StructsWithDifferentPropertyValues_ReturnsFalse()
        {
            // Arrange
            var context = MockComparisonContext.SetupContext((con, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(con, testClass, b);
                    case MockStruct mockStruct:
                        var c = (MockStruct)objB;
                        return _propertyComparer.AreDeepEqual(con, mockStruct, c);
                    default:
                        return false;
                }
            });

            var testA = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 8,
                    SubClass = new TestClass()
                }
            };
            var testB = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 9,
                    SubClass = new TestClass()
                }
            };

            // Act
            var result = _propertyComparer.AreDeepEqual(context, testA, testB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_OneObjectWithCircularReferenceOneWithout_ReturnsFalse()
        {
            // Arrange
            var testA = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 8,
                    SubClass = new TestClass()
                }
            };
            var testB = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 9,
                    SubClass = new TestClass()
                }
            };

            var context = MockComparisonContext.SetupContext((con, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(con, testClass, b);
                    case MockStruct mockStruct:
                        var c = (MockStruct)objB;
                        return _propertyComparer.AreDeepEqual(con, mockStruct, c);
                    default:
                        return false;
                }
            }, out _, out var mockCircularRefMonitor);

            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<MockStruct>(test => Equals(testA, test)), It.IsAny<object>()))
                .Returns(true);
            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<MockStruct>(test => Equals(testB, test)), It.IsAny<object>()))
                .Returns(false);

            // Act
            var result = _propertyComparer.AreDeepEqual(context, testA, testB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_BothCircularObjects_DifferentOriginalComparisonObjects_ReturnsFalse()
        {
            // Arrange
            var testA = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 8,
                    SubClass = new TestClass()
                }
            };
            var testB = new MockStruct()
            {
                TestString = "Hi",
                TestInt = 123,
                TestLongs = new List<long>()
                {
                    6,
                    9,
                    7
                },
                TestClass = new TestClass()
                {
                    A = "Test",
                    B = 9,
                    SubClass = new TestClass()
                }
            };

            var context = MockComparisonContext.SetupContext((con, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(con, testClass, b);
                    case MockStruct mockStruct:
                        var c = (MockStruct)objB;
                        return _propertyComparer.AreDeepEqual(con, mockStruct, c);
                    default:
                        return false;
                }
            }, out var mockObjectCache, out var mockCircularRefMonitor);

            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<MockStruct>(test => Equals(testA, test)), It.IsAny<object>()))
                .Returns(true);
            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<MockStruct>(test => Equals(testB, test)), It.IsAny<object>()))
                .Returns(true);

            var returnValue = new object();
            mockObjectCache.Setup(m => m.TryGet(It.IsAny<object>(), out returnValue))
                .Returns(true);

            // Act
            var result = _propertyComparer.AreDeepEqual(context, testA, testB);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreDeepEqual_BothCircularObjects_SameOriginalComparisonObjects_ReturnsTrue()
        {
            // Arrange
            var testA = new TestClass()
            {
                A = "Hello",
                B = 17,
                Ints = new List<int>()
                {
                    1,
                    2,
                    3
                }
            };
            testA.SubClass = testA;

            var testB = new TestClass()
            {
                A = "Hello",
                B = 17,
                Ints = new List<int>()
                {
                    1,
                    2,
                    3
                }
            };
            testB.SubClass = testB;

            var context = MockComparisonContext.SetupContext((con, objA, objB) =>
            {
                if (objA == null)
                {
                    return objB == null;
                }

                switch (objA)
                {
                    case string str:
                        return str.Equals((string)objB, StringComparison.Ordinal);
                    case int i:
                        return i == (int)objB;
                    case List<int> list:
                        return list.SequenceEqual((List<int>)objB);
                    case TestClass testClass:
                        var b = (TestClass)objB;
                        return _propertyComparer.AreDeepEqual(con, testClass, b);
                    case MockStruct mockStruct:
                        var c = (MockStruct)objB;
                        return _propertyComparer.AreDeepEqual(con, mockStruct, c);
                    default:
                        return false;
                }
            }, out var mockObjectCache, out var mockCircularRefMonitor);

            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<TestClass>(test => Equals(testA, test)), It.IsAny<TestClass>()))
                .Returns(true);
            mockCircularRefMonitor.Setup(m =>
                    m.AddReference(It.Is<TestClass>(test => Equals(testB, test)), It.IsAny<TestClass>()))
                .Returns(true);

            var returnValue = (object)testB;
            mockObjectCache.Setup(m => m.TryGet(It.IsAny<TestClass>(), out returnValue))
                .Returns(true);

            // Act
            var result = _propertyComparer.AreDeepEqual(MockComparisonContext.SetupContext(), testA, testB);

            // Assert
            Assert.True(result);
        }


        #endregion
    }
}
