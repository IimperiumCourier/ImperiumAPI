using ImperiumLogistics.SharedKernel.Query;
using ImperiumLogistics.SharedKernel.ViewModel;
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

        public static string ConcatenatePhoneNumber(this PhoneNumberDTO phoneNumber)
        {
            return $"{phoneNumber.CountryCode}{phoneNumber.Number}";
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

        public static string RemoveSpace(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) { return string.Empty; }

            return data.Trim().ToLower();
        }

        public static int PageNumber(this PagedQueryRequest pagedQuery)
        {
            if(pagedQuery == null) { return Utility.DefaultPageNumber; }

            return pagedQuery.PageNumber;
        }

        public static int PageSize(this PagedQueryRequest pagedQuery)
        {
            if (pagedQuery == null) { return Utility.DefaultPageSize; }

            return pagedQuery.PageSize;
        }
    }
}