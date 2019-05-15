using Autofac;
using StickerLib.DAL.DbScope.Implementations;
using StickerLib.DAL.DbScope.Interfaces;
using StickerLib.DAL.Repository;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.DAL
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register DbScope
            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>();
            builder.RegisterType<DbContextCollection>().As<IDbContextCollection>();
            builder.RegisterType<DbContextFactory>().As<IDbContextFactory>();
            builder.RegisterType<DbContextReadOnlyScope>().As<IDbContextReadOnlyScope>();
            builder.RegisterType<DbContextScope>().As<IDbContextScope>();
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>();
            builder.RegisterType<AmbientContextSuppressor>();

            // register Repository
            builder.RegisterType<StickerRepository>().As<IRepository<Sticker>>();



            base.Load(builder);
        }
    }
}
