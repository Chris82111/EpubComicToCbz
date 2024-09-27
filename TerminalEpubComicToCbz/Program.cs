using Chris82111.Domain.Helper;
using Chris82111.EpubConvertion;
using SkiaSharp;
using System.Globalization;

namespace Chris82111.TerminalEpubComicToCbz
{
    internal class Program
    {
        private static string red = Console.IsOutputRedirected ? "" : "\x1b[31m";
        private static string yellow = Console.IsOutputRedirected ? "" : "\x1b[0;33m";
        private static string normal = Console.IsOutputRedirected ? "" : "\x1b[0m"; 

        static void Help(string[] args)
        {
            Console.WriteLine("usage: ./TerminalEpubComicToCbz [-h help] [-i input]");
            Console.WriteLine("");
            foreach (var arg in args)
            {
                Console.WriteLine($"[{yellow}info{normal}] {arg}");
            }
        }

        static void Main(string[] args)
        {
            var input = "";
            for (int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                    case "-i":
                    case "--input":
                        if(++i < args.Length)
                        {
                            input = args[i];
                        }
                        break;
                    case "-h":
                    case "--help":
                        Help(args);
                        return;
                    default:
                        break;
                }
            }

            try
            {
                var epub = new FileInfo(input);
                using (var epubToCbz = new EpubComicToCbz(epub))
                {
                    epubToCbz.Convert();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[{red}fail{normal}] {ex}");
            }
        }
    }
}