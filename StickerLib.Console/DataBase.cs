using System;
using System.Collections.Generic;
using CommonServiceLocator;
using StickerLib.Domain.Helpers;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Console
{
    internal class DataBase
    {
        public static void FillDb(string pdfFile, string txtFile)
        {
            var pdfByteFiles = PdfReadHelper.ReadToDictionary(pdfFile);
            var titles = FileReaderHelper.Read(txtFile);

            var service = ServiceLocator.Current.GetInstance<IStickerService>();

            if (titles.Count != pdfByteFiles.Count) throw new ArgumentOutOfRangeException();

            List<Sticker> stickers = new List<Sticker>();

            for (int i = 0; i < titles.Count; i++)
                stickers.Add(new Sticker(titles[i], pdfByteFiles[i]));

            service.AddRange(stickers);
        }
    }
}