using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests;

public class EndToEnd
{
    #region End To End

    [Fact]
    public void EndToEnd_TestClass_ReturnsExpectedResults()
    {
        // Arrange
        var testClass1 = new TestClass()
        {
            A = "123",
            B = 2,
            SubClass = new TestClass()
            {
                A = "Abc",
                Ints = [1, 3, 4]
            },
            MockEnum = MockEnum.AwesomeTest
        };

        var testClass2_EnumDifference = new TestClass()
        {
            A = "123",
            B = 2,
            SubClass = new TestClass()
            {
                A = "Abc",
                Ints = [1, 3, 4]
            },
            MockEnum = MockEnum.Test
        };


        // Act

        var isSameObject = testClass1.DeepEquals(testClass1);
        var isEqualObject1_2 = testClass1.DeepEquals(testClass2_EnumDifference);

        // Assert
        Assert.True(isSameObject);
        Assert.False(isEqualObject1_2);
    }

    #endregion
}
