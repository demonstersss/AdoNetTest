using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WpfApp5.Classes
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int Pages { get; set; }
        public int GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int OriginalPrice { get; set; }
        public int TotalPrice { get; set; }
        public int? PreviousBookId { get; set; }
        public int? NextBookId { get; set; }
        public int Count { get; set; }
        public bool IsCribbed { get; set; }
        public int TotalMoney { get; set; } = 0;

    }
}
