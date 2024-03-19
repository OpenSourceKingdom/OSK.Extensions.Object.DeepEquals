using System;
using System.Collections.Generic;

namespace OSK.Extensions.Object.DeepEquals
{
    public static class TypeExtensions
    {
        private readonly static ISet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(sbyte),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort)
        };

        public static bool IsNumericType(this Type type) => NumericTypes.Contains(type);
    }
}
