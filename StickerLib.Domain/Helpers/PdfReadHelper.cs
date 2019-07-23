using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using Serilog;

namespace StickerLib.Domain.Helpers
{
    public class PdfReadHelper
    {
        public static byte[] ReadToStreamByte(string file)
        {
            return ReadToStreamByte(file, 1);
        }

        public static byte[] ReadToStreamByte(string file, int page)
        {
            Log.Debug("Starting read pdf file: FILE[ {@file} ]", file);
            using (var sourceDocument = new PdfDocument(new PdfReader(file)))
            {
                if (sourceDocument.GetNumberOfPages() == 0)
                {
                    Log.Error("Read document is empty! FILE[ {@file} ]", file);
                    return new byte[0];
                }

                using (var ms = new MemoryStream())
                {
                    var tempDocument = new PdfDocument(new PdfWriter(ms));
                    sourceDocument.CopyPagesTo(page, page, tempDocument);
                    tempDocument.Close();
                    Log.Debug("Ending read file and returned byte stream");
                    return ms.ToArray();
                }
            }
        }

        public static Dictionary<int, byte[]> ReadToDictionary(string file)
        {
            var document = new Dictionary<int, byte[]>();
            Log.Debug("Starting read pdf file: FILE[ {@file} ]", file);
            using (var sourceDocument = new PdfDocument(new PdfReader(file)))
                for (int i = 0; i < sourceDocument.GetNumberOfPages(); i++)
                    using (var ms = new MemoryStream())
                    {
                        var tempDocument = new PdfDocument(new PdfWriter(ms));
                        sourceDocument.CopyPagesTo(i + 1, i + 1, tempDocument);
                        tempDocument.Close();
                        document.Add(i, ms.ToArray());
                    }
            Log.Debug("Ending read file and returned dictionary");
            return document;
        }

        public static int CountPageInDocument(string file)
        {
            using (var sourceDocument = new PdfDocument(new PdfReader(file)))
                return sourceDocument.GetNumberOfPages();
        }

        public static Task<byte[]> ReadToStreamByteAsync(string file, int page)
        {
            return Task.Factory.StartNew(() => ReadToStreamByte(file, page));
        }

        public static Task<byte[]> ReadToStreamByteAsync(string file)
        {
            return Task.Factory.StartNew(() => ReadToStreamByte(file));
        }

        public static Task<Dictionary<int, byte[]>> ReadToDictionaryAsync(string file)
        {
            return Task.Factory.StartNew(() => ReadToDictionary(file));
        }
    }
}