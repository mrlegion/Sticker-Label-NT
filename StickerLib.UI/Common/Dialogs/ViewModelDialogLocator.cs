using Autofac;
using CommonServiceLocator;
using StickerLib.UI.Common.Dialogs.ViewModels;
using StickerLib.UI.Common.Dialogs.Views;

namespace StickerLib.UI.Common.Dialogs
{
    public class ViewModelDialogLocator
    {
        static ViewModelDialogLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AlertDialogView>();
            builder.RegisterType<AlertDialogViewModel>();

            builder.Build();
        }

        public AlertDialogViewModel AlertDialog
        {
            get { return ServiceLocator.Current.GetInstance<AlertDialogViewModel>(); }
        }
    }
}