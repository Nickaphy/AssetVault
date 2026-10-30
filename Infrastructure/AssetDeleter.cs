using Application.Ports;
using Core.Entities;

namespace Infrastructure;

public class AssetDeleter : IAssetDelete
{
    private readonly string filePath;

    public AssetDeleter(string filePath)
    {
        this.filePath = filePath;
    }

    // Overwrites the file - the deleted asset.
    public void Delete(Asset assetToDelete)
    {
        // Finish reading before creating any output file.
        var assets = new AssetImporter(filePath).Import().ToList();
        if (!assets.Any(asset => asset.Id == assetToDelete.Id))
            throw new ArgumentException("No asset found with that ID.");

        var remainingAssets = assets
            .Where(asset => asset.Id != assetToDelete.Id)
            .ToList();

        // Reuse the export format, then replace the original only after writing succeeds.
        string temporaryPath = filePath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            new TempAssetExporter(temporaryPath).Export(remainingAssets);
            File.Move(temporaryPath, filePath, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath))
                File.Delete(temporaryPath);
        }
    }
}
