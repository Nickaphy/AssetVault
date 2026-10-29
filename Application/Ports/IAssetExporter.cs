using Core.Entities;

namespace Application.Ports;

public interface IAssetExporter
{
    void Export(IEnumerable<Asset> assets);
}
