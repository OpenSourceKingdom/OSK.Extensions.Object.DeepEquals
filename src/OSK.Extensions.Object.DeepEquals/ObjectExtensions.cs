using OSK.Extensions.Object.DeepEquals.Options;

namespace OSK.Extensions.Object.DeepEquals
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs a typed deep comparison between of two objects/>.
        /// </summary>
        /// <param name="objA">The first object to compare</param>
        /// <param name="objB">The second object to compare against</param>
        /// <param name="comparisonOptionOverrides">Allows overriding the globally configured settings in <see cref="DeepEqualsConfiguration"/></param>
        /// <returns>True/False for whether the objects are deeply equal to each other</returns>
        public static bool DeepEquals<T, U>(this T objA, U objB, DeepComparisonOptions comparisonOptionOverrides = null)
        {
            var comparisonContext = DeepEqualsConfiguration.GetComparisonContext(comparisonOptionOverrides);
            return comparisonContext.AreDeepEqual(objA, objB);
        }
    }
}
