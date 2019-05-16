using System;
using System.IO;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using StickerLib.Domain;
using StickerLib.Infrastructure.Common;
using StickerLib.Infrastructure.Entities;
using StickerLib.Infrastructure.Helpers;

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

        public static void InitProperties()
        {
            var properties = StickerLib.Infrastructure.Common.Properties.GetInstance();
            properties.Column = 4;
            properties.Row = 4;
            properties.FileExistRule = FileExistRuleType.Replace;

            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Test sticker library app");
            properties.DirectoryForSaving = directory;

            properties.AutoShufflePattern = true;

            properties.StickerPageSize = new PrintPage("sticker", 105f, 75f, SizeType.Mm);
            properties.PrintPageSize = new PrintPage("a3", 297f, 420f, SizeType.Mm);

            properties.Anchor = AnchorType.TopLeft;
            properties.Orientation = OrientationType.Horizontal;
        }
    }
}