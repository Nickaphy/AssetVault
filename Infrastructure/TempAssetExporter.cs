using System.Globalization;
using Application.Ports;
using Core.Entities;

namespace Infrastructure;

public class TempAssetExporter : IAssetExporter
{
    private readonly string filePath;

    public TempAssetExporter(string filePath)
    {
        this.filePath = filePath;
    }

    public void Export(IEnumerable<Asset> assets)
    {
        // Replace the snapshot so exporting the whole list does not duplicate entries.
        using var writer = new StreamWriter(filePath, append: true);
        foreach (Asset asset in assets)
        {
            writer.WriteLine($"ID: {asset.Id}");
            writer.WriteLine($"Name: {asset.Name}");
            writer.WriteLine($"Type: {asset.Type}");
            writer.WriteLine($"Purchase date: {asset.PurchaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}");
            writer.WriteLine($"Assigned: {asset.IsAssigned}");
            writer.WriteLine();
        }
    }
}
