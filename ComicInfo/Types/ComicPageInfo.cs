using Chris82111.ComicInfo.Enums;

namespace Chris82111.ComicInfo.Types
{
    /// <summary>
    ///         Describes each page of the book.
    /// </summary>
    public class ComicPageInfo
    {  
        /// <summary>
        ///         Page number.
        /// </summary>
        public int Image { get; set; }

        /// <summary><inheritdoc cref="ComicPageType"/></summary>
        public ComicPageType Type { get; set; } = ComicPageInfoDefault.Type;

        /// <summary>
        ///         Whether the page is a double spread.
        /// </summary>
        public bool DoublePage { get; set; } = ComicPageInfoDefault.DoublePage;

        /// <summary>
        ///         File size of the image, supposedly in bytes.
        /// </summary>
        public long ImageSize { get; set; } = ComicPageInfoDefault.ImageSize;

        /// <summary>
        ///         ???
        /// </summary>
        public string Key { get; set; } = ComicPageInfoDefault.Key;

        /// <summary>
        ///         ComicRack uses this field when adding a bookmark in a book.
        /// </summary>
        public string Bookmark { get; set; } = ComicPageInfoDefault.Bookmark;

        /// <summary>
        ///         Width and height of the image in pixels.
        /// </summary>
        public int ImageWidth { get; set; } = ComicPageInfoDefault.ImageWidth;

        /// <summary><inheritdoc cref="ImageWidth"/></summary>
        public int ImageHeight { get; set; } = ComicPageInfoDefault.ImageHeight;
    }
}
