using System;
using System.IO;
using System.Linq;
using CommonServiceLocator;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start up operations
            Initialization();

            ConsoleWriteListStickers();

            var select = SelectStickers();



            System.Console.ReadKey();
        }

        private static void Initialization()
        {
            StartUp.InitAutofac();

            if (!File.Exists(".\\db\\Sticker.db"))
            {
                string pdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Source\\Ярлыки ИП Герр.pdf");
                string title = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Source\\Ярлыки.txt");

                DataBase.FillDb(pdf, title);
            }
        }

        private static int[] SelectStickers()
        {
            START:
            System.Console.WriteLine();
            System.Console.Write("Enter number of selected stickers with whitespace: ");
            string selected = System.Console.ReadLine();
            if (string.IsNullOrEmpty(selected) || string.IsNullOrWhiteSpace(selected))
            {
                System.Console.WriteLine("Error! You must enter number for selected stickers!");
                goto START;
            }
            string[] splited = selected.Split(' ');
            int[] result = new int[splited.Length];
            for (int i = 0; i < splited.Length; i++)
                result[i] = int.Parse(splited[i]);
            return result;
        }

        private static int Count()
        {

        }

        private static void ConsoleWriteListStickers()
        {

            var request = ServiceLocator.Current.GetInstance<IStickerService>().GetAll();
            var list = request.ToList();

            System.Console.WriteLine($"Stickers List [ Count in db: {list.Count}]:\n");
            System.Console.WriteLine(new string('-', 100));

            foreach (Sticker sticker in list)
            {
                System.Console.WriteLine($"  {sticker.Id}\t|  {sticker.Name}");
                System.Console.WriteLine(new string('-', 100));
            }
        }
    }
}
