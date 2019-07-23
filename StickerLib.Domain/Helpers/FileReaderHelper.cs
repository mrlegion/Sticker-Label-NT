﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Serilog;
using StickerLib.Domain.Common;

namespace StickerLib.Domain.Helpers
{
    public class FileReaderHelper
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

        public static IEnumerable<Title> ReadTitlesFile(string file, bool useHeaders = false)
        {
            if (!File.Exists(file)) throw new FileNotFoundException("Not found file with titles", nameof(file));
            List<Title> list = null;
            try
            {
                using (StreamReader sr = new StreamReader(file, Encoding.Default))
                {
                    list = new List<Title>();
                    while (!sr.EndOfStream)
                    {
                        string[] row = sr.ReadLine()?.Split(';');
                        if (row != null) list.Add(new Title() { Name = row[0], PageInFile = Int32.Parse(row[1]), });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
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