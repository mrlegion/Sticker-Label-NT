using System;
using System.Threading.Tasks;
using Serilog;

namespace StickerLib.Domain.Helpers
{
    internal static class ConvertManager
    {
        public static int[] PatternConvert(this string pattern, char divider)
        {
            Log.Debug("Splided pattern string to array string");
            string[] splited = pattern.Trim().Split(divider);
            Log.Debug("Convert string pattern to int array");
            int[] result = Array.ConvertAll<string, int>(splited, int.Parse);
            return result;
        }

        public static Task<int[]> PatternConvertAsync(this string pattern, char divider)
        {
            return Task.Factory.StartNew(() => pattern.PatternConvert(divider));
        }
        
    }
}