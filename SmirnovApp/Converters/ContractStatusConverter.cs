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
    public class ContractStatusConverter : IValueConverter
    {
        private static readonly Dictionary<ContractStatus, string> _enumValues = new()
        {
            {ContractStatus.NotPerformed, "Выполняется"},
            {ContractStatus.ServiceProvided, "Выполнен"},
            {ContractStatus.Terminated, "Расторгнут"}
        };

        public static string GetString(ContractStatus contractStatus) => _enumValues?[contractStatus];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumValue = (ContractStatus) value;
            return GetString(enumValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string) value;
            return _enumValues.SingleOrDefault(x => x.Value == stringValue).Key;
        }
    }
}
