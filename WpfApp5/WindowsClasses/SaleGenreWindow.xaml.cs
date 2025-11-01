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
using WpfApp5.Classes;
using WpfApp5.DLCWindows;

namespace WpfApp5.WindowsClasses
{

    public partial class SaleGenreWindow : Window
    {
        public SaleGenreWindow()
        {
            InitializeComponent();
            RefreshGenres();
        }

        private void RefreshGenres()
        {
            using (var context = new ApplicationContext())
            {
                var genres = context.Genres.ToList();
                GenresDataGrid.ItemsSource = genres;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshGenres();
        }

        private void EditGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenresDataGrid.SelectedItem is Genre selectedGenre)
            {
                var editWindow = new EditGenreDiscountWindow(selectedGenre);
                editWindow.ShowDialog();
                RefreshGenres(); 
            }
            else
            {
                MessageBox.Show("Выберите жанр для редактирования скидки!");
            }
        }
    }
}
