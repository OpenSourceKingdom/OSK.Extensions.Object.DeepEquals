using OSK.Extensions.Object.DeepEquals.Options;
using OSK.Hexagonal.MetaData;
using System;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IDeepComparisonBuilder
    {
        IDeepComparisonBuilder WithComparer<T>(IDeepEqualityComparer<T> comparer);

        IDeepComparisonBuilder WithStringComparisonOptions(Action<StringComparisonOptions> options);

        IDeepComparisonBuilder WithEnumerableComparisonOptions(Action<EnumerableComparisonOptions> options);

        IDeepComparisonBuilder WithPropertyComparisonOptions(Action<PropertyComparisonOptions> options);

        IDeepComparisonBuilder WithExecutionOptions(Action<ExecutionOptions> options);
    }
}
