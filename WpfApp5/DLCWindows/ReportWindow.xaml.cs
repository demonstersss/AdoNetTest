using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class ReportWindow : Window
    {
        private readonly string _period;

        public ReportWindow(string period)
        {
            InitializeComponent();
            _period = period;
            LoadReportData();
        }

        private void LoadReportData()
        {
            DateTime startDate = GetStartDate();
            using (var context = new ApplicationContext())
            {
                var periodPurchases = context.Purchases.Where(p => p.PurchaseDate >= startDate).ToList();

                var topBooks = periodPurchases
                    .GroupBy(p => p.BookId)
                    .Select(g => new
                    {
                        BookId = g.Key,
                        Sales = g.Count()
                    })
                    .OrderByDescending(x => x.Sales)
                    .Take(10)
                    .ToList();

                var topBooksList = new ObservableCollection<ReportItem>();
                foreach (var item in topBooks)
                {
                    var book = context.Books.FirstOrDefault(b => b.Id == item.BookId);
                    if (book != null)
                    {
                        topBooksList.Add(new ReportItem { DisplayText = $"{book.Title} (Продажи: {item.Sales})" });
                    }
                }
                TopBooksDataGrid.ItemsSource = topBooksList;

                var topAuthors = periodPurchases
                    .GroupBy(p => context.Books.First(b => b.Id == p.BookId).AuthorId)
                    .Select(g => new
                    {
                        AuthorId = g.Key,
                        Sales = g.Count()
                    })
                    .OrderByDescending(x => x.Sales)
                    .Take(10)
                    .ToList();

                var topAuthorsList = new ObservableCollection<ReportItem>();
                foreach (var item in topAuthors)
                {
                    var author = context.Authors.FirstOrDefault(a => a.Id == item.AuthorId);
                    if (author != null)
                    {
                        topAuthorsList.Add(new ReportItem { DisplayText = $"{author.FirstName ?? "Unknown"} (Продажи: {item.Sales})" });
                    }
                }
                TopAuthorsDataGrid.ItemsSource = topAuthorsList;

                var topGenres = periodPurchases
                    .GroupBy(p => context.Books.First(b => b.Id == p.BookId).GenreId)
                    .Select(g => new
                    {
                        GenreId = g.Key,
                        Sales = g.Count()
                    })
                    .OrderByDescending(x => x.Sales)
                    .Take(10)
                    .ToList();

                var topGenresList = new ObservableCollection<ReportItem>();
                foreach (var item in topGenres)
                {
                    var genre = context.Genres.FirstOrDefault(g => g.Id == item.GenreId);
                    if (genre != null)
                    {
                        topGenresList.Add(new ReportItem { DisplayText = $"{genre.GenreName ?? "Unknown"} (Продажи: {item.Sales})" });
                    }
                }
                TopGenresDataGrid.ItemsSource = topGenresList;

                var booksById = context.Books.OrderByDescending(b => b.Id).Take(20).ToList(); // Топ 20 новых

                var booksByIdList = new ObservableCollection<ReportItem>();
                foreach (var book in booksById)
                {
                    booksByIdList.Add(new ReportItem { DisplayText = $"{book.Title} (Id: {book.Id})" });
                }
                BooksByIdDataGrid.ItemsSource = booksByIdList;
            }

            TopBooksLabel.Content += $" за {_period}";
            TopAuthorsLabel.Content += $" за {_period}";
            TopGenresLabel.Content += $" за {_period}";
        }

        private DateTime GetStartDate()
        {
            var now = DateTime.Now;
            return _period switch
            {
                "Day" => now.Date,
                "Week" => now.AddDays(-7),
                "Month" => now.AddMonths(-1),
                "Year" => now.AddYears(-1),
                _ => now.Date
            };
        }
    }
    public class ReportItem : INotifyPropertyChanged
    {
        public string DisplayText { get; set; } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
