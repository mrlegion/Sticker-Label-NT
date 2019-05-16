using System;
using System.Threading.Tasks;
using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Helpers;

namespace StickerLib.Domain.Build.Implimentations
{
    internal class Shuffler : IShuffler
    {
        public int[] Shuffle(int[] source, int group)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Length == 0) throw new ArgumentException(nameof(source));
            int[] result = new int[source.Length];
            int pages = source.Length / group;
            for (int i = 0; i < pages; i++)
                for (int j = 0; j < group; j++)
                {
                    int index = j * pages + i; // calculate index for page
                    result[i * group + j] = source[index];
                }
            return result;
        }

        public Task<int[]> ShuffleAsync(int[] source, int group)
        {
            return Task.Factory.StartNew(() => Shuffle(source, group));
        }
    }
}