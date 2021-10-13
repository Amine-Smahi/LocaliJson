using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace LocaliJson.Models
{
    public class AppCulture
    {
        public AppCulture(string culture, bool isDefault = false)
        {
            Culture = culture;
            IsDefault = isDefault;
        }

        private string Culture { get; }
        public bool IsDefault { get; }

        public CultureInfo GetCulture()
        {
            return new(Culture);
        }

        public RequestCulture GetRequestCulture()
        {
            return new(GetCulture());
        }
    }
}