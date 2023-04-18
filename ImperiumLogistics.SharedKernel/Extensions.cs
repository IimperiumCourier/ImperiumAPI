using Newtonsoft.Json;
using System.Globalization;

namespace ImperiumLogistics.SharedKernel
{
    public static class Extensions
    {
        public static string ToJson(this object value)
        {
            if (value == null) return "{ }";
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(value, Formatting.Indented, settings);
        }

        public static string ConvertToElevenDigits(this string phoneNumber)
        {
            if (phoneNumber.StartsWith("+234"))
            {
                phoneNumber = phoneNumber.Replace("+234", "0").Replace("(", "").Replace(")", "").Replace("-", "").Trim();
            }
            else if (phoneNumber.StartsWith("234"))
            {
                phoneNumber = phoneNumber.Replace("234", "0").Replace("(", "").Replace(")", "").Replace("-", "").Trim();
            }
            else
            {
                phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
            }

            return phoneNumber;
        }

        public static string ToSentenceCase(this string phrase)
        {
            if (!string.IsNullOrEmpty(phrase))
            {
                var convertedString = phrase.Split(new char[0]).ToList().Select(e => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(e.ToLower()));
                return string.Join(" ", convertedString);
            }
            return string.Empty;
        }
    }
}