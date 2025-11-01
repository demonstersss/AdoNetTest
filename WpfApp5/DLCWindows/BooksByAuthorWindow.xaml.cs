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
    public partial class BooksByAuthorWindow : Window
    {
        public BooksByAuthorWindow(int authorId)
        {
            InitializeComponent();
            LoadBooks(authorId);
        }

        private void LoadBooks(int authorId)
        {
            using (var context = new ApplicationContext())
            {
                var books = context.Books
                    .Where(b => b.AuthorId == authorId)
                    .ToList();
                BooksDataGrid.ItemsSource = books;
            }
        }
    }
}
