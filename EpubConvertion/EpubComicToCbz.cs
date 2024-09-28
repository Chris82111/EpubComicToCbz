using Chris82111.ComicInfo.Dto;
using Chris82111.ComicInfo.Enums;
using Chris82111.ComicInfo.Helper;
using Chris82111.ComicInfo.Types;
using Chris82111.Domain.Helper;
using Chris82111.Epub;
using Chris82111.Epub.Enums;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Chris82111.EpubConvertion
{
    public class EpubComicToCbz : IDisposable
    {
        private List<EpubInfo> EpubInfoList = new List<EpubInfo>();

        public EpubComicToCbz(FileInfo epub)
        {
            Add(epub);
        }

        public EpubComicToCbz(DirectoryInfo epubDirectory)
        {
            Add(epubDirectory);
        }

        public EpubComicToCbz(string fileOrDirectory)
        {
            var epub = new FileInfo(fileOrDirectory);
            if (epub.Exists)
            {
                Add(epub);
            }
            else
            {
                var epubDirectory = new DirectoryInfo(fileOrDirectory);
                if (epubDirectory.Exists)
                {
                    Add(epubDirectory);
                }
            }
        }

        ~EpubComicToCbz() { Dispose(); }

        public void Dispose()
        {
            for (int i = EpubInfoList.Count - 1; 0 <= i; i--)
            {
                EpubInfoList[i].Dispose();
                EpubInfoList.RemoveAt(i);
            }
        }

        public void Convert()
        {
            foreach (var epubInfo in EpubInfoList)
            {
                if (null == epubInfo) { return; }

                var fullNameWithoutExtension = Path.Combine(
                        epubInfo.DirectoryName,
                        Path.GetFileNameWithoutExtension(epubInfo.FullName));

                var dir = new DirectoryInfo(fullNameWithoutExtension + "-CbzTemp");

                epubInfo.CopyAllPictures(dir);

                CreateComicInfo(dir, epubInfo);

                CreateZip(dir, new FileInfo(fullNameWithoutExtension + ".cbz"));

                dir.Delete(true);
            }
        }


        private void Add(FileInfo epub)
        {
            EpubInfoList.Add(new EpubInfo(epub));
        }

        private void Add(DirectoryInfo epubDirectory)
        {
            foreach (var epub in epubDirectory.GetFiles())
            {
                if (epub.Exists && "epub" == epub.Extension.Substring(1).ToLower())
                {
                    Add(epub);
                }
            }
        }

        private void CreateComicInfo(DirectoryInfo dir, EpubInfo epubInfo)
        {
            var comicInfoPages = new List<ComicPageInfo>();

            foreach (var page in epubInfo.Pages)
            {
                var length = new FileInfo(page.Picture).Length;
                var size = ImageHelper.PictureSize(page.Picture);

                ComicPageType comicPageType = ComicPageInfoDefault.Type;
                if (EpubTypes.Cover == page.EpubType)
                {
                    comicPageType = ComicPageType.FrontCover;
                }

                comicInfoPages.Add(new ComicPageInfo()
                {
                    Image = page.Number,
                    Type = comicPageType,
                    ImageSize = length,
                    ImageWidth = size.Width,
                    ImageHeight = size.Height,
                });
            }

            var comicInfoEpubDto = new ComicInfoEpubDto()
            {
                Title = epubInfo.Meta.Title ?? Path.GetFileNameWithoutExtension(epubInfo.FullName),
                Series = "",
                Number = "",
                Volume = "",

                Year = epubInfo.Meta.Date?.Year ?? ComicInfo_V2_1_DtoDefault.Year,
                Month = epubInfo.Meta.Date?.Month ?? ComicInfo_V2_1_DtoDefault.Month,
                Day = epubInfo.Meta.Date?.Day ?? ComicInfo_V2_1_DtoDefault.Day,

                Writer = epubInfo.Meta.Creator,
                Publisher = epubInfo.Meta.Publisher,

                PageCount = epubInfo.Pages.Count,
                LanguageISO = LanguageHelper.TryGetCultureInfo(epubInfo.Meta.Language)?.TwoLetterISOLanguageName ?? "",
                GTIN = epubInfo.Meta.Identifier,

                Pages = comicInfoPages,
            };

            ComicInfoEpubDto.Create(comicInfoEpubDto, Path.Combine(dir.FullName, @"ComicInfo.xml"));
        }

        private void CreateZip(DirectoryInfo dir, FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
            ZipFile.CreateFromDirectory(dir.FullName, file.FullName, CompressionLevel.NoCompression, false);
        }
    }
}