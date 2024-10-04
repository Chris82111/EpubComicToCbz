using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Chris82111.Domain.Enums
{
    public class EnumHelper<T> where T : Enum
    {
        public static readonly List<T> EnumList = ((T[])System.Enum.GetValues(typeof(T))).ToList();

        public static readonly Dictionary<T, string> EnumString = EnumList
            .Select(c => new KeyValuePair<T, string>(c, c.ToString()))
            .ToList()
            .ToDictionary(x => x.Key, x => x.Value);

        public static readonly Dictionary<string, T> StringEnum = EnumString
            .ToDictionary(x => x.Value, x => x.Key);

        public static readonly Dictionary<T, string> EnumDisplay = EnumList
            .Select(c => new KeyValuePair<T, string>(c, ReadAttributeNameOrString<DisplayAttribute>(c)))
            .ToList()
            .ToDictionary(x => x.Key, x => x.Value);

        public static readonly Dictionary<string, T> DisplayEnum = EnumDisplay
            .ToDictionary(x => x.Value, x => x.Key);

        /// <summary>
        /// <see href="https://stackoverflow.com/questions/5850873/enum-hasflag-why-no-enum-setflag"/>
        /// </summary>
        public static void WriteFlag(ref T value, T flag, bool set)
        {
            if (null == value || null == flag)
            {
                return;
            }

            Type underlyingType = Enum.GetUnderlyingType(value.GetType());

            // note: AsInt mean: math integer vs enum (not the c# int type)
            dynamic valueAsInt = Convert.ChangeType(value, underlyingType);
            dynamic flagAsInt = Convert.ChangeType(flag, underlyingType);

            if (set)
            {
                valueAsInt |= flagAsInt;
            }
            else
            {
                valueAsInt &= ~flagAsInt;
            }

            value = (T)valueAsInt;
        }

        public static void SetFlag(ref T value, T flag)
        {
            WriteFlag(ref value, flag, true);
        }

        public static void ClearFlag(ref T value, T flag)
        {
            WriteFlag(ref value, flag, false);
        }

        private static object? GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        private static string? ReadAttributeNameOrDefault<ATTRIBUTE>(T enumItem) where ATTRIBUTE : Attribute
        {
            string? name = null;

            MemberInfo[] memberInfo = enumItem.GetType().GetMember(enumItem.ToString());

            if (null != memberInfo && 0 < memberInfo.Length)
            {
                Attribute? descriptionAttribute = Attribute.GetCustomAttribute(memberInfo[0], typeof(ATTRIBUTE)) as ATTRIBUTE;

                if (null != descriptionAttribute)
                {
                    name = (string?)GetPropValue(descriptionAttribute, "Name");
                }
            }

            return name;
        }

        private static string ReadAttributeNameOrString<ATTRIBUTE>(T enumItem) where ATTRIBUTE : Attribute
        {
            return ReadAttributeNameOrDefault<ATTRIBUTE>(enumItem) ?? enumItem.ToString();
        }
    }
}
