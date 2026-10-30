using Core.Enums;

namespace Core.Entities;

public class Asset
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public AssetType Type { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public bool IsAssigned { get; set; } = false;
    
    //Constructor
    public Asset(Guid id, string name, AssetType type, DateTime purchaseDate)
    { 
        Id = id;
        Name = name;
        Type = type;
        PurchaseDate = purchaseDate;
    }

    // Create an asset, and assign its ID
    public static Asset CreateAsset( string assetName, AssetType assetType, DateTime purchaseDate )
    {
        Guid id = Guid.NewGuid();
        Asset asset = new Asset(id, assetName, assetType, purchaseDate);
        return asset;
    }
}