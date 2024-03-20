using System;
using System.Linq;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.Internal.Comparers
{
    internal class PropertyComparer : IPropertyComparer
    {
        #region DeepEqualityComparer

        public bool CanCompare(Type type)
        {
            return type.IsClass || type.IsValueType && !type.IsPrimitive && type.Namespace != null && !type.Namespace.StartsWith("System.");
        }

        public bool AreDeepEqual(DeepComparisonContext context, object a, object b)
        {
            context.ObjectCache.Add(a, b);
            var properties = context.PropertyCache.GetPropertyInfos(a.GetType(), context.PropertyComparisonOptions.PropertyComparison);
            return properties.All(property =>
            {
                var valueA = property.GetValue(a);
                var valueB = property.GetValue(b);

                var isACircular = context.CircularReferenceMonitor.AddReference(a, valueA);
                var isBCircular = context.CircularReferenceMonitor.AddReference(b, valueB);

                if (isACircular && isBCircular)
                {
                    return context.ObjectCache.TryGet(valueA, out var originalB) && ReferenceEquals(originalB, valueB);
                }
                if (isACircular || isBCircular)
                {
                    return false;
                }

                var result = context.DeepComparisonService.AreDeepEqual(context, valueA, valueB);
                if (!result)
                {
                    context.Fail($"Property {property.Name}, type {property.PropertyType.FullName} was not equal. Value A: {valueA} Value B: {valueB}.");
                }

                return result;
            });
        }

        #endregion
    }
}
