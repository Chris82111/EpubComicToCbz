using Chris82111.Domain.Terminal;
using Chris82111.EpubConvertion;

namespace Chris82111.TerminalEpubComicToCbz
{
    internal class Program
    {
        public static string NewLine = "\n";

        static void Help(string[] args)
        {
            var nameOfExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName
                ?? System.AppDomain.CurrentDomain.FriendlyName;

            nameOfExecutable = Path.GetFileName(nameOfExecutable);

            var y = AnsiColor.TextColor.Yellow;
            var n = AnsiColor.TextColor.Normal;

            // https://stackoverflow.com/questions/9725675/is-there-a-standard-format-for-command-line-shell-help-text
            Console.WriteLine(
                $"usage: ./{nameOfExecutable} [{y}--help{n}] [{y}--git{n}] [{y}--version{n}] [<{y}--input{n} inputFile>] <inputFile>" + NewLine +
                "" + NewLine +
                $"  {y}--help{n}, {y}-h{n}, {y}-?{n} : Shows this help" + NewLine +
                $"  {y}--git{n}          : Shows information about the repository" + NewLine +
                $"  {y}--version{n}      : Shows git commit with 8 characters" + NewLine +
                $"  {y}--input{n}, {y}-i{n},   : The input file or directory follows" + NewLine
            );

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  args[{i}]: {args[i]}");
            }
        }
        static void Git()
        {
            Console.WriteLine(
                $"Url   : {AssemblyInfo.GitUrl}" + NewLine +
                $"Hash  : {AssemblyInfo.GitHash}" + NewLine +
                $"Commit: {AssemblyInfo.GitCommit}"
            );
            return;
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
            AnsiColor.EnableForWindows();

            var inputs = new List<string>();
            bool help = false;
            bool version = false;
            bool git = false;
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
                    case "--help": help = true; break;
                    case "--version": version = true; break;
                    case "--no-ansi":
                        AnsiColor.UseAnsiColor = AnsiColor.UseAnsiColorState.No;
                        break;
                    case "--git": git = true; break;
                    default:
                        if(null != ReadValue(args[i], "-i",      (value) => inputs.Add(value))) { break; }
                        if(null != ReadValue(args[i], "--input", (value) => inputs.Add(value))) { break; }
                        inputs.Add(args[i]);
                        break;
                }
            }
            
            if(true == help)
            {
                Help(args);
                return;
            }

            if (true == git)
            {
                Git();
                return;
            }

            if (true == version)
            {
                Console.WriteLine($"{AssemblyInfo.Version}-{AssemblyInfo.GitHash}");
                return;
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
                Console.WriteLine($"[{AnsiColor.TextColor.Red}fail{AnsiColor.TextColor.Normal}] {ex}");
            }
        }
    }
}