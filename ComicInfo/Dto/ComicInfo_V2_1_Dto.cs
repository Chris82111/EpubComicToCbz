using Chris82111.ComicInfo.Enums;
using Chris82111.ComicInfo.Model;
using Chris82111.ComicInfo.Types;

namespace Chris82111.ComicInfo.Dto
{
    /// <summary>
    /// <see href="https://anansi-project.github.io/docs/comicinfo/schemas/v2.1"/>
    /// </summary>
    public class ComicInfo_V2_1_Dto : ComicInfoBase
    {
        /// <summary>
        ///         Title of the book.
        /// </summary>
        public string? Title { get; set; } = null;

        /// <summary>
        ///         Title of the series the book is part of.
        /// </summary>
        public string? Series { get; set; } = null;

        /// <summary>
        ///         Number of the book in the series.
        /// </summary>
        public string? Number { get; set; } = null;

        /// <summary>
        ///         The total number of books in the series.
        /// <br/> 
        /// <br/>   The Count could be different on each book in a series.
        /// <br/>   Consuming applications should consider using only the value
        /// <br/>   for the latest book in the series.
        /// </summary>
        public int? Count { get; set; } = null;
        public bool ShouldSerializeCounth() { return Count.HasValue; } // This removes the element from the output, otherwise it is created: <Type xsi:nil="true" />

        /// <summary>
        ///         Volume containing the book. Volume is a notion that is specific
        /// <br/>   to US Comics, where the same series can have multiple volumes.
        /// <br/>   Volumes can be referenced by numer (1, 2, 3…) or by year (2018, 2020…).
        /// </summary>
        public int? Volume { get; set; } = null;
        public bool ShouldSerializeVolume() { return Volume.HasValue; }

        /// <summary>
        ///         Quite specific to US comics, some books can be part of cross-over
        /// <br/>   story arcs. Those fields can be used to specify an alternate series,
        /// <br/>   its number and count of books.
        /// </summary>
        public string? AlternateSeries { get; set; } = null;

        /// <summary><inheritdoc cref="AlternateSeries"/></summary>
        public string? AlternateNumber { get; set; } = null;

        /// <summary><inheritdoc cref="AlternateSeries"/></summary>
        public int? AlternateCount { get; set; } = null;
        public bool ShouldSerializeAlternateCount() { return AlternateCount.HasValue; }

        /// <summary>
        ///         A description or summary of the book.
        /// </summary>
        public string? Summary { get; set; } = null;

        /// <summary>
        ///         A free text field, usually used to store information about the
        /// <br/>   application that created the ComicInfo.xml file.
        /// </summary>
        public string? Notes { get; set; } = null;

        /// <summary>
        ///         Usually contains the release date of the book.
        /// </summary>
        public int? Year { get; set; } = null;
        public bool ShouldSerializeYear() { return Year.HasValue; }

        /// <summary><inheritdoc cref="Year"/></summary>
        public int? Month { get; set; } = null;
        public bool ShouldSerializeMonth() { return Month.HasValue; }

        /// <summary><inheritdoc cref="Year"/></summary>
        public int? Day { get; set; } = null;
        public bool ShouldSerializeDay() { return Day.HasValue; }


        #region CreatorFields

        /// <summary>
        ///         Person or organization responsible for creating the scenario.
        /// <br/>
        /// <br/>
        /// <remarks>
        ///         According to the schema, each creator element can only be present once.
        /// <br/>   In order to cater for multiple creator with the same role,
        /// <br/>   it is accepted that values are comma separated.
        ///</remarks>
        /// </summary>
        public string? Writer { get; set; } = null;

        /// <summary>
        ///         Person or organization responsible for drawing the art.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Penciller { get; set; } = null;

        /// <summary>
        ///         Person or organization responsible for inking the pencil art.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Inker { get; set; } = null;

        /// <summary>
        ///         Person or organization responsible for applying color to drawings.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Colorist { get; set; } = null;

        /// <summary>
        ///         Person or organization responsible for drawing text and speech bubbles.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Letterer { get; set; } = null;

        /// <summary>
        ///         Person or organization responsible for drawing the cover art.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? CoverArtist { get; set; } = null;

        /// <summary>
        ///         A person or organization contributing to a resource by revising or
        /// <br/>   elucidating the content, e.g., adding an introduction, notes, or
        /// <br/>   other critical matter. An editor may also prepare a resource for
        /// <br/>   production, publication, or distribution.
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Editor { get; set; } = null;

        /// <summary>
        ///         A person or organization who renders a text from one language into another,
        /// <br/>   or from an older form of a language into the modern form.
        /// <br/>
        /// <br/>   This can also be used for fan translations("scanlator"). 
        /// <br/>
        /// <br/>   <inheritdoc cref="Writer" path="/summary/remarks"/>
        /// </summary>
        public string? Translator { get; set; } = null;

        #endregion


        /// <summary>
        ///         A person or organization responsible for publishing, releasing, or issuing a resource.
        /// </summary>
        public string? Publisher { get; set; } = null;

        /// <summary>
        ///         An imprint is a group of publications under the umbrella of a larger imprint or
        /// <br/>   a Publisher. For example, Vertigo is an Imprint of DC Comics.
        /// </summary>
        public string? Imprint { get; set; } = null;

        /// <summary>
        ///         Genre of the book or series. For example, Science-Fiction or Shonen.
        /// <br/>
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? Genre { get; set; } = null;

        /// <summary>
        ///         Tags of the book or series. For example, ninja or school life.
        /// <br/>
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? Tags { get; set; } = null;

        /// <summary>
        ///         A URL pointing to a reference website for the book.
        /// <br/>
        /// <br/>   It is accepted that multiple values are space separated.
        /// <br/>   If a space is a part of the url it must be percent encoded.
        /// </summary>
        public string? Web { get; set; } = null;

        /// <summary>
        /// The number of pages in the book.
        /// </summary>
        public int? PageCount { get; set; } = null;
        public bool ShouldSerializePageCount() { return PageCount.HasValue; }

        /// <summary>
        ///         A language code describing the language of the book.
        /// <br/>
        /// <br/>   Without any information on what kind of code this element is
        /// <br/>   supposed to contain, it is recommended to use the IETF BCP 47
        /// <br/>   language tag, which can describe the language but also the
        /// <br/>   script used. This helps to differentiate languages with
        /// <br/>   multiple scripts, like Traditional and Simplified Chinese.
        /// </summary>
        public string? LanguageISO { get; set; } = null;

        /// <summary>
        ///         The original publication's binding format for scanned
        /// <br/>   physical books or presentation format for digital sources.
        /// <br/>
        /// <br/>   "TBP", "HC", "Web", "Digital" are common designators.
        /// </summary>
        public string? Format { get; set; } = null;

        /// <summary>
        ///         Whether the book is in black and white.
        /// </summary>
        public YesNo? BlackAndWhite { get; set; } = null;
        public bool ShouldSerializeBlackAndWhite() { return BlackAndWhite.HasValue; }

        /// <summary>
        ///         Whether the book is a manga. This also defines the reading
        /// <br/>   direction as right-to-left when set to YesAndRightToLeft.
        /// </summary>
        public Manga? Manga { get; set; } = null;
        public bool ShouldSerializeManga() { return Manga.HasValue; }

        /// <summary>
        ///         Characters present in the book.
        /// <br/>
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? Characters { get; set; } = null;

        /// <summary>
        ///         Teams present in the book. Usually refer to super-hero teams (e.g. Avengers).
        /// <br/>
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? Teams { get; set; } = null;

        /// <summary>
        ///         Locations mentioned in the book.
        /// <br/>
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? Locations { get; set; } = null;

        /// <summary>
        ///         A free text field, usually used to store information about who scanned the book.
        /// </summary>
        public string? ScanInformation { get; set; } = null;

        /// <summary>
        ///         The story arc that books belong to.
        /// <br/>   
        /// <br/>   For example, for Undiscovered Country, issues 1-6 are part
        /// <br/>   of the Destiny story arc, issues 7-12 are part of the Unity
        /// <br/>   story arc.
        /// </summary>
        public string? StoryArc { get; set; } = null;

        /// <summary>
        ///         While StoryArc was originally designed to store the arc
        /// <br/>   within a series, it was often used to indicate that a book
        /// <br/>   was part of a reading order, composed of books from
        /// <br/>   multiple series. Mylar for instance was using the field as such.
        /// <br/>
        /// <br/>   Since StoryArc itself wasn't able to carry the information
        /// <br/>   about ordering of books within a reading order, StoryArcNumber was added.
        /// <br/>
        /// <br/>   StoryArc and StoryArcNumber can work in combination,
        /// <br/>   to indicate in which position the book is located at for
        /// <br/>   a specific reading order.
        /// <br/>
        /// <br/>   It is accepted that multiple values can be specified for
        /// <br/>   both StoryArc and StoryArcNumber. Multiple values are comma separated.
        /// </summary>
        public string? StoryArcNumber { get; set; } = null;

        /// <summary>
        ///         A group or collection the series belongs to.
        /// <br/>   It is accepted that multiple values are comma separated.
        /// </summary>
        public string? SeriesGroup { get; set; } = null;

        /// <summary>
        ///         Age rating of the book.
        /// </summary>
        public AgeRating? AgeRating { get; set; } = null;
        public bool ShouldSerializeAgeRating() { return AgeRating.HasValue; }

        /// <summary><inheritdoc cref="ComicPageInfo"/></summary>
        public List<ComicPageInfo>? Pages { get; set; } = null;

        /// <summary><inheritdoc cref="Types.CommunityRating"/></summary>
        public CommunityRating? CommunityRating { get; set; } = null;

        /// <summary>
        ///         Main character or team mentioned in the book.
        /// <br/>
        /// <br/>   It is accepted that a single value should be present.
        /// </summary>
        public string? MainCharacterOrTeam { get; set; } = null;

        /// <summary>
        ///         Review of the book.
        /// </summary>
        public string? Review { get; set; } = null;

        /// <summary>
        ///         A Global Trade Item Number identifying the book.
        /// <br/>   GTIN incorporates other standards like
        /// <br/>   ISBN, ISSN, EAN, or JAN.
        /// </summary>
        public string? GTIN { get; set; } = null;
    }
}
