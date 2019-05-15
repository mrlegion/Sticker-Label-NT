using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using StickerLib.Domain;

namespace StickerLib.Console
{
    internal class StartUp
    {
        public static void InitAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DomainModule>();

            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
    }
}