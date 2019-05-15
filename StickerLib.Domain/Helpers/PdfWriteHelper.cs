using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using Serilog;
using StickerLib.Domain.Common;
using StickerLib.Infrastructure.Common;
using StickerLib.Infrastructure.Entities;
using Config = StickerLib.Infrastructure.Common.Properties;
using Path = System.IO.Path;

namespace StickerLib.Domain.Helpers
{
    internal class PdfWriteHelper
    {
        public static void WriteToFileWithNup(IEnumerable<Sticker> stickers, Queue<Page> pages, string file)
        {
            // check path
            if (string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file))
            {
                Log.Fatal("File for saving can not be empty or nullable! \nPARAM_NAME[ {@name} ]\nPATH_TO_FILE[ {@file} ]", nameof(file), file);
                throw new ArgumentNullException(nameof(file));
            }

            var info = new FileInfo(file);

            // check directory
            CreateDirectoryIfNotExist(info.Directory);

            // check file for on exist
            CheckedFileToExist(ref info);

            var stickerList = stickers.ToList();

            using (var document = new PdfDocument(new PdfWriter(info)))
            {
                var width = Config.GetInstance().PrintPageSize.Width;
                var height = Config.GetInstance().PrintPageSize.Height;

                document.SetDefaultPageSize(new PageSize(width, height));

                foreach (Page page in pages)
                {
                    var canvas = new PdfCanvas(document.AddNewPage());
                    for (int rows = 0; rows < Config.GetInstance().Row; rows++)
                    {
                        for (int cols = 0; cols < Config.GetInstance().Column; cols++)
                        {
                            int index = page.Pages[rows, cols];
                            using (var ms = new MemoryStream(stickerList[index].File))
                            {
                                using (var sticker = new PdfDocument(new PdfReader(ms)))
                                {
                                    float x = Config.GetInstance().StickerPageSize.Width * (cols - 1);
                                    float y = (Config.GetInstance().PrintPageSize.Height -
                                               Config.GetInstance().StickerPageSize.Height) -
                                              (Config.GetInstance().StickerPageSize.Height * rows);
                                    PdfFormXObject pdfFormXObject = sticker.GetFirstPage().CopyAsFormXObject(document);
                                    canvas.AddXObject(pdfFormXObject, x, y);
                                }
                            }
                        }
                    }
                }
            }
        }

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
            CreateDirectoryIfNotExist(info.Directory);

            // check file for on exist
            CheckedFileToExist(ref info);

            // write to document
            int index = 0;
            using (var document = new PdfDocument(new PdfWriter(info)))
                foreach (int i in pattern)
                    using (var ms = new MemoryStream(stickers[i].File))
                    using (var sticker = new PdfDocument(new PdfReader(ms)))
                        sticker.CopyPagesTo(1, ++index, document);

            return true;
        }

        private static void CheckedFileToExist(ref FileInfo info)
        {
            if (info.Exists)
            {
                Log.Information("File for saving is exist!");
                switch (Config.GetInstance().FileExistRule)
                {
                    case FileExistRuleType.Replace:
                        Log.Information("Replace old file: FILE[ {@file} ]", info.FullName);
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
                        string newFile = Path.Combine(info.DirectoryName ?? throw new InvalidOperationException(),
                            name + extantion);
                        info = new FileInfo(newFile);
                        Log.Information("Generate random prefix to file: FILE[ {@file} ]", newFile);
                        break;
                    case FileExistRuleType.None:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void CreateDirectoryIfNotExist(DirectoryInfo info)
        {
            if (info != null)
                if (!info.Exists)
                {
                    Log.Information("Select directory is not exist. Create directory: DIRECTORY_NAME[ {@name} ]",
                        info.FullName);
                    info.Create();
                }
        }

        private static ArrayList GetParams(byte[] file, int rows, int cols, PdfDocument document)
        {
            var list = new ArrayList();

            using (var ms = new MemoryStream(file))
            {
                using (var sticker = new PdfDocument(new PdfReader(ms)))
                {
                    float x = Config.GetInstance().StickerPageSize.Width * (cols - 1);
                    float y = (Config.GetInstance().PrintPageSize.Height -
                               Config.GetInstance().StickerPageSize.Height) -
                              (Config.GetInstance().StickerPageSize.Height * rows);
                    PdfFormXObject pdfFormXObject = sticker.GetFirstPage().CopyAsFormXObject(document);

                    list.Add(pdfFormXObject);
                    list.Add(x);
                    list.Add(y);
                }
            }

            return list;
        }
    }
}