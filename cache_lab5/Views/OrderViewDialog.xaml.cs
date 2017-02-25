using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cache_lab5.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderViewDialog.xaml
    /// </summary>
    public partial class OrderViewDialog : Window
    {
        public OrderViewDialog()
        {
            InitializeComponent();
        }

        public class CacheIntegerConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return Int32.Parse((string)value);
            }
        }
    }
}
