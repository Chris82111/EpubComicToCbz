using System.Globalization;

namespace Chris82111.Domain.Helper
{
    public static class LanguageHelper
    {
        public static CultureInfo? TryGetCultureInfo(string? input, CultureInfo? defaultValue = null)
        {
            CultureInfo? cultureInfo = null;

            do
            {
                if (null != input && "" != input)
                {
                    var inputLower = input.ToLower();

                    // Get all available cultures on the current system.
                    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                    if (2 == input.Length)
                    {
                        cultureInfo = cultures.Where(c => c.TwoLetterISOLanguageName.ToLower() == inputLower).FirstOrDefault();
                        if (null != cultureInfo) { break; }
                    }

                    if (3 == input.Length)
                    {
                        cultureInfo = cultures.Where(c => c.ThreeLetterISOLanguageName.ToLower() == inputLower).FirstOrDefault();
                        if (null != cultureInfo) { break; }
                    }

                    cultureInfo = cultures.Where(c => c.Name.ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    cultureInfo = cultures.Where(c => c.NativeName.ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    cultureInfo = cultures.Where(c => c.DisplayName.ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    cultureInfo = cultures.Where(c => c.EnglishName.ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    cultureInfo = cultures.Where(c => c.EnglishName.Substring(0, inputLower.Length).ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    cultureInfo = cultures.Where(c => c.NativeName.Substring(0, inputLower.Length).ToLower() == inputLower).FirstOrDefault();
                    if (null != cultureInfo) { break; }

                    try
                    {
                        cultureInfo = new CultureInfo(input);
                        if (null != cultureInfo) { break; }
                    }
                    catch {; }
                }

                cultureInfo = defaultValue;

            } while (false);

            return cultureInfo;
        }
    }
}
