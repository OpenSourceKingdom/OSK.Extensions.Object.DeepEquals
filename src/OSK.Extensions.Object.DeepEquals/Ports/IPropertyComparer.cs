using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface IPropertyComparer: IDeepEqualityComparer
    {
    }
}
