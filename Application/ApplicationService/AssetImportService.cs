using Application.Ports;
using Core.Entities;
namespace Application.ApplicationService;

public class AssetImportService
{
    private readonly IAssetImporter importer;
    
    public AssetImportService(IAssetImporter importer)
    {
        this.importer = importer;
    }

    public IEnumerable<Asset> ImportAssets()
    {
        return importer.Import();
    }
    
}