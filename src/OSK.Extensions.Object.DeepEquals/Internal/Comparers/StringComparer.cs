using OSK.Extensions.Object.DeepEquals.Models;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class StringComparer : DeepEqualityComparer<string>
    {
        #region DeepEqualityComparer Overrides

        public override bool AreDeepEqual(DeepComparisonContext context, string a, string b)
        {
            return string.Equals(a, b, context.StringComparisonOptions.StringComparison);
        }

        #endregion
    }
}
