using Chris82111.ComicInfo.Model;
using Chris82111.ComicInfo.Types;

namespace Chris82111.ComicInfo.Dto
{
    public class ComicInfoEpubDto : ComicInfoBase
    {
        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Title"/></summary>
        public string? Title { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Series"/></summary>
        public string? Series { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Number"/></summary>
        public string? Number { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Volume"/></summary>
        public string? Volume { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Year"/></summary>
        public int? Year { get; set; } = null;
        public bool ShouldSerializeYear() { return Year.HasValue; }

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Month"/></summary>
        public int? Month { get; set; } = null;
        public bool ShouldSerializeMonth() { return Month.HasValue; }

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Day"/></summary>
        public int? Day { get; set; } = null;
        public bool ShouldSerializeDay() { return Day.HasValue; }

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Writer"/></summary>
        public string? Writer { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.Publisher"/></summary>
        public string? Publisher { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.PageCount"/></summary>
        public int? PageCount { get; set; } = null;
        public bool ShouldSerializePageCount() { return PageCount.HasValue; }

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.LanguageISO"/></summary>
        public string? LanguageISO { get; set; } = null;

        /// <summary><inheritdoc cref=" ComicInfo_V2_1_Dto.GTIN"/></summary>
        public string? GTIN { get; set; } = null;

        /// <summary><inheritdoc cref="ComicInfo_V2_1_Dto.Pages"/></summary>
        public List<ComicPageInfo>? Pages { get; set; } = null;
    }
}