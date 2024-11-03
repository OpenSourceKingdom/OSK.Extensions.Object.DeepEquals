using OSK.Extensions.Object.DeepEquals.Models;
using System;
using System.Collections;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class DictionaryComparer : DeepEqualityComparer<IDictionary>
    {
        private static readonly Type DictionaryType = typeof(IDictionary);

        #region DeepEqualityComparer

        public override bool CanCompare(Type typeToCompare)
        {
            return DictionaryType.IsAssignableFrom(typeToCompare);
        }

        public override bool AreDeepEqual(DeepComparisonContext context, IDictionary a, IDictionary b)
        {
            if (a.Keys.Count != b.Keys.Count)
            {
                return false;
            }

            foreach (var key in a.Keys)
            {
                if (b.Contains(key) && context.AreDeepEqual(a[key], b[key]))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        #endregion

    }
}
