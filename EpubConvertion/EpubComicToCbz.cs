using Chris82111.ComicInfo.Dto;
using Chris82111.ComicInfo.Enums;
using Chris82111.ComicInfo.Helper;
using Chris82111.ComicInfo.Types;
using Chris82111.Domain.Helper;
using Chris82111.Epub;
using Chris82111.Epub.Enums;
using System.IO.Compression;

namespace Chris82111.EpubConvertion
{
    public class EpubComicToCbz : IDisposable
    {
        private EpubInfo? EpubInfo;

        public EpubComicToCbz(FileInfo epub)
        {
            EpubInfo = new EpubInfo(epub);
        }

        ~EpubComicToCbz() { Dispose(); }

        public void Dispose()
        {
            EpubInfo?.Dispose();
            EpubInfo = null;
        }

        public void Convert()
        {
            if(null == EpubInfo) { return; }

            var fullNameWithoutExtension = Path.Combine(
                    EpubInfo.DirectoryName,
                    Path.GetFileNameWithoutExtension(EpubInfo.FullName));

            var dir = new DirectoryInfo(fullNameWithoutExtension + "-CbzTemp");

            EpubInfo.CopyAllPictures(dir);

            CreateComicInfo(dir, EpubInfo);

            CreateZip(dir, new FileInfo(fullNameWithoutExtension + ".cbz"));

            dir.Delete(true);
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