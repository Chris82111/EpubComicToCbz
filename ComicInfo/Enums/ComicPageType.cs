namespace Chris82111.ComicInfo.Enums
{
    /// <summary>
    /// Type of the page:
    /// </summary>
    public enum ComicPageType
    {
        /// <summary>
        /// FrontCover
        /// </summary>
        FrontCover = 0,

        /// <summary>
        /// InnerCover: sometimes found inside the book as a second cover
        /// </summary>
        InnerCover,

        /// <summary>
        /// Roundup: summary of previous issues
        /// </summary>
        Roundup,

        /// <summary>
        /// Story
        /// </summary>
        Story,

        /// <summary>
        /// Advertisement
        /// </summary>
        Advertisement,

        /// <summary>
        /// Editorial
        /// </summary>
        Editorial,

        /// <summary>
        /// Letters: fan letters
        /// </summary>
        Letters,

        /// <summary>
        /// Preview: sneak preview of the next book, or another comic
        /// </summary>
        Preview,

        /// <summary>
        /// BackCover
        /// </summary>
        BackCover,

        /// <summary>
        /// Other: for anything not covered above
        /// </summary>
        Other,

        /// <summary>
        /// Delete: indicate that the page should not be shown by readers
        /// </summary>
        Deleted,
    }
}
