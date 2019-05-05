using System.Data.Entity;
using System.Reflection;
using Serilog;
using SQLite.CodeFirst;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.DAL.DbContexts
{
    public class StickerDbContext : DbContext
    {
        public StickerDbContext() : base("Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Log.Information("Start create model for DbContext");

            var sqliteDbInitialize = new SqliteCreateDatabaseIfNotExists<StickerDbContext>(modelBuilder);
            Database.SetInitializer(sqliteDbInitialize);

            Log.Information("Adding fluet configuration for entity in DbContext");

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(StickerDbContext)));
        }

        public DbSet<Sticker> Stickers { get; set; }
    }
}