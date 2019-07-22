using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace StickerLib.UI.Common.Services
{
    public interface IDialog
    {
        // Identifier
        string AlertDialogHost { get; set; }
        string LoadingDialogHost { get; set; }
        string CustomDialogHost { get; set; }


        /// <summary>
        /// Показать информационное диалоговое окно
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        void ShowInfo(string title, string message);

        /// <summary>
        /// Показать инфомационное диалоговое окно с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        void ShowInfo(string title, string message, string identifier);

        /// <summary>
        /// Показать диалоговое окно с успешным выполнением
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        void ShowSuccess(string title, string message);

        /// <summary>
        /// Показать диалоговое окно с успешным выполнением с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        void ShowSuccess(string title, string message, string identifier);

        /// <summary>
        /// Показать диалоговое окно ошибки
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        void ShowError(string title, string message);

        /// <summary>
        /// Показать диалоговое окно ошибки с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        void ShowError(string title, string message, string identifier);

        /// <summary>
        /// Показать предупреждающее диалоговое окно
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        void ShowWarning(string title, string message);

        /// <summary>
        /// Показать предупреждающее диалоговое окно с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        void ShowWarning(string title, string message, string identifier);

        /// <summary>
        /// Показать диалоговое окно с возможностью запроса действия от пользователя
        /// </summary>
        /// <param name="title">Заголовок для диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <returns>Ответ от пользователя</returns>
        Task<bool> ShowRequest(string title, string message);

        /// <summary>
        /// Показать диалоговое окно с возможностью запроса действия от пользователя с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок для диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        /// <returns>Ответ от пользователя</returns>
        Task<bool> ShowRequest(string title, string message, string identifier);

        /// <summary>
        /// Показать диалоговое окно для запроса действия от пользователя с выбором заголовков для кнопок действий
        /// </summary>
        /// <param name="title">Заголовок для диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="positiveButtonTitle">Заголовок кнопки для положительного ответа пользователя</param>
        /// <param name="negativeButtonTitle">Заголовок кнопки для отрицательного ответа пользователя</param>
        /// <returns>Ответ от пользователя</returns>
        Task<bool> ShowRequest(string title, string message, string positiveButtonTitle, string negativeButtonTitle);

        /// <summary>
        /// Показать диалоговое окно для запроса действия от пользователя с выбором заголовков для кнопок действий с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="title">Заголовок для диалогового окна</param>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="positiveButtonTitle">Заголовок кнопки для положительного ответа пользователя</param>
        /// <param name="negativeButtonTitle">Заголовок кнопки для отрицательного ответа пользователя</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        /// <returns>Ответ от пользователя</returns>
        Task<bool> ShowRequest(string title, string message, string positiveButtonTitle, string negativeButtonTitle,
            string identifier);

        /// <summary>
        /// Показать диалоговое окно занятости программы
        /// </summary>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="callback"><see cref="Action"/> выполняемый в отдельном потоке программы (например, загрузка данных из базы данных)</param>
        void ShowLoading(string message, Action callback);

        /// <summary>
        /// Показать диалоговое окно занятости программы с указанием идентификатора для <see cref="DialogHost"/>
        /// </summary>
        /// <param name="message">Сообщение диалогового окна</param>
        /// <param name="callback"><see cref="Action"/> выполняемый в отдельном потоке программы (например, загрузка данных из базы данных)</param>
        /// <param name="identifier">Идентификатор для <see cref="DialogHost"/></param>
        void ShowLoading(string message, Action callback, string identifier);

        void ShowDialog(UserControl content);
        void ShowDialog(UserControl content, string title);
        void ShowDialog(UserControl content, string title, string identifier);
        void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme);
        void ShowDialog(string title, string message, PackIconKind icon, SolidColorBrush theme, string identifier);
    }
}