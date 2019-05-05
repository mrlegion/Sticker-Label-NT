using System.Threading.Tasks;

namespace StickerLib.Domain.Helpers
{
    public interface IPageManager
    {
        int[] AddEmptyPages(int[] source, int count, int defaultValue = -1);
        Task<int[]> AddEmptyPagesAsync(int[] source, int count, int defaultValue = -1);
        int[] DublicatePages(int[] source, int count, DuplicationType type = DuplicationType.EachPage);
        Task<int[]> DuplicatePageAsync(int[] source, int count, DuplicationType type = DuplicationType.EachPage);
    }
}