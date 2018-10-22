using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace house.Data
{
    //create a migration based on dbcontext model changes 
    //dotnet ef migrations add create_identity_tables
    //
    //apply all pending migrations. will create database if not created.
    //dotnet ef database update
    //
    //rollback to specified migration
    //dotnet ef database update LastGoodMigration
    //
    //drop database
    //dotnet ef database drop
    //
    //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/
    //
    //migrate local database in code
    //myDbContext.Database.Migrate()
    //
    //Don't call myDbContext.Database.EnsureCreated() before myDbContext.Database.Migrate(). 
    //EnsureCreated bypasses Migrations to create the schema, which causes Migrate to fail.
    //To use migrations after EnsureCreated call EnsureDeleted or dotnet ef database drop
    //
    //call EnsureDeleted before calling EnsureCreated to drop and recreate database and schema
    //
    public class HouseDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public HouseDbContext(DbContextOptions<HouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<house.Data.House> House { get; set; }
    }
}
