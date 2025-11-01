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
using WpfApp5.DLCWindows;

namespace WpfApp5.WindowsClasses
{

    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void NameButton_Click(object sender, RoutedEventArgs e)
        {
            var nameSearchWindow = new NameSearchWindow();
            nameSearchWindow.Show();
        }

        private void AuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var authorSearchWindow = new AuthorSearchWindow();
            authorSearchWindow.Show();
        }

        private void GenreButton_Click(object sender, RoutedEventArgs e)
        {
            var genreSearchWindow = new GenreSearchWindow();
            genreSearchWindow.Show();
        }
    }
}
