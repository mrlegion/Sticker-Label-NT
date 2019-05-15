namespace StickerLib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] temp = new int[4,4];
            // row 1
            temp[0, 0] = 0;
            temp[0, 1] = 1;
            temp[0, 2] = 2;
            temp[0, 3] = 3;
            // row 2
            temp[1, 0] = 4;
            temp[1, 1] = 5;
            temp[1, 2] = 6;
            temp[1, 3] = 7;
            // row 3
            temp[2, 0] = 8;
            temp[2, 1] = 9;
            temp[2, 2] = 10;
            temp[2, 3] = 11;
            // row 4
            temp[3, 0] = 12;
            temp[3, 1] = 13;
            temp[3, 2] = 14;
            temp[3, 3] = 15;

            for (int rows = 0; rows < 4; rows++)
            {
                for (int cols = 0; cols < 4; cols++)
                {
                    string s = $"Rows: {rows}, Cols: {cols}, Value: {temp[rows, cols]}";
                    System.Console.WriteLine(s);
                }
            }

            System.Console.ReadKey();
        }
    }
}
