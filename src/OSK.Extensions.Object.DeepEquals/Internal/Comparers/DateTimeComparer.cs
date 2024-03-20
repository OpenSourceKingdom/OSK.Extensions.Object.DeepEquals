using OSK.Extensions.Object.DeepEquals.Models;
using System;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class DateTimeComparer : DeepEqualityComparer<DateTime>
    {
        public override bool AreDeepEqual(DeepComparisonContext context, DateTime a, DateTime b)
        {
            return a == b;
        }
    }
}
