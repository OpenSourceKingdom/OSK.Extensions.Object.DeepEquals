using System;
using System.Collections.Generic;
using System.Reflection;
using OSK.Extensions.Object.DeepEquals.Models;
using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IPropertyCache
    {
        IEnumerable<PropertyInfo> GetPropertyInfos(Type type, PropertyComparison propertyComparison);
    }
}
