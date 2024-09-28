using Chris82111.EpubConvertion;

namespace Chris82111.TerminalEpubComicToCbz
{
    internal class Program
    {
        private static string red = Console.IsOutputRedirected ? "" : "\x1b[31m";
        private static string yellow = Console.IsOutputRedirected ? "" : "\x1b[0;33m";
        private static string normal = Console.IsOutputRedirected ? "" : "\x1b[0m"; 

        static void Help(string[] args)
        {
            var newLine = "\n";

            var nameOfExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? System.AppDomain.CurrentDomain.FriendlyName;
            nameOfExecutable = Path.GetFileName(nameOfExecutable);

            Console.WriteLine(
                $"usage: ./{nameOfExecutable} [-h help] [-i input]" + newLine +
                "" + newLine +
                "  -h, --help  : Shows this help" + newLine +
                "  -i, --input : The input file or directory" + newLine
            );

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  args[{i}]: {args[i]}");
            }
        }

        static void Main(string[] args)
        {
            if (0 == args.Length)
            {
                Help(args);
                return;
            }

            var inputs = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {                
                switch (args[i])
                {
                    case "-i":
                    case "--input":
                        if(++i < args.Length)
                        {
                            inputs.Add(args[i]);
                        }
                        break;
                    case "-?":
                    case "-h":
                    case "--help":
                        Help(args);
                        return;
                    case "--":
                        i = args.Length;
                        break;
                    default:
                        var search = "-i";
                        if(search == args[i].Substring(0, search.Length))
                        {
                            inputs.Add(args[i].Substring(search.Length));
                        }
                        search = "--input";
                        if (search == args[i].Substring(0, search.Length))
                        {
                            inputs.Add(args[i].Substring(search.Length));
                        }
                        break;
                }
            }

            try
            {
                foreach(var input in inputs)
                {
                    using (var epubToCbz = new EpubComicToCbz(input))
                    {
                        epubToCbz.Convert();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[{red}fail{normal}] {ex}");
            }
        }
    }
}