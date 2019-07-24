using System;
using System.Reflection;
using StickerLib.UI.Common.Dialogs.Themes;

namespace StickerLib.UI.Common.Services
{
    public class DialogThemeFactory
    {
        public static IDialogTheme CreateTheme(DialogThemeType type)
        {
            // TODO: create exception
            if (type == DialogThemeType.None) return null;
            if (!Enum.IsDefined(typeof(DialogThemeType), type)) return null;

            // Create theme class
            string themeName = Enum.GetName(typeof(DialogThemeType), type);
            string className = themeName + "Theme";
            var typeName = Assembly.GetExecutingAssembly().GetName().Name + ".Common.Dialogs.Themes." + className;
            IDialogTheme theme = Assembly.GetCallingAssembly().CreateInstance(typeName) as IDialogTheme;
            return theme;
        }
    }
}