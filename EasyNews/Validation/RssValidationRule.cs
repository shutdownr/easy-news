using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
using EasyNews.Helpers;

namespace EasyNews.Validation
{
    class RssValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false,"Field is empty");
            }
            var url = value.ToString();
            Trace.WriteLine(Thread.CurrentThread.ManagedThreadId + " IsValidRssFeed");
            var result = RssManager.Instance.IsValidRssFeed(url).Result;

            if (result)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "No valid RSS");
        }
    }
}
