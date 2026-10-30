using Application.Ports;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SqlAssetExporter : IAssetExporter
{
    private readonly IDbContextFactory<AssetDbContext> contextFactory;
    
    public SqlAssetExporter(IDbContextFactory<AssetDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Export(IEnumerable<Asset> assets)
    {
        using var context = contextFactory.CreateDbContext();
        context.Assets.AddRange(assets);
        context.SaveChanges();
    }
    
}