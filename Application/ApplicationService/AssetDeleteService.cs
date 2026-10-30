using Application.Ports;
using Core.Entities;

namespace Application.ApplicationService;

public class AssetDeleteService
{
    private readonly IAssetDelete deleter;   
    
    // Constructor recieves the deleter
    public AssetDeleteService(IAssetDelete deleter)
    {
        this.deleter = deleter;
    }

    public void DeleteAsset(Asset asset) 
    {
        deleter.Delete(asset); // Calls the delete Contract
    }
}

