namespace Chris82111.Epub.Dto
{
    public class BookmarkDto
    {
        public string Xhtml { get; set; } = "";
        public string Title { get; set; } = "";

        /// <summary>
        ///         First page of this chapter
        /// <br/>   Numbers smaller than 0 are not valid
        /// </summary>
        public int StartIndex { get; set; } = -1;

        /// <summary>
        ///         Last page of this chapter
        /// <br/>   Numbers smaller than 0 are not valid
        /// </summary>
        public int EndIndex { get; set; } = -1;
    }
}
