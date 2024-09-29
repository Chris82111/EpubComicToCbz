using Chris82111.Epub.Dto;
using Chris82111.Epub.Enums;
using System.Globalization;
using System.Xml.Linq;

namespace Chris82111.Epub
{
    public class EpubInfo : IDisposable
    {
        public MetaDto Meta = new MetaDto();
        public List<PageDto> Pages = new List<PageDto>();
        public List<BookmarkDto> Bookmarks = new List<BookmarkDto>();
        public string FullName => Epub?.FullName ?? "";
        public string DirectoryName => Epub?.DirectoryName ?? "";

        public bool IsDisposed { get; private set; } = false;

        private FileInfo? Epub;
        private DirectoryInfo? EpubExtract;
        private DirectoryInfo? Oebps;
        private FileInfo? ContentFile;
        private XDocument? ContentXdoc;
        private FileInfo? Navigation;
        private XDocument? NavigationXdoc;
        private FileInfo? Cover;

        public EpubInfo(FileInfo epub)
        {
            Epub = epub;

            if (false == Epub.Exists) { throw new Exception($"File {Epub.FullName} does not exists."); }

            var extension = Epub.Extension.Substring(1).ToLower();
            if ("epub" != extension) { throw new Exception($"Type {extension} not supported."); }

            Extract();

            GetMeta();

            GetNavigationAndCover();

            GetId();

            GetXhtml();

            GetPicture();

            GetNumber();

            GetBookmarks();
        }

        ~EpubInfo() { Dispose(); }

        public void Dispose()
        {
            EpubExtract?.Delete(true);
            EpubExtract = null;
            IsDisposed = true;
        }

        public void CopyAllPictures(DirectoryInfo dir)
        {

            FileInfo pictureSource;
            FileInfo pictureDestination;

            if (false == dir.Exists)
            {
                System.IO.Directory.CreateDirectory(dir.FullName);
            }

            if (null != Cover)
            {
                pictureDestination = new FileInfo(Path.Combine(dir.FullName, Path.GetFileName(Cover.FullName)));
                if (true == Cover.Exists)
                {
                    if (false == pictureDestination.Exists)
                    {
                        File.Copy(Cover.FullName, pictureDestination.FullName);
                    }
                }
            }

            foreach (var page in Pages)
            {
                pictureSource = new FileInfo(page.Picture);
                pictureDestination = new FileInfo(Path.Combine(dir.FullName,page.Number.ToString("000000") + pictureSource.Extension));

                if (false == pictureDestination.Exists)
                {
                    File.Copy(pictureSource.FullName, pictureDestination.FullName);
                }
            }
        }

#if false
        public void CopyBookmarks()
        {
            var cbz = @"C:\Users\PC01\Desktop\Neuer Ordner (3)\Kawakami_Meine-Wiedergeburt-als-Schleim_9783753901800.cbz";
            var Cbz = new FileInfo(cbz);

            if (null == Cbz || null == Cbz.DirectoryName) { return; }

            FileInfo pictureSource;
            FileInfo pictureDestination;
            PageDto page;

            DirectoryInfo dir;

            for (int i = 0; i < Bookmarks.Count; i++)
            {
                dir = new DirectoryInfo(Path.Combine(
                    Cbz.DirectoryName,
                    Path.GetFileNameWithoutExtension(Cbz.FullName) + "-out" + i));

                if (false == dir.Exists)
                {
                    System.IO.Directory.CreateDirectory(dir.FullName);
                }

                if (null != Cover && 0 == i)
                {
                    pictureDestination = new FileInfo(Path.Combine(dir.FullName, Path.GetFileName(Cover.FullName)));
                    if (true == Cover.Exists)
                    {
                        if (false == pictureDestination.Exists)
                        {
                            File.Copy(Cover.FullName, pictureDestination.FullName);
                        }
                    }
                }

                int k = 0;
                for (int j = Bookmarks[i].StartIndex; j < (Bookmarks[i].EndIndex + 1); j++)
                {
                    page = Pages[j];
                    pictureSource = new FileInfo(page.Picture);
                    pictureDestination = new FileInfo(Path.Combine(dir.FullName, k.ToString("000000") + pictureSource.Extension));

                    if (false == pictureDestination.Exists)
                    {
                        File.Copy(pictureSource.FullName, pictureDestination.FullName);
                    }

                    k++;
                }
            }
        }
#endif

        private void Extract()
        {
            if (null == Epub || null == Epub.DirectoryName) { return; }

            EpubExtract = new DirectoryInfo(Path.Combine(
                Epub.DirectoryName,
                Path.GetFileNameWithoutExtension(
                    Epub.FullName)));

            System.IO.Compression.ZipFile.ExtractToDirectory(Epub.FullName, EpubExtract.FullName, true);

            Oebps = new DirectoryInfo(Path.Combine(EpubExtract.FullName, @"OEBPS"));

            ContentFile = new FileInfo(Path.Combine(Oebps.FullName, @"content.opf"));

            if (true == ContentFile.Exists)
            {
                ContentXdoc = XDocument.Load(ContentFile.FullName);
            }

        }

        private void GetMeta()
        {
            if (null == ContentXdoc || null == Epub) { return; }

            var root = ContentXdoc
                .Root;

            if (null != root)
            {
                var versionString = root
                    .Attributes()
                    .Where(a => "version" == a.Name.LocalName)
                    .FirstOrDefault()?.Value ?? "";

                Meta.Version = double.TryParse(versionString, NumberStyles.Number, CultureInfo.InvariantCulture, out double version) ? version : -1;

                var metadataList = root
                    .Elements()
                    .Where(e => e.Name.LocalName == "metadata")
                    .Elements()
                    .ToList();

                Meta.Title = metadataList
                    .Where(e => e.Name.LocalName == "title")
                    .FirstOrDefault()?.Value;

                Meta.Creator = metadataList
                    .Where(e => e.Name.LocalName == "creator")
                    .FirstOrDefault()?.Value;

                Meta.Publisher = metadataList
                    .Where(e => e.Name.LocalName == "publisher")
                    .FirstOrDefault()?.Value;

                var dateString = metadataList
                    .Where(e => e.Name.LocalName == "date")
                    .FirstOrDefault()?.Value;

                Meta.Date = DateTime.TryParse(dateString, out DateTime dateTime) ? dateTime : null;

                Meta.Language = metadataList
                    .Where(e => e.Name.LocalName == "language")
                    .FirstOrDefault()?.Value;

                Meta.Identifier = metadataList
                    .Where(e => e.Name.LocalName == "identifier")
                    .FirstOrDefault()?.Value;
            }
        }

        private void GetNavigationAndCover()
        {
            if (null == ContentXdoc || null == Oebps) { return; }

            var manifestList = ContentXdoc
                .Root?
                .Elements()
                .Where(e => e.Name.LocalName == "manifest")
                .Elements()
                .ToList() ?? new List<XElement>();

            var navigationFile = manifestList
                .Where(e => "nav" == e.Attributes().Where(a => "properties" == a.Name.LocalName).FirstOrDefault()?.Value)
                .Where(e => null != e.Attribute("href"))
                .Select(a => a.Attribute("href").Value)
                .FirstOrDefault() ?? "";

            if ("" != navigationFile)
            {
                Navigation = new FileInfo(Path.Combine(Oebps.FullName, navigationFile));
                if (true == Navigation.Exists)
                {
                    NavigationXdoc = XDocument.Load(Navigation.FullName);
                }
            }


            var coverFile = manifestList
                    .Where(e => "cover-image" == e.Attributes().Where(a => "properties" == a.Name.LocalName).FirstOrDefault()?.Value)
                    .Where(e => null != e.Attribute("href"))
                    .Select(a => a.Attribute("href").Value)
                    .FirstOrDefault() ?? "";

            if ("" != coverFile)
            {
                Cover = new FileInfo(Path.Combine(Oebps.FullName, coverFile));
            }
        }

        private void GetId()
        {
            if (null == ContentXdoc) { return; }

            var nameList = ContentXdoc
                .Root?
                .Elements()
                .Where(e => e.Name.LocalName == "spine")
                .Elements()
                .Where(e => null != e.Attribute("idref"))
                .Select(e => e.Attribute("idref").Value)
                .ToList() ?? new List<string>();

            for (int i = 0; i < nameList.Count; i++)
            {
                var page = new PageDto
                {
                    Id = nameList[i]
                };

                Pages.Add(page);
            }

        }

        private void GetXhtml()
        {
            if (null == ContentXdoc || null == Oebps) { return; }


            var manifestList = ContentXdoc
                .Root?
                .Elements()
                .Where(e => e.Name.LocalName == "manifest")
                .Elements()
                .Where(e => null != e.Attribute("id") && null != e.Attribute("href"))
                .Select(e => new KeyValuePair<string, string>(e.Attribute("id").Value, e.Attribute("href").Value))
                .ToDictionary(e => e.Key, e => e.Value)
                 ?? new Dictionary<string, string>();

            foreach (var name in Pages)
            {
                var xhtmlFile = manifestList[name.Id];
                if (null != xhtmlFile)
                {
                    name.Xhtml = xhtmlFile;
                    name.XhtmlFull = Path.Combine(Oebps.FullName, xhtmlFile);
                }
            }
        }

        private void GetPicture()
        {
            if (null == Oebps) { return; }

            string picture;
            string type;
            XElement? element;

            foreach (var page in Pages)
            {
                XDocument xdoc = XDocument.Load(page.XhtmlFull);

                element = xdoc
                    .Root?
                    .Elements()
                    .Where(e => e.Name.LocalName == "body")
                    .Elements()
                    .Where(e => e.Name.LocalName == "div")
                    .FirstOrDefault();

                picture = element?
                    .Elements()
                    .Where(e => null != e.Attribute("src"))
                    .Select(e => e.Attribute("src")?.Value)
                    .FirstOrDefault() ?? "";

                type = element?
                    .Attributes()
                    .Where(a => "type" == a.Name.LocalName)
                    .FirstOrDefault()?.Value ?? "";

                page.EpubType = Enum.TryParse<EpubTypes>(type.Replace("-",""), ignoreCase: true, out EpubTypes pageType)
                    ? pageType
                    : EpubTypes.None;

                if (null != picture)
                {
                    page.Picture = Path.Combine(Oebps.FullName, picture);
                }
            }
        }

        private void GetNumber()
        {
            if (null == Navigation || null == NavigationXdoc) { return; }

            if (false == Navigation.Exists)
            {
                Dictionary<string, int> bookmarks = new Dictionary<string, int>();

                string xhtml;
                int number;

                var listList = NavigationXdoc
                    .Root?
                    .Elements()
                    .Where(e => e.Name.LocalName == "body")
                    .Elements()
                    .Where(e => e.Name.LocalName == "nav")
                    .Where(e => "page-list" == e.Attributes().Where(a => "type" == a.Name.LocalName).FirstOrDefault()?.Value)
                    .Elements()
                    .Where(e => e.Name.LocalName == "ol")
                    .Elements()
                    .Where(e => e.Name.LocalName == "li")
                    .ToList() ?? new List<XElement>();

                foreach (var element in listList)
                {
                    var a = element
                        .Elements()
                        .Where(e => e.Name.LocalName == "a")
                        .FirstOrDefault();

                    xhtml = a?.Attribute("href")?.Value ?? "";
                    if ("" != xhtml)
                    {
                        var value = a?.Value ?? "";

                        if ("" != value)
                        {
                            number = int.TryParse(value, out int dummy)
                                ? dummy
                                : -1;

                            bookmarks.Add(xhtml, number);
                        }
                    }
                }

                foreach (var page in Pages)
                {
                    page.Number = bookmarks.TryGetValue(page.Xhtml, out int value)
                        ? value
                        : -1;

                    if (-1 != value)
                    {
                        bookmarks.Remove(page.Xhtml);
                    }

                }
            }
            else
            {
                for (int i = 0; i < Pages.Count; i++)
                {
                    Pages[i].Number = i + 1;
                }
            }
        }

        private void GetBookmarks()
        {
            if (null == NavigationXdoc) { return; }

            string xhtml;
            string title;

            var listList = NavigationXdoc
                .Root?
                .Elements()
                .Where(e => e.Name.LocalName == "body")
                .Elements()
                .Where(e => e.Name.LocalName == "nav")
                .Where(e => "toc" == e.Attributes().Where(a => "type" == a.Name.LocalName).FirstOrDefault()?.Value)
                .Elements()
                .Where(e => e.Name.LocalName == "ol")
                .Elements()
                .Where(e => e.Name.LocalName == "li")
                .ToList() ?? new List<XElement>();

            foreach (var element in listList)
            {
                var a = element
                    .Elements()
                    .Where(e => e.Name.LocalName == "a")
                    .FirstOrDefault();

                xhtml = a?.Attribute("href")?.Value ?? "";
                if ("" != xhtml)
                {
                    title = a?.Value ?? "";

                    if ("" != title)
                    {
                        Bookmarks.Add(
                            new BookmarkDto()
                            {
                                Xhtml = xhtml,
                                Title = title
                            });
                    }
                }
            }


            int bookmarksIndex = 0;
            Bookmarks[bookmarksIndex].StartIndex = 0;
            Bookmarks[Bookmarks.Count - 1].EndIndex = Pages.Count - 1;
            string bookmarkXhtml = Bookmarks[bookmarksIndex].Xhtml;
            string bookmarkTitle = Bookmarks[bookmarksIndex].Title;
            PageDto page;

            for (int i = 0; i < Pages.Count; i++)
            {
                page = Pages[i];

                if (page.Xhtml == bookmarkXhtml)
                {
                    page.Bookmark = bookmarkTitle;

                    // Save page infos of current bookmark
                    Bookmarks[bookmarksIndex].EndIndex = i;
                    bookmarksIndex++;

                    if (bookmarksIndex < (Bookmarks.Count))
                    {
                        // Save page infos of next bookmark
                        Bookmarks[bookmarksIndex].StartIndex = i + 1;
                        bookmarkXhtml = Bookmarks.ElementAt(bookmarksIndex).Xhtml;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
