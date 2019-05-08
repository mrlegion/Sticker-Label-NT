using System;
using System.Threading.Tasks;
using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Helpers;

namespace StickerLib.Domain.Build.Implimentations
{
    internal class Shuffler : IShuffler
    {
        public int[] Shuffle(int[] source, int group, string pattern)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Length == 0) throw new ArgumentException(nameof(source));
            int[] p = pattern.PatternConvert(' ');
            if (group != p.Length) throw new ArgumentOutOfRangeException(nameof(pattern), "pattern length and group mast be equals");
            int[] result = new int[source.Length];
            int pages = source.Length / group;
            for (int i = 0; i < pages; i++)
            for (int j = 0; j < group; j++)
            {
                int index = p[j] + (i * group); // calculate index for page
                result[i * group + j] = source[index];
            }
            return result;
        }

        public Task<int[]> ShuffleAsync(int[] source, int group, string pattern)
        {
            return Task.Factory.StartNew(() => Shuffle(source, group, pattern));
        }
    }
}