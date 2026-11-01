using Microsoft.EntityFrameworkCore;
using Application.Ports;
using Core.Entities;

namespace Infrastructure;

public class SqlAssetDeleter : IAssetDelete
{
    private readonly IDbContextFactory<AssetDbContext> contextFactory;

    // Supplies the factory 
    public SqlAssetDeleter(IDbContextFactory<AssetDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public bool Delete(Guid assetId)
    {
        using var context = contextFactory.CreateDbContext();
        
        // Find the asset with assetId
        var assetForDeletion = context.Assets.Find(assetId);

        if (assetForDeletion is null)
        {
            return false;
        }
        
        // Remove the found asset
        context.Assets.Remove(assetForDeletion);
        context.SaveChanges();

        return true;
    }
}