﻿using System;
using System.Collections;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Internal.Comparers;
using OSK.Extensions.Object.DeepEquals.UnitTests.Helpers;
using Xunit;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Comparers
{
    public class DateTimeComparerTests
    {
        #region Variables

        private readonly DateTimeComparer _comparer;

        #endregion

        #region Constructors

        public DateTimeComparerTests()
        {
            _comparer = new DateTimeComparer();
        }

        #endregion

        #region IsComparerType

        [Theory]
        [InlineData(typeof(List<>), false)]
        [InlineData(typeof(ArrayList), false)]
        [InlineData(typeof(object[]), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(DateTime), true)]
        public void IsComparerType_TypeVariations_ReturnsExpectedResult(Type type, bool expectedResult)
        {
            // Arrange/Act
            var result = _comparer.CanCompare(type);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion

        #region AreDeepEqual

        [Theory]
        [InlineData("2024-07-24T00:00:00Z", "2024-07-24T00:00:00Z", true)]
        [InlineData("2020-07-24T00:00:00Z", "2024-07-24T00:00:00Z", false)]
        public void AreDeepEqual_DateTimeVariations_ReturnsExpectedResult(string a, string b, bool expectedResult)
        {
            // Arrange
            var dateA = DateTime.Parse(a);
            var dateB = DateTime.Parse(b);

            // Act
            var result = _comparer.AreDeepEqual(MockComparisonContext.SetupContext(), dateA, dateB);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        #endregion
    }
}
