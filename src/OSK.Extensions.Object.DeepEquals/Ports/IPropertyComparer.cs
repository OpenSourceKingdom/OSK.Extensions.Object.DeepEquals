using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IPropertyComparer: IDeepEqualityComparer
    {
    }
}
