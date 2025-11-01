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
namespace WpfApp5.DLCWindows
{ 
    public partial class GenreSearchWindow : Window
    {
        public GenreSearchWindow()
        {
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            using (var context = new ApplicationContext())
            {
                var genres = context.Genres.ToList();
                GenresDataGrid.ItemsSource = genres;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenresDataGrid.SelectedItem is Genre selectedGenre)
            {
                var booksWindow = new BooksByGenreWindow(selectedGenre.Id);
                booksWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите жанр!");
            }
        }
    }
}
