
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

        public int GetBookCount(int libraryId)
        {
            return _repository.GetBookCount(libraryId);
        }

        public IEnumerable<Book> GetAllBooks(int libraryId)
        {
            return _repository.GetAllBooks(libraryId);
        }

        public IEnumerable<Book> GetAllBooksWithDomain(int libraryId)
        {
            return _repository.GetAllBooksWithDomain(libraryId);
        }

        public IEnumerable<Book> GetAllBooksByDomainId(int domainId, int libraryId)
        {
            return _repository.GetAllBooksByDomainId(domainId, libraryId);
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

        public IEnumerable<Publisher> GetAllBookPublishersGrouped(int libraryId)
        {
            return _repository.GetAllBookPublishersGrouped(libraryId);
        }

        public List<Book> GetBooksByDay(DateTime dateTime, int libraryId)
        {
            return _repository.GetBooksByDay(dateTime, libraryId);
        }

        public List<Book> GetBooksByTitlePublisherDomain(string title, int? publisherId, int? domainId, int libraryId)
        {
            return _repository.GetBooksByTitlePublisherDomain(title, publisherId, domainId, libraryId);
        }

        public IEnumerable<Book> GetBooksByAuthorId(int id, int libraryId)
        {
            return _repository.GetBooksByAuthorId(id, libraryId);
        }

        public IEnumerable<Book> GetBooksByPublisherId(int id, int libraryId)
        {
            return _repository.GetBooksByPublisherId(id, libraryId);
        }

        public IEnumerable<Book> GetBooksLastAdded(int nr, int libraryId)
        {
            return _repository.GetBooksLastAdded(nr, libraryId);
        }

        public Book GetBookByISBN(string isbn, int libraryId)
        {
            return _repository.GetBookByISBN(isbn, libraryId);
        }
    }
}
