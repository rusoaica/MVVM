/// Written by: Yulia Danilova
/// Creation Date: 5th of November, 2019
/// Purpose: XAML Converter for image sources
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
#endregion

namespace MVVM.Views.Common.Converters
{
    /// <summary>
    /// A converter that accepts <see cref="SwitchConverterCase"/>s and converts them to the 
    /// Then property of the case.
    /// </summary>
    public class ImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="_value">The value produced by the binding source.</param>
        /// <param name="_targetType">The type of the binding target property.</param>
        /// <param name="_parameter">The converter parameter to use.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            if (!string.IsNullOrEmpty(_value as string))
                return new BitmapImage(new Uri(@"pack://application:,,,/MVVM;component/Resources/" + (_value as string), UriKind.Absolute));
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="_value">The value that is produced by the binding target.</param>
        /// <param name="_targetType">The type to convert to.</param>
        /// <param name="_parameter">The converter parameter to use.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            // According to https://msdn.microsoft.com/en-us/library/system.windows.data.ivalueconverter.convertback(v=vs.110).aspx#Anchor_1
            // (kudos Scott Chamberlain), if you do not support a conversion back you should return a Binding.DoNothing or a DependencyProperty.UnsetValue
            return Binding.DoNothing;
            // Original code:
            // throw new NotImplementedException();
        }
    }
}
