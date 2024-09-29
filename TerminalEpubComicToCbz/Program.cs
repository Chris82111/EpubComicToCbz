using Chris82111.EpubConvertion;

namespace Chris82111.TerminalEpubComicToCbz
{
    internal class Program
    {
        // ANSI Escape Codes
        private static string red = Console.IsOutputRedirected ? "" : "\x1b[31m";
        private static string yellow = Console.IsOutputRedirected ? "" : "\x1b[0;33m";
        private static string normal = Console.IsOutputRedirected ? "" : "\x1b[0m"; 

        static void Help(string[] args)
        {
            var newLine = "\n";

            var nameOfExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? System.AppDomain.CurrentDomain.FriendlyName;
            nameOfExecutable = Path.GetFileName(nameOfExecutable);

            Console.WriteLine(
                $"usage: ./{nameOfExecutable} [-h -? help] [-i input] [--]" + newLine +
                "" + newLine +
                "  -h, -? --help : Shows this help" + newLine +
                "  -i, --input   : The input file or directory" + newLine +
                "  --            : Ends the input of commands" + newLine
            );

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  args[{i}]: {args[i]}");
            }
        }

        static string? ReadValue(string parameter, string search, Action<string>? handler = null)
        {
            string? value = null;
            int argumentStart = search.Length;

            if (search == parameter.Substring(0, argumentStart))
            {
                if (argumentStart < parameter.Length)
                {
                    if ("=" == parameter.Substring(argumentStart, 1))
                    {
                        argumentStart++;
                        if (argumentStart < parameter.Length)
                        {
                            value = parameter.Substring(argumentStart);
                            handler?.Invoke(value);
                        }
                    }
                    else
                    {
                        value = parameter.Substring(argumentStart);
                        handler?.Invoke(value);
                    }
                }
            }
            return value;
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
                        ReadValue(args[i], "-i",      (value) => inputs.Add(value));
                        ReadValue(args[i], "--input", (value) => inputs.Add(value));
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