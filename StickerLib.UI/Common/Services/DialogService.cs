namespace StickerLib.UI.Common.Services
{
    public class DialogService : IDialog
    {

    }

    public interface IDialog
    {
        void ShowInfo();
        bool ShowRequest();
        void ShowDialog();
    }
}