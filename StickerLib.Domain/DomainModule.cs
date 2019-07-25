using Autofac;
using StickerLib.DAL;
using StickerLib.Domain.Build;
using StickerLib.Domain.Build.Implimentations;
using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;

namespace StickerLib.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DalModule>();

            builder.RegisterType<PageManager>().As<IPageManager>();
            builder.RegisterType<StickerService>().As<IStickerService>();
            builder.RegisterType<GroupService>().As<IGroupService>();

            builder.RegisterType<Nuper>().As<INuper>();
            builder.RegisterType<Shuffler>().As<IShuffler>();
            builder.RegisterType<StickerBuilder>().As<IStickerBuilder>();

            builder.RegisterType<Creator>();
            builder.RegisterType<StickerFactory>();
            
            base.Load(builder);
        }
    }
}
