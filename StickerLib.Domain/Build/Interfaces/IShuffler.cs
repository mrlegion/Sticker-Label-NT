using System.Threading.Tasks;

namespace StickerLib.Domain.Build.Interfaces
{
    internal interface IShuffler
    {
        int[] Shuffle(int[] source, int group, string pattern);
        Task<int[]> ShuffleAsync(int[] source, int group, string pattern);
    }
}