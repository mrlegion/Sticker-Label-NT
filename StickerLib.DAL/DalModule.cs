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
            builder.RegisterType<IAmbientDbContextLocator>().As<AmbientDbContextLocator>();
            builder.RegisterType<IDbContextCollection>().As<DbContextCollection>();
            builder.RegisterType<IDbContextFactory>().As<DbContextFactory>();
            builder.RegisterType<IDbContextReadOnlyScope>().As<DbContextReadOnlyScope>();
            builder.RegisterType<IDbContextScope>().As<DbContextScope>();
            builder.RegisterType<IDbContextScopeFactory>().As<DbContextScopeFactory>();
            builder.RegisterType<AmbientContextSuppressor>();

            // register Repository
            builder.RegisterType<IRepository<Sticker>>().As<StickerRepository>();

            base.Load(builder);
        }
    }
}
