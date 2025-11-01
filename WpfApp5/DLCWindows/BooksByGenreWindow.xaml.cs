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

namespace WpfApp5.DLCWindows
{
    public partial class BooksByGenreWindow : Window
    {
        public BooksByGenreWindow(int genreId)
        {
            InitializeComponent();
            LoadBooks(genreId);
        }

        private void LoadBooks(int genreId)
        {
            using (var context = new ApplicationContext())
            {
                var books = context.Books
                    .Where(b => b.GenreId == genreId)
                    .ToList();
                BooksDataGrid.ItemsSource = books;
            }
        }
    }
}
