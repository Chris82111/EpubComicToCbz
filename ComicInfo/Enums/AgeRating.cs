using System.ComponentModel.DataAnnotations;

namespace Chris82111.ComicInfo.Enums
{
    public enum AgeRating
    {
        [Display(Name = "Unknown")]
        Unknown = 0,

        [Display(Name = "Adults Only 18+")]
        AdultsOnly18,

        [Display(Name = "Early Childhood")]
        EarlyChildhood,

        [Display(Name = "Everyone")]
        Everyone,

        [Display(Name = "Everyone 10+")]
        Everyone10,

        [Display(Name = "G")]
        G,

        [Display(Name = "Kids to Adults")]
        KidsToAdults,

        [Display(Name = "M")]
        M,

        [Display(Name = "MA15+")]
        MA15,

        [Display(Name = "Mature 17+")]
        Mature17,

        [Display(Name = "PG")]
        PG,

        [Display(Name = "R18+")]
        R18,

        [Display(Name = "Rating Pending")]
        RatingPending,

        [Display(Name = "Teen")]
        Teen,

        [Display(Name = "X18+")]
        X18,
    }
}
