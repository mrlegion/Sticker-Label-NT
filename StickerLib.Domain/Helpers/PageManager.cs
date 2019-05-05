using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace StickerLib.Domain.Helpers
{
    internal class PageManager : IPageManager
    {
        public int[] AddEmptyPages(int[] source, int count, int defaultValue = -1)
        {
            Log.Debug("Start adding empty pages");
            var temp = new List<int>(count);
            for (int i = 0; i < count; i++)
                temp.Add(defaultValue);
            Log.Debug("Adding pages is end");
            return temp.ToArray();
        }

        public Task<int[]> AddEmptyPagesAsync(int[] source, int count, int defaultValue = -1)
        {
            return Task<int[]>.Factory.StartNew(() => AddEmptyPages(source, count, defaultValue));
        }

        public int[] DublicatePages(int[] source, int count, DuplicationType type = DuplicationType.EachPage)
        {
            var temp = new List<int>();
            switch (type)
            {
                case DuplicationType.EachPage:
                    Log.Debug("Duplicating each pages");
                    foreach (var t in source)
                        for (int j = 0; j < count; j++)
                            temp.Add(t);
                    break;
                case DuplicationType.GroupPage:
                    Log.Debug("Dublicating group pages");
                    for (int i = 0; i < count; i++)
                        temp.AddRange(source);
                    break;
                case DuplicationType.None:
                default:
                    Log.Fatal("Duplication type are not is defined");
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return temp.ToArray();
        }

        public Task<int[]> DuplicatePageAsync(int[] source, int count,
            DuplicationType type = DuplicationType.EachPage)
        {
            return Task<int[]>.Factory.StartNew(() => DublicatePages(source, count, type));
        }
    }
}