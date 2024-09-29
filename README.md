# EpubComicToCbz

<div align="center">

[![The Anansi Project 2.1 (Draft)](https://img.shields.io/badge/The_Anansi_Project-2.1_(Draft)-25C2A0)](https://anansi-project.github.io/docs/comicinfo/schemas/v2.1 "Link to The Anansi Project webpage Version 2.1 (Draft)")
[![SkiaSharp](https://img.shields.io/badge/SkiaSharp-GitHub-D8B024)](https://github.com/mono/SkiaSharp "Link to SkiaSharp GitHub repository")

</div>

In progress. The project converts unencrypted version 3.0 ePUB comic file into a Comic Book Archive (ZIP → cbz) file. The metadata of the ePUB file is stored in a ComicInfo file. Since not all data is used, it is not possible to undo the conversion.

## Run the App

To run the application, enter the name followed by the parameters:

```txt
./Chris82111.TerminalEpubComicToCbz.exe [--help] [<--input inputFile>] <inputFile>
```

- `--help`, `-h`, `-?`: Shows the help
- `--input`, `-i`: Specifies the input file or directory, this follows as the next parameter
