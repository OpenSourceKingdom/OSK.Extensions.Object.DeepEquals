using System;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    /// <summary>
    /// A comparer capable of performing a deep comparison between objects
    /// </summary>
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IDeepEqualityComparer
    {
        /// <summary>
        /// Determines whether the comparer can handle comparing objects of a given type
        /// </summary>
        /// <param name="typeToCompare">The type of object that is expected to be used when calling <see cref="AreDeepEqual"/></param>
        /// <returns>True/False</returns>
        bool CanCompare(Type typeToCompare);

        /// <summary>
        /// Performs a deep comparison on the given objects. Objects passed are expected to comply with the types the comparer <see cref="CanCompare"/>
        /// </summary>
        /// <param name="context">The context of the current deep comparison</param>
        /// <param name="a">The first object to compare</param>
        /// <param name="b">The second object to compare against</param>
        /// <returns>True/False for whether a deeply equals b</returns>
        bool AreDeepEqual(DeepComparisonContext context, object a, object b);
    }
}
