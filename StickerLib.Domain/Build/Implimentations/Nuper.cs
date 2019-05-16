using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Common;

namespace StickerLib.Domain.Build.Implimentations
{
    internal class Nuper : INuper
    {
        public Queue<Page> Nup(int[] source)
        {
            // calculated page count
            var pageRemainder = source.Length % Infrastructure.Common.Properties.GetInstance().Groups;
            var pageCount = pageRemainder > 0
                ? source.Length / Infrastructure.Common.Properties.GetInstance().Groups + 1
                : source.Length / Infrastructure.Common.Properties.GetInstance().Groups;

            var queue = new Queue<Page>();
            int count = -1;
            for (int page = 0; page < pageCount; page++)
            {
                int[,] pages = new int[Infrastructure.Common.Properties.GetInstance().Row, Infrastructure.Common.Properties.GetInstance().Column];
                for (int row = 0; row < Infrastructure.Common.Properties.GetInstance().Row; row++)
                    for (int cols = 0; cols < Infrastructure.Common.Properties.GetInstance().Column; cols++)
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