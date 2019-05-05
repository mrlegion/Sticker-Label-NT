using Autofac;
using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;

namespace StickerLib.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IPageManager>().As<PageManager>();
            builder.RegisterType<IStickerService>().As<StickerService>();
            
            base.Load(builder);
        }
    }
}
