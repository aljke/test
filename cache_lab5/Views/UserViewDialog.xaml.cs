using InterSystems.Data.CacheTypes;
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
    /// Логика взаимодействия для UserViewDialog.xaml
    /// </summary>
    public partial class UserViewDialog : Window
    {
        public UserViewDialog()
        {
            InitializeComponent();
        }

        public class CacheListConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                CacheListOfStrings l = (CacheListOfStrings)value;
                string phones = string.Join(",", l.ToList());
                return phones;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                /*
                string Phones = (string)value;
                CacheListOfStrings res = new CacheListOfStrings();
                
                List<string> phones = Phones.Split(',').ToList();
                if (phones.Count > 0)
                {
                    foreach (string str in phones)
                    {
                        res.Add(str);
                        //MessageBox.Show(str);
                    }
                }
                return res;
            }*/
                return null;
            }
        }
    }
}
