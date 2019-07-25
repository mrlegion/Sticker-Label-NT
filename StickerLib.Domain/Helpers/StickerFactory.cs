using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StickerLib.Infrastructure.Common;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Helpers
{
    public class StickerFactory
    {
        /// <summary>
        /// Create new Instance for Entity Sticker
        /// </summary>
        /// <param name="title">Name of Sticker</param>
        /// <param name="file">Path to pdf file on Sticker</param>
        /// <returns>New instance on Sticker class</returns>
        public Sticker Create(string title, string file)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title), "Title for sticker cannot be Empty");

            if (string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file), "Path to sticker file cannot be empty");

            if (!File.Exists(file))
                throw new FileNotFoundException("File for sticker not found");

            byte[] content = PdfReadHelper.ReadToStreamByte(file);
            if (content.Length == 0)
                throw new ArgumentException("In file sticker not found pages or content", nameof(content));

            return new Sticker(title, content);
        }

        /// <summary>
        /// Create collections for Stickers
        /// </summary>
        /// <param name="titles">Collections Title classes with title name and page number in file</param>
        /// <param name="file">PDF file with included stickers content</param>
        /// <returns>Collection Stickers</returns>
        public IEnumerable<Sticker> Create(IEnumerable<Title> titles, string file)
        {
            if (string.IsNullOrEmpty(file) || string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file), "Path to sticker file cannot be empty");

            if (!File.Exists(file))
                throw new FileNotFoundException("File for sticker not found");

            if (titles == null)
                throw new ArgumentNullException(nameof(titles), "Collection with titles is null");

            var titleArray = titles.ToArray();
            if (titleArray.Length == 0)
                throw new ArgumentException("Collection titles cannot be empty");

            var stickerContents = PdfReadHelper.ReadToDictionary(file);
            if (stickerContents.Count == 0)
                throw new ArgumentException("In files not found pages");

            List<Sticker> stickers = new List<Sticker>();

            foreach (Title title in titleArray)
            {
                if (stickerContents.ContainsKey(title.PageInFile))
                {
                    Sticker sticker = new Sticker(title.Name, stickerContents[title.PageInFile]);
                    stickers.Add(sticker);
                }
            }

            return stickers;
        }

    }
}