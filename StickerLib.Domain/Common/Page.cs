namespace StickerLib.Domain.Common
{
    public struct Page
    {
        public int[,] Pages { get; set; }

        public int Number { get; set; }

        public Page(int number, int[,] pages)
        {
            Number = number;
            Pages = pages;
        }
    }
}