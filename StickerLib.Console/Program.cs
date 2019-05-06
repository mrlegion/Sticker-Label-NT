using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StickerLib.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = "{@title};{@page}";
            if (template.Contains("{@title}"))
            {
                template = template.Replace("{@title}", "1");
            }

            if (template.Contains("{@page}"))
            {
                template = template.Replace("{@page}", "0");
            }

            string[] separate = Regex.Split(template, "\\D");

            System.Console.ReadKey();
        }
    }
}
