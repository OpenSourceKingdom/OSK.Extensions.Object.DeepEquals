using OSK.Extensions.Object.DeepEquals.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class EnumerableComparer : DeepEqualityComparer<IEnumerable>
    {
        #region DeepEqualityComparer

        public override bool CanCompare(Type typeToCompare)
        {
            return typeof(IEnumerable).IsAssignableFrom(typeToCompare);
        }

        public override bool AreDeepEqual(DeepComparisonContext context, IEnumerable a, IEnumerable b)
        {
            return context.EnumerableComparisonOptions.EnforceEnumerableOrdering 
                ? CompareOrder(context, a, b) 
                : CompareItems(context, a, b);
        }

        #endregion

        #region Helpers

        private bool CompareOrder(DeepComparisonContext context, IEnumerable a, IEnumerable b)
        {
            var enumeratorA = a.GetEnumerator();
            var enumeratorB = b.GetEnumerator();

            var aHasValue = enumeratorA.MoveNext();
            var bHasValue = enumeratorB.MoveNext();
            var areEqual = aHasValue && bHasValue || !aHasValue && !bHasValue;

            while (areEqual && aHasValue)
            {
                areEqual = context.AreDeepEqual(enumeratorA.Current, enumeratorB.Current);

                if (areEqual)
                {
                    aHasValue = enumeratorA.MoveNext();
                    bHasValue = enumeratorB.MoveNext();
                    areEqual = aHasValue && bHasValue || !aHasValue && !bHasValue;
                    continue;
                }

                break;
            }

            return areEqual;
        }

        private bool CompareItems(DeepComparisonContext context, IEnumerable a, IEnumerable b)
        {
            var enumeratorA = a.GetEnumerator();

            var tempList = new List<object>();

            while (enumeratorA.MoveNext())
            {
                tempList.Add(enumeratorA.Current);
            }

            var enumeratorB = b.GetEnumerator();
            var bSize = 0;

            context.SuppressErrorThrow = true;
            while (enumeratorB.MoveNext())
            {
                bSize++;

                if (tempList.Any(item => context.AreDeepEqual(item, enumeratorB.Current)))
                {
                    continue;
                }

                context.SuppressErrorThrow = false;
                return false;
            }

            context.SuppressErrorThrow = false;
            return tempList.Count == bSize;
        }

        #endregion
    }
}
