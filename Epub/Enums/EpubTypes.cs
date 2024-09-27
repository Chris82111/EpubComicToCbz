namespace Chris82111.Epub.Enums
{
    public enum EpubTypes
    {
        None = 0,
        /// <summary>
        /// The book cover(s), jacket information, etc.
        /// </summary>
        Cover,

        /// <summary>
        /// Table of contents
        /// </summary>
        Toc,

        /// <summary>
        /// First "real" page of content (e.g. "Chapter 1")
        /// </summary>
        Bodymatter,

        /// <summary>
        /// Page with possibly title, author, publisher, and other metadata
        /// </summary>
        Titlepage,

        Frontmatter,

        Backmatter,

        /// <summary>
        /// List of illustrations
        /// </summary>
        Loi,

        /// <summary>
        /// List of tables
        /// </summary>
        Lot,

        Preface,

        Bibliography,

        /// <summary>
        /// Back-of-book style index
        /// </summary>
        Index,

        Glossary,

        Acknowledgments,
    }
}
