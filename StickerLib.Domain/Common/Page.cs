namespace StickerLib.Domain.Common
{
    internal struct Page
    {
        private int[,] _pages;
        private int _number;

        public int[,] Pages
        {
            get { return _pages; }
        }
        public int Number
        {
            get { return _number; }
        }

        public Page(int number, int[,] pages)
        {
            _number = number;
            _pages = pages;
        }

        public void SetPages(int[,] pages)
        {
            _pages = pages;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}