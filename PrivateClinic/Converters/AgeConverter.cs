using System;
using System.Globalization;
using System.Windows.Data;

namespace PrivateClinic.Converters
{
    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string yearOfBirthString)
            {
                if (int.TryParse(yearOfBirthString, out int yearOfBirth))
                {
                    int currentYear = DateTime.Now.Year;
                    int age = currentYear - yearOfBirth;
                    return age;
                }
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from age to year of birth is not supported.");
        }
    }
}
