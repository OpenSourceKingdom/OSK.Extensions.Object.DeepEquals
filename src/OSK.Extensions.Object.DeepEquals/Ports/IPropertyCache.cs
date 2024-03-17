using System;
using System.Collections.Generic;
using System.Reflection;
using OSK.Extensions.Object.DeepEquals.Models;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    // Primary Port: Implemented Internally
    public interface IPropertyCache
    {
        IEnumerable<PropertyInfo> GetPropertyInfos(Type type, PropertyComparison propertyComparison);
    }
}
