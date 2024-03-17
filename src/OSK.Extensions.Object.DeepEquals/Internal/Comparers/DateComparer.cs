using System;
using OSK.Extensions.Object.DeepEquals.Abstracts;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class DateComparer : DeepEqualityComparer
    {
        #region DeepEqualityComparer

        protected override bool IsComparerType(Type type)
        {
            return type == typeof(DateTime);
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return a.Equals(b);
        }

        #endregion
    }
}
