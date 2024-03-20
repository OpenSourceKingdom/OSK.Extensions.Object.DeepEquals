using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;
using System;

namespace OSK.Extensions.Object.DeepEquals
{
    public abstract class DeepEqualityComparer<T> : IDeepEqualityComparer<T>
    {
        public abstract bool AreDeepEqual(DeepComparisonContext context, T a, T b);

        public bool AreDeepEqual(DeepComparisonContext context, object a, object b)
        {
            try
            {
                return AreDeepEqual(context, (T)a, (T)b);
            }
            catch (Exception ex)
            {
                context.Fail(ex.Message);
                return false;
            }
        }

        public virtual bool CanCompare(Type typeToCompare)
        {
            return typeToCompare == typeof(T);
        }
    }
}
