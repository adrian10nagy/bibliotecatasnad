
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
    }
}
