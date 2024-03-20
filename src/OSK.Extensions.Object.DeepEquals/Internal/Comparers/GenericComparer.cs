using System;
using System.Collections.Generic;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class GenericComparer : IGenericEqualityComparer
    {
        #region Variables

        private readonly IPropertyComparer _propertyComparer;
        private readonly Dictionary<Type, bool> _isGenericComparisonLookup;

        #endregion

        #region Constructors

        public GenericComparer(IPropertyComparer propertyComparer)
        {
            _propertyComparer = propertyComparer;
            _isGenericComparisonLookup = new Dictionary<Type, bool>();
        }

        #endregion

        #region IDeepEqualityComparer

        /// <inheritdoc />
        public bool CanCompare(Type typeToCompare)
        {
            return true;
        }

        /// <inheritdoc />
        public bool AreDeepEqual(DeepComparisonContext context, object a, object b)
        {
            var aType = a.GetType();
            if (_isGenericComparisonLookup.TryGetValue(aType, out var isGenericComparison))
            {
                return isGenericComparison 
                    ? a.Equals(b)
                    : _propertyComparer.AreDeepEqual(context, a, b);
            }

            isGenericComparison = !_propertyComparer.CanCompare(aType);
            _isGenericComparisonLookup.Add(aType, isGenericComparison);

            return isGenericComparison
                ? a.Equals(b)
                : _propertyComparer.AreDeepEqual(context, a, b);
        }

        #endregion
    }
}
