using OSK.Extensions.Object.DeepEquals.Models;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class BooleanComparer : DeepEqualityComparer<bool>
    {
        public override bool AreDeepEqual(DeepComparisonContext context, bool a, bool b)
        {
            return a == b;
        }
    }
}
