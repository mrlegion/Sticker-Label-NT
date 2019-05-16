using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonServiceLocator;
using StickerLib.Domain.Build;
using StickerLib.Domain.Services;
using StickerLib.Infrastructure;
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

            Dictionary<int, int[]> groups = new Dictionary<int, int[]>();

            SELECT:
            // get selected stickers id
            int[] select = SelectStickers();
            // get count for selected stickers
            int count = Count();

            groups.Add(count, select);

            System.Console.Write("You want add second group? (y - yes; n - no): ");
            string request = System.Console.ReadLine();
            if (request == "y" || request == "yes" || request == "д" || request == "да")
                goto SELECT;

            // create group
            var service = ServiceLocator.Current.GetInstance<IStickerService>();
            List<Group> groupsList = new List<Group>();

            foreach (var entity in groups)
            {
                List<Sticker> stickers = new List<Sticker>();

                Print("Get stickers on db", MessageType.Debug);
                foreach (int i in entity.Value)
                    stickers.Add(service.Get(i));
                Print("Create group for " + entity.Key + " count", MessageType.Debug);
                Group group = new Group("Stickers for " + entity.Key, entity.Key, stickers);
                groupsList.Add(group);
            }
            
            Print("Inizialize creator", MessageType.Debug);
            var creator = ServiceLocator.Current.GetInstance<Creator>();
            Print("Start create new files", MessageType.Info);
            creator.Create(groupsList);
            Print("Document has saved!", MessageType.Success);

            System.Console.ReadKey();
        }

        private static void Initialization()
        {
            StartUp.InitAutofac();
            StartUp.InitProperties();

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
                Print("Error! You must enter number for selected stickers!", MessageType.Error);
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
            START:
            System.Console.WriteLine();
            System.Console.Write("Enter count for selected group: ");
            string count = System.Console.ReadLine();
            if (string.IsNullOrEmpty(count) || string.IsNullOrWhiteSpace(count))
            {
                Print("Error! You must enter number for count selected stickers!", MessageType.Error);
                goto START;
            }

            if (int.TryParse(count.Trim(), out int result))
            {
                if (result <= 0)
                {
                    Print("Count cannot be less or equals 0!", MessageType.Error);
                    goto START;
                    
                }

                return result;
            }

            Print("Enter not number! Please check!", MessageType.Warning);
            goto START;
        }

        private static void Print(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Debug:
                    System.Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case MessageType.Info:
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case MessageType.Message:
                    System.Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case MessageType.Success:
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case MessageType.Warning:
                    System.Console.ForegroundColor = ConsoleColor.Black;
                    System.Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case MessageType.Error:
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case MessageType.Fatal:
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            System.Console.WriteLine(message);

            // reset all options console
            System.Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void ConsoleWriteListStickers()
        {

            var request = ServiceLocator.Current.GetInstance<IStickerService>().GetAll();
            var list = request.ToList();

            Print($"Stickers List [ Count in db: {list.Count}]:\n", MessageType.Info);
            System.Console.WriteLine(new string('-', 100));

            foreach (Sticker sticker in list)
            {
                Print($"  {sticker.Id}\t|  {sticker.Name}", MessageType.Message);
                System.Console.WriteLine(new string('-', 100));
            }
        }
    }
}
