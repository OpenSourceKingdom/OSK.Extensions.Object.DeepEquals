using System;
using OSK.Extensions.Object.DeepEquals.Abstracts;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class EnumComparer : DeepEqualityComparer
    {
        #region DeepeEqualityComparer Overrides

        protected override bool IsComparerType(Type type)
        {
            return type.IsEnum;
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return a.Equals(b);
        }

        #endregion
    }
}
