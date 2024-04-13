using System.Globalization;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Terp.UI.ViewModels;

namespace Terp.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public MainWindow()
        {

            InitializeComponent();

            SwitchLanguage("vn");

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("vn");



        }
        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(dictionary);
            switch (languageCode)
            {
                case "en":
                    dictionary.Source = new Uri("Views\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "kr":
                    dictionary.Source = new Uri("Views\\StringResources.kr.xaml", UriKind.Relative);
                    break;
                case "vn":
                    dictionary.Source = new Uri("Views\\StringResources.vn.xaml", UriKind.Relative);
                    break;
                default:

                    dictionary.Source = new Uri("Views\\StringResources.vn.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = new CultureInfo(languageCode);
        }



        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            SwitchLanguage("en");

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("kr");

        }

    }
}