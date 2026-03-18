using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    [HexagonalIntegration(HexagonalIntegrationType.LibraryProvided)]
    public interface ICircularReferenceMonitor
    {
        bool AddReference(object parent, object child);
    }
}
