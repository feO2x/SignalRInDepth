using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SignalRWpfClient
{
    public sealed class SeverityLevelToColorConverter : IValueConverter
    {
        public SeverityLevelToColorConverter()
        {
            NormalColor = Color.FromRgb(130, 219, 219);
            WarningColor = Color.FromRgb(223, 196, 189);
            ErrorColor = Color.FromRgb(168, 90, 83);
        }

        public Color NormalColor { get; set; }
        public Color WarningColor { get; set; }
        public Color ErrorColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var severityLevel = (SeverityLevel) value;
            switch (severityLevel)
            {
                case SeverityLevel.Normal:
                    return NormalColor;
                case SeverityLevel.Warning:
                    return WarningColor;
                case SeverityLevel.Error:
                    return ErrorColor;
            }
            throw new ArgumentException(string.Format("{0} is an unknown severity level.", severityLevel));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}