using Chris82111.Epub.Enums;

namespace Chris82111.Epub.Dto
{
    public class PageDto
    {
        /// <summary>
        ///         Page number
        /// </summary>
        public int Number { get; set; } = -1;
        public string Id { get; set; } = "";
        public string Xhtml { get; set; } = "";
        public string XhtmlFull { get; set; } = "";

        /// <summary>
        ///         Full path of the picture e.g. `path/pic.jpg` 
        /// </summary>
        public string Picture { get; set; } = "";

        /// <summary>
        ///         Bookmakrs listed in `nav.xhtml` or `toc.xhtml` file
        /// </summary>
        public string Bookmark { get; set; } = "";

        /// <summary>
        ///         EpubType, information stored in each `*.xhtml` file
        /// </summary>
        public EpubTypes EpubType { get; set; } = EpubTypes.None;
    }
}
