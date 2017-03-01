
namespace BL.Managers
{
    using DAL.Entities;
    using System.Collections.Generic;

    public class SearchManager
    {
        public static List<Book> GetBooksBySimpleSearch(string searchterm)
        {
           
            var books = new List<Book>();
            if (string.IsNullOrEmpty(searchterm))
            {
                return books;
            }

            var allBooks = BooksManager.GetAllBooks();
            foreach (var item in allBooks)
            {
                if(item.Title.ToLower().Contains(searchterm.ToLower()))
                {
                    books.Add(item);
                }
            }

            return books;
        }
    }
}
