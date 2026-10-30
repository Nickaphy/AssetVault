using System.Globalization;
using Application.ApplicationService;
using Application.Ports;
using Core.Entities;
using Core.Enums;
using Microsoft.VisualBasic.FileIO;

namespace Infrastructure;

public class AssetImporter : IAssetImporter
{
	private readonly string filePath;
	public AssetImporter(string filePath)
	{
		this.filePath = filePath;
	}

	public IEnumerable<Asset> Import()
	{
		List<Asset> assets = new();
		using var reader = new StreamReader(filePath);
		{
			while (true)
			{
				string? idLine = reader.ReadLine();
				// End of file
				if (idLine is null)
				{
					break;
				}

				// Skip blank seperators
				if (string.IsNullOrWhiteSpace(idLine))
				{
					continue;	
				}
				
				string nameLine = reader.ReadLine() ?? throw new FormatException("Incomplete asset record.");
				string typeLine = reader.ReadLine() ?? throw new FormatException("Incomplete asset record.");
				string dataLine = reader.ReadLine() ?? throw new FormatException("Incomplete asset record.");
				string assignedLine = reader.ReadLine() ?? throw new FormatException("Incomplete asset record.");
				
				// Reconstruction Asset object
				
				// Reconstructing the ID
				Guid id = Guid.Parse(Getvalue(idLine));
				string name = Getvalue(nameLine);
				AssetType type = Enum.Parse<AssetType>(Getvalue(typeLine));
				DateTime purchaseDate = DateTime.ParseExact(Getvalue(dataLine), "yyyy-MM-dd", CultureInfo.InvariantCulture);
				bool isAssigned = bool.Parse(Getvalue(assignedLine));

				Asset asset = new Asset(id, name, type, purchaseDate);
				asset.IsAssigned = isAssigned;
				assets.Add(asset);
			}
			return assets;
		}
	}

	// "Name: Office Laptop" becomes "Office Laptop".
	public static string Getvalue(string line)
	{
		int colonPosition = line.IndexOf(':');
		return line[(colonPosition + 1)..].Trim();
	}
}
