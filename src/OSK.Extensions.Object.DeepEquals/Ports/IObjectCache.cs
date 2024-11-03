using OSK.Hexagonal.MetaData;

namespace OSK.Extensions.Object.DeepEquals.Ports
{
    [HexagonalPort(HexagonalPort.Primary)]
    public interface IObjectCache
    {
        void Add(object key, object value);

        bool TryGet(object key, out object value);
    }
}
