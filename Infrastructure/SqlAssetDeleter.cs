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

    public void Delete(Asset asset)
    {
        using var context = contextFactory.CreateDbContext();
        
        var existingAssets = context.Assets.Find(asset.Id);

        if (existingAssets is null)
        {
            throw new ArgumentException($"No asset found with that ID: {asset.Id}");
        }

        context.Assets.Remove(asset);
        context.SaveChanges();
    }
}