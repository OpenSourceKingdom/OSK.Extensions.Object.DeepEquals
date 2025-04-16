using System;
using System.Threading.Tasks;
using Moq;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.Ports;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class GenericComparerTests
    {
        #region Variables

        private readonly GenericComparer _comparer;

        #endregion

        #region Constructors

        public GenericComparerTests()
        {
            var mockPropertyComparer = new Mock<IPropertyComparer>();
            mockPropertyComparer.Setup(m => m.CanCompare(It.IsAny<Type>()))
                .Returns(false);

            _comparer = new GenericComparer(mockPropertyComparer.Object);
        }

        #endregion

        #region CanCompare

        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(char))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(short))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(string))]
        [InlineData(typeof(object))]
        [InlineData(typeof(MockEnum))]
        public void CanCompare_DifferentTypes_ReturnsTrue(Type type)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.True(result);
        }

        #endregion

        #region AreDeepEqual    

        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(10, 10, true)]
        [InlineData(1d, 1d, true)]
        [InlineData(-2L, -1L, false)]
        [InlineData(20.12, 20.12, true)]
        [InlineData(20.12, 20.121, false)]
        [InlineData(MockEnum.AwesomeTest, MockEnum.EpicTest, false)]
        [InlineData(MockEnum.Test, MockEnum.AwesomeTest, false)]
        [InlineData(MockEnum.EpicTest, MockEnum.EpicTest, true)]
        [InlineData(MockEnum.Test, MockEnum.Test, true)]
        public void AreDeepEqual_NumericVariations_ReturnsExpectedResult(object a, object b, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.AreDeepEqual(MockComparisonContext.SetupContext(), a, b);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void AreDeepEqual_ActionClass_ReturnsExpectedResult()
        {
            var actionClassA = new ActionClass()
            {
                Action = _ => Task.CompletedTask,
                Action2 = _ => { }
            };
            var actionClassB = new ActionClass()
            {
                Action = _ => Task.CompletedTask,
                Action2 = _ => { }
            };

            _comparer.AreDeepEqual(MockComparisonContext.SetupContext(), actionClassA, actionClassB);
        }

        #endregion
    }
}
