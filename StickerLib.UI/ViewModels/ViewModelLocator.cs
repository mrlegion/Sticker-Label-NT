using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using StickerLib.UI.Views;

namespace StickerLib.UI.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            // base init
            var builder = new ContainerBuilder();

            // register views
            builder.RegisterType<ShellView>();

            // register view models
            builder.RegisterType<ShellViewModel>();

            // register ioc
            var container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        public ShellViewModel Shell
        {
            get { return ServiceLocator.Current.GetInstance<ShellViewModel>(); }
        }
    }
}