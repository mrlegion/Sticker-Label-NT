using System.Collections.Generic;
using System.Threading.Tasks;
using StickerLib.Domain.Common;

namespace StickerLib.Domain.Build.Interfaces
{
    internal interface INuper
    {
        Queue<Page> Nup(int[] source);
        Task<Queue<Page>> NupAsync(int[] source);
        IEnumerable<Queue<Page>> Nup(IEnumerable<int[]> sources);
        Task<IEnumerable<Queue<Page>>> NupAsync(IEnumerable<int[]> sources);
    }
}