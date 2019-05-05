using System;
using System.Collections.Generic;
using System.IO;
using Serilog;

namespace StickerLib.Domain.Helpers
{
    internal class FileReaderHelper
    {
        public static List<string> Read(string file)
        {
            if (!File.Exists(file))
            {
                Log.Fatal("Not found file with titles. File: [ {@file} ]", file);
                throw new FileNotFoundException("Not found file with titles.", nameof(file));
            }

            try
            {
                using (var sr = new StreamReader(file))
                {
                    var result = new List<string>();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        result.Add(line);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}