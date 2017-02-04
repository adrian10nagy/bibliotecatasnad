
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;

    public static class BooksManager
    {
        public static int GetBooksNrAll()
        {
            return Kit.Instance.Books.GetBookCount();
        }

       
        public static IEnumerable<Book> GetAllBooks()
        {
            var books = CacheHelper.Instance.GetMyCachedItem(CacheConstants.BooksGetAll) as List<Book>;
            if (books == null || books.Count == 0)
            {
                books = Kit.Instance.Books.GetAllBooks() as List<Book>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.BooksGetAll, books, MyCachePriority.Default);
            }

            return books;
        }

        public static IEnumerable<Author> GetBookAuthorsByBookId(int bookId)
        {
            return Kit.Instance.BookAuthors.GetAllBookAuthorsByBookId(bookId);
        }

        public static void AddBook(DAL.Entities.Book book)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BooksGetAll);

            book.Id = Kit.Instance.Books.AddBook(book);
            AddBookAuthor(book);
        }
        
        public static void AddBookAuthor(Book book)
        {
            foreach (var author in book.Authors)
            {
                author.BookAthorType = BookAthorType.Author;
                Kit.Instance.BookAuthors.AddBookAuthor(author, book.Id);
            }
        }
    }
}
