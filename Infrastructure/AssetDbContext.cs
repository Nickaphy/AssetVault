using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure;

/*
 * DbContext from EF responsible for managing database access and tracks changes of entities
 * options supplies configuration such as which database to connect to (we configure that through DI)
 * DbSet<Asset> Lets us query and save assets. 
 */
public class AssetDbContext : DbContext 
{
    public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options)
    {
        
    }
    public DbSet<Asset> Assets => Set<Asset>();
}