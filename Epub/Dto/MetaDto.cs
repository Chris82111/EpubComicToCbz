namespace Chris82111.Epub.Dto
{
    public class MetaDto
    {
        public double Version { get; set; } = -1;
        public string? Title { get; set; } = null;
        public string? Creator { get; set; } = null;
        public string? Publisher { get; set; } = null;
        public DateTime? Date { get; set; } = null;
        public string? Language { get; set; } = null;
        public string? Identifier { get; set; } = null;
    }
}
