using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IDeepEqualityComparer<T>: IDeepEqualityComparer
    {        
        /// <summary>
        /// Performs a deep comparison on the given objects. Objects passed are expected to comply with the types the <see cref="IDeepEqualityComparer"/>
        /// </summary>
        /// <param name="context">The context of the current deep comparison</param>
        /// <param name="a">The first object to compare</param>
        /// <param name="b">The second object to compare against</param>
        /// <returns>True/False for whether a deeply equals b</returns>
        bool AreDeepEqual(DeepComparisonContext context, T a, T b);
    }
}
