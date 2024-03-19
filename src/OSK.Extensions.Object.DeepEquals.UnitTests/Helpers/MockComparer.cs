using System;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public class MockComparer : IDeepEqualityComparer
    {
        public Func<Type, bool> CanCompareFunc { get; set; }
        public Func<DeepComparisonContext, object, object, bool> EqualsFunc { get; set; }

        public bool CanCompare(Type typeToCompare)
        {
            return CanCompareFunc?.Invoke(typeToCompare) ?? true;
        }

        public bool AreDeepEqual(DeepComparisonContext context, object a, object b)
        {
            return EqualsFunc?.Invoke(context, a, b) ?? true;
        }
    }
}
