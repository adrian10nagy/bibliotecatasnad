
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System;
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

        public IEnumerable<Book> GetAllBooksWithDomain()
        {
            return _repository.GetAllBooksWithDomain();
        }

        public IEnumerable<Book> GetAllBooksByDomainId(int domainId)
        {
            return _repository.GetAllBooksByDomainId(domainId);
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

        public List<Book> GetBooksByDay(DateTime dateTime)
        {
            return _repository.GetBooksByDay(dateTime);
        }

        public List<Book> GetBooksByTitlePublisherDomain(string title, int? publisherId, int? domainId)
        {
            return _repository.GetBooksByTitlePublisherDomain(title, publisherId, domainId);
        }

        public IEnumerable<Book> GetBooksByAuthorId(int id)
        {
            return _repository.GetBooksByAuthorId(id);
        }

        public IEnumerable<Book> GetBooksByPublisherId(int id)
        {
            return _repository.GetBooksByPublisherId(id);
        }

        public IEnumerable<Book> GetBooksLastAdded(int nr)
        {
            return _repository.GetBooksLastAdded(nr);
        }
    }
}
