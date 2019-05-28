using System;
using System.IO;
using System.Windows;
using System.Configuration;
using GalaSoft.MvvmLight.Threading;
using StickerLib.Infrastructure.Entities;
using StickerLib.Infrastructure.Helpers;
using Setup = StickerLib.UI.Properties.Settings;

namespace StickerLib.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherHelper.Initialize();

            // Inint properties object when we start application
            var properties = Infrastructure.Common.Properties.GetInstance();
            properties.Column = Setup.Default.Columns;
            properties.Row = Setup.Default.Rows;
            properties.AutoGenerateColsAndRows = Setup.Default.AutoGenerateColsAndRows;
            
            var stickerPage = new PrintPage(Setup.Default.StickerPageName, 
                Setup.Default.StickerPageWidth, Setup.Default.StickerPageHeight, 
                (SizeType)Setup.Default.MetricType);

            var printPage = new PrintPage(Setup.Default.PrintPageName,
                Setup.Default.PrintPageWidth, Setup.Default.PrintPageHeight,
                (SizeType)Setup.Default.MetricType);

            properties.StickerPageSize = stickerPage;
            properties.PrintPageSize = printPage;

            properties.Anchor = (AnchorType) Setup.Default.Anchor;
            properties.Orientation = (OrientationType) Setup.Default.Orientation;

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Save all settings when we shutdown app
            var properties = Infrastructure.Common.Properties.GetInstance();

            Setup.Default.Columns = properties.Column;
            Setup.Default.Rows = properties.Column;
            Setup.Default.AutoGenerateColsAndRows = properties.AutoGenerateColsAndRows;
            
            // sticker page information
            Setup.Default.StickerPageName = properties.StickerPageSize.Name;
            Setup.Default.StickerPageWidth = properties.StickerPageSize.Width;
            Setup.Default.StickerPageHeight = properties.StickerPageSize.Height;
            
            // print page information
            Setup.Default.PrintPageName = properties.PrintPageSize.Name;
            Setup.Default.PrintPageWidth = properties.PrintPageSize.Width;
            Setup.Default.PrintPageHeight = properties.PrintPageSize.Height;

            Setup.Default.Anchor = (int)properties.Anchor;
            Setup.Default.Orientation = (int)properties.Orientation;

            base.OnExit(e);
        }
    }
}
