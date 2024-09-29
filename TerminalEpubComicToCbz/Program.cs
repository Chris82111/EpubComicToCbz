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

            // https://stackoverflow.com/questions/9725675/is-there-a-standard-format-for-command-line-shell-help-text
            Console.WriteLine(
                $"usage: ./{nameOfExecutable} [--help] [<--input inputFile>] <inputFile>" + newLine +
                "" + newLine +
                "  --help, -h, -? : Shows this help" + newLine +
                "  --input, -i,   : The input file or directory follows" + newLine
            );

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  args[{i}]: {args[i]}");
            }
        }

        /// <summary>
        ///         Extracts a value from the specified parameter that begins
        /// <br/>   with the search string (flag/switch). An equals sign between
        /// <br/>   the search string and the value is permitted.
        /// </summary>
        /// <param name="parameter">Contains the element and value to be searched for</param>
        /// <param name="search">The name of the flag/switch</param>
        /// <param name="handler">
        ///         A handler that is called when a value is found, the found 
        /// <br/>   value is the input parameter</param>
        /// <returns>null if the search string is not found</returns>
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
                    default:
                        if(null != ReadValue(args[i], "-i",      (value) => inputs.Add(value))) { break; }
                        if(null != ReadValue(args[i], "--input", (value) => inputs.Add(value))) { break; }
                        inputs.Add(args[i]);
                        break;
                }
            }
            
            if (0 == inputs.Count)
            {
                Help(args);
                Console.WriteLine("No parameter found, press key to continue.");
                Console.ReadKey();
                return;
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