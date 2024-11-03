using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Extensions.Object.DeepEquals.Ports;
using System;

namespace OSK.Extensions.Object.DeepEquals
{
    /// <summary>
    /// A base class that provides a means of more easily creating a typed <see cref="DeepEqualityComparer{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DeepEqualityComparer<T> : IDeepEqualityComparer<T>
    {
        #region IDeepEqualityComparer

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

        #endregion

        #region Helpers

        public abstract bool AreDeepEqual(DeepComparisonContext context, T a, T b);

        #endregion
    }
}
