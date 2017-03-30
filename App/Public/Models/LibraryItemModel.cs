
namespace Public.Models
{
    using DAL.Entities;
    using System.Collections.Generic;

    public class LibraryItemModel
    {
        public BookDomain Domain { get; set; }
        public List<Book> Books { get; set; }
    }
}