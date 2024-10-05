# EpubComicToCbz

<div align="center">

[![The Anansi Project 2.1 (Draft)](https://img.shields.io/badge/The_Anansi_Project-2.1_(Draft)-25C2A0)](https://anansi-project.github.io/docs/comicinfo/schemas/v2.1 "Link to The Anansi Project webpage Version 2.1 (Draft)")
[![SkiaSharp](https://img.shields.io/badge/SkiaSharp-GitHub-D8B024)](https://github.com/mono/SkiaSharp "Link to SkiaSharp GitHub repository")

</div>

The project converts unencrypted version 3.0 ePUB comic file into a Comic Book Archive (ZIP → cbz) file. The metadata of the ePUB file is stored in a ComicInfo file. Since not all data is used, it is not possible to undo the conversion.

## Run the App

To run the application, enter the name followed by the parameters:

`[--help] [--git] [--version] [--no-ansi] [<--input inputFile>] <inputFile>`

- `--help`, `-h`, `-?`: Shows the help
- `--git`: Shows information about the repository
- `--version`: Shows Git commit with 8 characters
- `--no-ansi`: Prevents the use of ANSI colors
- `--input`, `-i`: Specifies the input file or directory, this follows as the next parameter

### Linux and Windos

1. The correct runtime is necessary.
1. A combined batch/shell script file can be used. Call it like a Linux or Windows script file:

   ```bash
   TerminalEpubComicToCbz.bat
   ```

### Linux

1. Install

   ```bash
   sudo apt-get update && sudo apt-get install -y aspnetcore-runtime-6.0
   ```

1. Start

   ```bash
   dotnet Chris82111.TerminalEpubComicToCbz.dll
   ```

### Windows

- Start by double-clicking or drag & drop files or directories onto the executable file.
- Start via Windows PowerShell terminal:

   ```terminal
   .\Chris82111.TerminalEpubComicToCbz.exe
   ```

- Start via Windows CMD:

   ```cmd
   Chris82111.TerminalEpubComicToCbz.exe
   ```
