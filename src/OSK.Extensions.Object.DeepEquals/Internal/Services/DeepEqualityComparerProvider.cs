using OSK.Extensions.Object.DeepEquals.Ports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Extensions.Object.DeepEquals.Internal.Services
{
    internal class DeepEqualityComparerProvider : IDeepEqualityComparerProvider
    {
        #region Variables

        private readonly static Type DictionaryType = typeof(IDictionary);
        private readonly static Type StringType = typeof(string);
        private readonly static Type EnumerableType = typeof(IEnumerable);

        private readonly Dictionary<Type, IDeepEqualityComparer> _comparerLookup;
        private readonly Dictionary<Type, (IDeepEqualityComparer, bool)> _resolvedTypeLookup;

        #endregion

        #region Constructors

        public DeepEqualityComparerProvider(Dictionary<Type, IDeepEqualityComparer> comparerLookup)
        {
            _comparerLookup = comparerLookup;
            _resolvedTypeLookup = new Dictionary<Type, (IDeepEqualityComparer,bool)>();
        }

        #endregion

        #region IDeepEqualityComparerProvider

        public bool TryGetEqualityComparerOrFallback<T>(object comparerObject, out IDeepEqualityComparer comparer)
        {
            var objType = comparerObject.GetType();
            if (_resolvedTypeLookup.TryGetValue(objType, out var resolvedLookup))
            {
                comparer = resolvedLookup.Item1;
                return resolvedLookup.Item2;
            }

            var resolvedType = GetGenericCollectionLookupType(comparerObject) ?? objType;
            var canCastToT = false;
            if (_comparerLookup.TryGetValue(resolvedType, out comparer))
            {
                canCastToT = typeof(T) == resolvedType;
            }
            else
            {
                comparer = _comparerLookup.Values.First(c => c.CanCompare(resolvedType));
            }

            _resolvedTypeLookup.Add(objType, (comparer, canCastToT));
            return canCastToT;
        }

        #endregion

        #region Helpers

        private Type GetGenericCollectionLookupType(object comparingObject)
        {
            if (comparingObject is IDictionary)
            {
                return DictionaryType;
            }
            if (comparingObject is string)
            {
                return StringType;
            }
            if (comparingObject is IEnumerable)
            {
                return EnumerableType;
            }

            return null;
        }

        #endregion
    }
}
