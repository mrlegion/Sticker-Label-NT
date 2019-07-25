using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace StickerLib.UI.Common.Services
{
    public interface IDialog
    {
        // Identifier
        string AlertDialogHost { get; set; }
        string LoadingDialogHost { get; set; }
        string CustomDialogHost { get; set; }

        int DelayShortValue { get; set; }

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

        void ShowShortInfo(string message);
        void ShowShortInfo(string title, string message);
        void ShowShortInfo(string title, string message, string identifier);

        void ShowShortSuccess(string message);
        void ShowShortSuccess(string title, string message);
        void ShowShortSuccess(string title, string message, string identifier);

        void ShowShortWarning(string message);
        void ShowShortWarning(string title, string message);
        void ShowShortWarning(string title, string message, string identifier);

        void ShowShortError(string message);
        void ShowShortError(string title, string message);
        void ShowShortError(string title, string message, string identifier);

        void ShowShort(string title, string message, int delay = 1500, DialogThemeType theme = DialogThemeType.Default,
            PackIconKind icon = default, string identifier = null);

        void ShowDialog(string title, string message, PackIconKind icon = default,
            DialogThemeType theme = DialogThemeType.Default, string identifier = null);

        // Open file dialog

        /// <summary>
        /// Показать диалоговое окно выбора файла
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="filters">Фильтры для выбора файла, то есть какой тип файла нужно выбирать</param>
        /// <returns>Путь для выбранного файла в типе строки</returns>
        string OpenFileDialog(string title, IEnumerable<CommonFileDialogFilter> filters);

        /// <summary>
        /// Показать диалоговое окно выбора нескольких файлов
        /// </summary>
        /// <param name="title">Заголовок диалогового окна</param>
        /// <param name="filters">Фильтры для выбора файла, то есть какой тип файла нужно выбирать</param>
        /// <returns>Коллекция путей для выбранных файлов</returns>
        IEnumerable<string> OpenMultiselectFileDialog(string title, IEnumerable<CommonFileDialogFilter> filters);

        string OpenFolderDialog(string title);

        IEnumerable<string> OpenMultiselectFolderDilaog(string title);

        IEnumerable<string> OpenDialog(string title, IEnumerable<CommonFileDialogFilter> filters,
            bool multiselect = false, bool isFolderPicker = false);
    }
}