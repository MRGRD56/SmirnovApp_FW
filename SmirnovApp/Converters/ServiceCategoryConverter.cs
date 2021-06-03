using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SmirnovApp.Model.DbModels;

namespace SmirnovApp.Converters
{
    public class ServiceCategoryConverter : IValueConverter
    {
        private static readonly Dictionary<ServiceCategory, string> _enumValues = new Dictionary<ServiceCategory, string>()
        {
            {ServiceCategory.Legal, "Юридическая"},
            {ServiceCategory.Realtor, "Риэлторская"}
        };

        public static string GetString(ServiceCategory serviceCategory) => _enumValues?[serviceCategory];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumValue = (ServiceCategory)value;
            return GetString(enumValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value;
            return _enumValues.SingleOrDefault(x => x.Value == stringValue).Key;
        }
    }
}
