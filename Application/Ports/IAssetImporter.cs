using Core.Entities;

namespace Application.Ports;

public interface IAssetImporter
{
    IEnumerable<Asset> Import();
}
