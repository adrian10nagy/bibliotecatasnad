
namespace Public.Models
{
    using DAL.Entities;
    using System.Collections.Generic;
    
    public class MainPageModel
    {
        public int BookNumber { get; set; }
        public List<Book> LastAddedBooks { get; set; }
    }
}