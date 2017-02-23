
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class Books
    {
        private static IBookRepository _repository;

        static Books()
        {
            _repository = new Repository();
        }

        #region Get

        public int GetBookCount()
        {
            return _repository.GetBookCount();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _repository.GetAllBooks();

        }
        #endregion

        public int AddBook(Book book)
        {
            return _repository.AddBook(book);
        }

        public Book GetBookById(int bookId)
        {
            return _repository.GetBookById(bookId);
        }

        public void UpdateBook(Book book)
        {
            _repository.UpdateBook(book);
        }

        public void RemoveAuthors(int bookId)
        {
            _repository.RemoveAuthors(bookId);
        }

        public IEnumerable<Publisher> GetAllBookPublishersGrouped()
        {
            return _repository.GetAllBookPublishersGrouped();
        }
        
    }
}
