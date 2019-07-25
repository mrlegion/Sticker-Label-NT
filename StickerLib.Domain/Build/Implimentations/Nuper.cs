using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StickerLib.Domain.Build.Implimentations
{
    internal class Nuper : INuper
    {
        public Queue<Page> Nup(int[] source)
        {
            var propertiies = Infrastructure.Common.Properties.GetInstance();

            // calculated page count
            var pageRemainder = source.Length % propertiies.Groups;
            var pageCount = pageRemainder > 0
                ? source.Length / propertiies.Groups + 1
                : source.Length / propertiies.Groups;

            var queue = new Queue<Page>();
            int count = -1;
            for (int page = 0; page < pageCount; page++)
            {
                int[,] pages = new int[propertiies.Row, propertiies.Column];
                for (int row = 0; row < propertiies.Row; row++)
                    for (int cols = 0; cols < propertiies.Column; cols++)
                        pages[row, cols] = source[++count];
                queue.Enqueue(new Page(page, pages));
            }

            return queue;
        }

        public Task<Queue<Page>> NupAsync(int[] source)
        {
            return Task.Factory.StartNew(() => Nup(source));
        }

        public IEnumerable<Queue<Page>> Nup(IEnumerable<int[]> sources)
        {
            var list = sources.ToList();
            if (list.Count == 0) throw new ArgumentNullException(nameof(sources));

            var queueList = new List<Queue<Page>>();
            foreach (int[] source in list)
                queueList.Add(Nup(source));
            return queueList;
        }

        public async Task<IEnumerable<Queue<Page>>> NupAsync(IEnumerable<int[]> sources)
        {
            var list = sources.ToList();
            if (list.Count == 0) throw new ArgumentNullException(nameof(sources));

            var queueList = new List<Queue<Page>>();
            foreach (int[] source in list)
                queueList.Add(await NupAsync(source));
            return queueList;
        }
    }
}