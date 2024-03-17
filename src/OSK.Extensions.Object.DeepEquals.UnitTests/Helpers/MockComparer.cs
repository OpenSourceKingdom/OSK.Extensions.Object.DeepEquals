using System;
using OSK.Extensions.Object.DeepEquals.Abstracts;
using OSK.Extensions.Object.DeepEquals.Options;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public class MockComparer : DeepEqualityComparer
    {
        public Func<Type, bool> CanCompareFunc { get; set; }
        public Func<object, object, DeepComparisonOptions, bool> EqualsFunc { get; set; }

        protected override bool IsComparerType(Type typeToCompare)
        {
            return CanCompareFunc?.Invoke(typeToCompare) ?? true;
        }

        protected override bool AreDeepEqual(object a, object b)
        {
            return EqualsFunc?.Invoke(a, b, DeepComparisonOptions) ?? true;
        }
    }
}
