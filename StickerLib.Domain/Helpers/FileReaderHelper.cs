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

        // maybe create string template for file
        // example: {@title};{@page}
        // or:      [$title$]-[$page$]

        // Todo: create algoriphm to use read file use to template
        //public static List<string> Read(string file, string template)
        //{
        //    // check file
        //    if (!File.Exists(file))
        //    {
        //        Log.Fatal("Not found file with titles. File: [ {@file} ]", file);
        //        throw new FileNotFoundException("Not found file with titles.", nameof(file));
        //    }

        //    //check pattern
        //    if (string.IsNullOrEmpty(template) || string.IsNullOrWhiteSpace(template))
        //    {
        //        Log.Fatal("Template for read file cannot be nullable or empty");
        //        throw new FileNotFoundException("Template for read file cannot be nullable or empty", nameof(template));
        //    }

        //    try
        //    {
        //        using (var sr = new StreamReader(file))
        //        {
        //            var result = new List<string>();
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                result.Add(line);
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //    throw new NotImplementedException();
        //}
    }
}