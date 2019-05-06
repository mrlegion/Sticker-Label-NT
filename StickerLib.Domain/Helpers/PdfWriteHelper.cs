using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using Serilog;
using StickerLib.Infrastructure.Common;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Helpers
{
    internal class PdfWriteHelper
    {
        public static bool WriteToFile(IEnumerable<Sticker> source, int[] pattern, string file)
        {
            // check collection
            if (source == null)
            {
                Log.Fatal("Collection for writing to file can not be Null: PARAM_NAME[ {@name} ]", nameof(source));
                throw new ArgumentNullException(nameof(source));
            }

            Log.Debug("Convert collection to List");

            var stickers = source.ToList();

            if (stickers.Count == 0)
            {
                Log.Fatal("Collection is empty! PARAM_NAME[ {@name} ]", nameof(source));
                throw new ArgumentException(nameof(source));
            }

            // check path
            if (string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file))
            {
                Log.Fatal("File for saving can not be empty or nullable! \nPARAM_NAME[ {@name} ]\nPATH_TO_FILE[ {@file} ]", nameof(file), file);
                throw new ArgumentNullException(nameof(file));
            }

            var info = new FileInfo(file);

            // check directory
            if (info.Directory != null)
                if (!info.Directory.Exists)
                {
                    Log.Information("Select directory is not exist. Create directory: DIRECTORY_NAME[ {@name} ]", info.Directory.FullName);
                    info.Directory.Create();
                }

            // check file for on exist
            if (info.Exists)
            {
                Log.Information("File for saving is exist!");
                switch (Properties.FileExistRule)
                {
                    case FileExistRuleType.Replace:
                        Log.Information("Replace old file: FILE[ {@file} ]", file);
                        info.Delete();
                        break;
                    case FileExistRuleType.Rename:
                        // Todo: create rename algoritm
                        // send message to UI and showing dialog window to user,
                        // where we ask new name for file, then return this name
                        // and replace him in path string
                        break;
                    case FileExistRuleType.RandomName:
                        var name = Path.GetFileNameWithoutExtension(info.Name);
                        var extantion = Path.GetExtension(info.Name);
                        // add random name to name
                        name += "_" + Path.GetRandomFileName();
                        // get new FileInfo
                        string newFile = Path.Combine(info.DirectoryName ?? throw new InvalidOperationException(), name + extantion);
                        info = new FileInfo(newFile);
                        Log.Information("Generate random prefix to file: FILE[ {@file} ]", newFile);
                        break;
                    case FileExistRuleType.None:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // write to document
            int index = 0;
            using (var document = new PdfDocument(new PdfWriter(info)))
                foreach (int i in pattern)
                    using (var ms = new MemoryStream(stickers[i].File))
                        using (var sticker = new PdfDocument(new PdfReader(ms)))
                            sticker.CopyPagesTo(1, ++index, document);

            return true;
        }
    }
}