using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class CircularReferenceMonitorTests
    {
        #region Variables

        private readonly ICircularReferenceMonitor _circularReferenceMonitor;

        #endregion

        #region Constructors

        public CircularReferenceMonitorTests()
        {
            _circularReferenceMonitor = new CircularReferenceMonitor();
        }

        #endregion

        #region AddReference

        [Fact]
        public void AddReference_NullParent_ReturnsFalse()
        {
            // Arrange/Act
            var result = _circularReferenceMonitor.AddReference(null, new object());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddReference_NullChild_ReturnsFalse()
        {
            // Arrange/Act
            var result = _circularReferenceMonitor.AddReference(new object(), null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Hello", "Hello")]
        [InlineData(1, "Hello")]
        [InlineData(12, 12)]
        [InlineData(MockEnum.AwesomeTest, MockEnum.AwesomeTest)]
        public void AddReference_DifferingNonCircularReferenceTypes_ReturnsFalse(object a, object b)
        {
            // Arrange/Act
            var result = _circularReferenceMonitor.AddReference(a, b);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddReference_NonCircularReferences_ReturnsFalse()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();
            var obj = new TestClass();

            // Act
            var result = _circularReferenceMonitor.AddReference(obj, dictionary);
            var result2 = _circularReferenceMonitor.AddReference(obj, dictionary);

            // Assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void AddReference_SameParentAndChild_CircularReference_ReturnsTrue()
        {
            // Arrange
            var test = new TestClass();

            // Act
            var result = _circularReferenceMonitor.AddReference(test, test);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddReference_DifferentParentAndChild_ChildAlreadyAdded_CircularReference_ReturnsTrue()
        {
            // Arrange
            var test = new TestClass();
            var other = new TestClass();
            var circular = new TestClass();

            // Act
            var isNotCircular = _circularReferenceMonitor.AddReference(test, other);
            var isNotCircular2 = _circularReferenceMonitor.AddReference(other, circular);
            var isCircular = _circularReferenceMonitor.AddReference(circular, test);

            // Assert
            Assert.False(isNotCircular);
            Assert.False(isNotCircular2);
            Assert.True(isCircular);
        }

        #endregion
    }
}
