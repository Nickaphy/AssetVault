using Application.Ports;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SqlAssetImporter : IAssetImporter
{
    private readonly IDbContextFactory<AssetDbContext> contextFactory;

    // Supplies the factory we registered
    public SqlAssetImporter(IDbContextFactory<AssetDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public IEnumerable<Asset> Import()
    {
        using var context = contextFactory.CreateDbContext();
        return context.Assets
            .AsNoTracking() // We only read so no need to track anything
            .ToList(); // Exectued the query
    }
}