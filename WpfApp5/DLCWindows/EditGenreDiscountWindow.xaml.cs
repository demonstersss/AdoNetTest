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

    public partial class EditGenreDiscountWindow : Window
    {
        private readonly Genre _genreToEdit;

        public EditGenreDiscountWindow(Genre genreToEdit)
        {
            InitializeComponent();
            _genreToEdit = genreToEdit;
            LoadGenreData();
        }

        private void LoadGenreData()
        {
            DiscountTextBox.Text = _genreToEdit.Discount.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiscountTextBox.Text))
            {
                MessageBox.Show("Введите скидку!");
                return;
            }

            if (!int.TryParse(DiscountTextBox.Text, out int newDiscount) || newDiscount < 0 || newDiscount > 99)
            {
                MessageBox.Show("Скидка должна быть целым числом от 0 до 99%!");
                return;
            }

            try
            {
                using (var context = new ApplicationContext())
                {
                    var genre = context.Genres.Find(_genreToEdit.Id);
                    if (genre != null)
                    {
                        int oldDiscount = genre.Discount;
                        genre.Discount = newDiscount;

                        var books = context.Books.Where(b => b.GenreId == genre.Id).ToList();
                        foreach (var book in books)
                        {
                            double oldFactor = 1.0 - (oldDiscount / 100.0);
                            double originalPrice = book.TotalPrice / oldFactor; 
                            double newFactor = 1.0 - (newDiscount / 100.0);
                            book.TotalPrice = (int)(originalPrice * newFactor);
                        }

                        context.SaveChanges();
                    }
                }

                MessageBox.Show($"Скидка для жанра '{_genreToEdit.GenreName}' обновлена на {newDiscount}%! Цены книг скорректированы.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
