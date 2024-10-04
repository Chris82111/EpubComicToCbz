using Chris82111.Domain.Enums;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Chris82111.Domain.Terminal
{
    public class AnsiColor
    {
        #region WINDOWS

        private class WindowsSystem
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetStdHandle(int handle);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int mode);

            // P/Invoke for setting console mode
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        }

        /// <summary>
        ///         The standard device. This parameter can be one of the following emums.
        /// <br/>   <see href="https://learn.microsoft.com/de-de/windows/console/getstdhandle"/>
        /// </summary>
        private enum nStdHandle : Int32
        {
            /// <summary>
            ///         The standard input device. Initially, this is the console input buffer, CONIN$.
            /// <br/>   4294967286
            /// </summary>
            STD_INPUT_HANDLE = (Int32)(-10),

            /// <summary>
            ///         The standard output device. Initially, this is the active console screen buffer, CONOUT$.
            /// <br/>   4294967285
            /// </summary>
            STD_OUTPUT_HANDLE = (Int32)(-11),

            /// <summary>
            ///         The standard error device. Initially, this is the active console screen buffer, CONOUT$.
            /// <br/>   4294967284
            /// </summary>
            STD_ERROR_HANDLE = (Int32)(-12),
        }

        /// <summary>
        ///         The input or output mode to be set.
        /// <br/>   <see href="https://learn.microsoft.com/en-us/windows/console/setconsolemode"/>
        /// </summary>
        [Flags]
        private enum dwMode
        {
            /// <summary>
            ///         Characters written by the WriteFile or WriteConsole function
            /// <br/>   or echoed by the ReadFile or ReadConsole function are parsed
            /// <br/>   for ASCII control sequences, and the correct action is performed.
            /// <br/>   Backspace, tab, bell, carriage return, and line feed characters
            /// <br/>   are processed. It should be enabled when using control sequences
            /// <br/>   or when <see cref="dwMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING"/> is set.
            /// </summary>
            ENABLE_PROCESSED_OUTPUT = 0x0001,

            /// <summary>
            ///         When writing with WriteFile or WriteConsole or echoing with 
            /// <br/>   ReadFile or ReadConsole, the cursor moves to the beginning of 
            /// <br/>   the next row when it reaches the end of the current row. This
            /// <br/>   causes the rows displayed in the console window to scroll up
            /// <br/>   automatically when the cursor advances beyond the last row in
            /// <br/>   the window. It also causes the contents of the console screen
            /// <br/>   buffer to scroll up (../discarding the top row of the console
            /// <br/>   screen buffer) when the cursor advances beyond the last row
            /// <br/>   in the console screen buffer. If this mode is disabled, the
            /// <br/>   last character in the row is overwritten with any subsequent
            /// <br/>   characters.
            /// </summary>
            ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,

            /// <summary>
            ///         When writing with WriteFile or WriteConsole, characters are
            /// <br/>   parsed for VT100 and similar control character sequences 
            /// <br/>   that control cursor movement, color/font mode, and other
            /// <br/>   operations that can also be performed via the existing
            /// <br/>   Console APIs.
            /// <br/>   For more information, see Console Virtual Terminal Sequences.
            /// <br/>   Ensure <see cref="dwMode.ENABLE_PROCESSED_OUTPUT"/> is set
            /// <br/>   when using this flag.
            /// </summary>
            ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,

            /// <summary>
            ///         When writing with WriteFile or WriteConsole, this adds an
            /// <br/>   additional state to end-of-line wrapping that can delay the
            /// <br/>   cursor move and buffer scroll operations.
            /// <br/>   
            /// <br/>   Normally when <see cref="dwMode.ENABLE_WRAP_AT_EOL_OUTPUT"/> 
            /// <br/>   is set and text reaches the end of the line, the cursor will 
            /// <br/>   immediately move to the next line and the contents of the 
            /// <br/>   buffer will scroll up by one line. In contrast with this flag 
            /// <br/>   set, the cursor does not move to the next line, and the scroll 
            /// <br/>   operation is not performed. The written character will be 
            /// <br/>   printed in the final position on the line and the cursor will 
            /// <br/>   remain above this character as if 
            /// <br/>   <see cref="dwMode.ENABLE_WRAP_AT_EOL_OUTPUT"/> was off, but
            /// <br/>   the next printable character will be printed as if
            /// <br/>   <see cref="dwMode.ENABLE_WRAP_AT_EOL_OUTPUT"/> is on.
            /// <br/>   No overwrite will occur. Specifically, the cursor quickly
            /// <br/>   advances down to the following line, a scroll is performed
            /// <br/>   if necessary, the character is printed, and the cursor
            /// <br/>   advances one more position.
            /// <br/>   
            /// <br/>   The typical usage of this flag is intended in conjunction with
            /// <br/>   setting <see cref="dwMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING"/>
            /// <br/>   to better emulate a terminal emulator where writing the final
            /// <br/>   character on the screen (../in the bottom right corner) without
            /// <br/>   triggering an immediate scroll is the desired behavior.
            /// </summary>
            DISABLE_NEWLINE_AUTO_RETURN = 0x0008,

            /// <summary>
            /// The APIs for writing character attributes including WriteConsoleOutput
            /// <br/>   and WriteConsoleOutputAttribute allow the usage of flags from
            /// <br/>   character attributes to adjust the color of the foreground
            /// <br/>   and background of text. Additionally, a range of DBCS flags
            /// <br/>   was specified with the COMMON_LVB prefix. Historically,
            /// <br/>   these flags only functioned in DBCS code pages for Chinese,
            /// <br/>   Japanese, and Korean languages.
            /// <br/>   
            /// <br/>   With exception of the leading byte and trailing byte flags,
            /// <br/>   the remaining flags describing line drawing and reverse
            /// <br/>   video(../swap foreground and background colors) can be useful
            /// <br/>   for other languages to emphasize portions of output.
            /// <br/>   
            /// <br/>   Setting this console mode flag will allow these attributes
            /// <br/>   to be used in every code page on every language.
            /// <br/>   
            /// <br/>   It is off by default to maintain compatibility with known
            /// <br/>   applications that have historically taken advantage of the
            /// <br/>   console ignoring these flags on non-CJK machines to store
            /// <br/>   bits in these fields for their own purposes or by accident.
            /// <br/>   
            /// <br/>   Note that using the <see cref="dwMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING"/>
            /// <br/>   mode can result in LVB grid and reverse video flags being
            /// <br/>   set while this flag is still off if the attached application
            /// <br/>   requests underlining or inverse video via Console Virtual
            /// <br/>   Terminal Sequences.
            /// </summary>
            ENABLE_LVB_GRID_WORLDWIDE = 0x0010,
        }

        private static IntPtr GetStdHandle(nStdHandle handle)
        {
            return WindowsSystem.GetStdHandle((int)handle);
        }

        private static bool GetConsoleMode(IntPtr hConsoleHandle, out dwMode mode)
        {
            bool value = WindowsSystem.GetConsoleMode(hConsoleHandle, out int modeNumber);
            mode = (dwMode)modeNumber;
            return value;
        }

        private static bool SetConsoleMode(IntPtr hConsoleHandle, dwMode mode)
        {
            return WindowsSystem.SetConsoleMode(hConsoleHandle, (int)mode);
        }

        #endregion

        public enum UseAnsiColorState
        {
            /// <summary>
            ///         This switches off the ANSI color.
            /// </summary>
            No = 0,

            /// <summary>
            ///         With this option <c>Console.IsOutputRedirected</c> is used.
            /// <br/>   If it returns true no color will be used.
            /// </summary>
            Auto,

            /// <summary>
            ///         This switches on the ANSI color.
            /// </summary>
            Yes,
        }

        public static UseAnsiColorState UseAnsiColor { get; set; } = UseAnsiColorState.Auto;

        public class TextColor
        {
            public static string Red => Rgb(197, 15, 31);
            public static string Yellow => Rgb(193, 156, 0);
            public static string Normal => Ansi("\x1b[0m");

            public static string Rgb(int r, int g, int b)
            {
                string color;
                if (0 > r || 0 > g || 0 > b || 255 < r || 255 < g || 255 < b)
                {
                    color = "\x1b[0m";
                }
                else
                {
                    color = $"\x1b[38;2;{r};{g};{b}m";
                }
                switch (UseAnsiColor)
                {
                    case UseAnsiColorState.No: return "";
                    case UseAnsiColorState.Auto: return Console.IsOutputRedirected ? "" : color;
                    case UseAnsiColorState.Yes: return color;
                    default: return "";
                }
            }

            public static string Ansi(string ansiColorString)
            {
                switch (UseAnsiColor)
                {
                    case UseAnsiColorState.No: return "";
                    case UseAnsiColorState.Auto: return Console.IsOutputRedirected ? "" : ansiColorString;
                    case UseAnsiColorState.Yes: return ansiColorString;
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Use this method when using Windows to ensure that color is supported.
        /// </summary>
        /// <param name="enable"></param>
        public static void EnableForWindows(bool enable = true)
        {
            // Enable ANSI support only on Windows
            if (OperatingSystem.IsWindows())
            {   
                IntPtr handle = GetStdHandle(nStdHandle.STD_OUTPUT_HANDLE);

                if(GetConsoleMode(handle, out dwMode mode))
                {
                    EnumHelper<dwMode>.WriteFlag(ref mode, dwMode.ENABLE_VIRTUAL_TERMINAL_PROCESSING, enable);
                    SetConsoleMode(handle, mode);
                }
            }
        }
    }
}
