
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IBookRepository
    {
        Book GetBookById(int bookId);
        int GetBookCount();
        IEnumerable<Book> GetAllBooks();
        int AddBook(Book book);
        void UpdateBook(Book book);
        void RemoveAuthors(int bookId);
        IEnumerable<Publisher> GetAllBookPublishersGrouped();
        List<Book> GetBooksByDay(DateTime dateTime);
        List<Book> GetBooksByTitlePublisherDomain(string title, int? publisherId, int? domainId);
        IEnumerable<Book> GetBooksByAuthorId(int id);
        IEnumerable<Book> GetBooksByPublisherId(int id);
        IEnumerable<Book> GetAllBooksWithDomain();
        IEnumerable<Book> GetAllBooksByDomainId(int domainId);
        IEnumerable<Book> GetBooksLastAdded(int nr);
    }

    public partial class Repository : IBookRepository
    {
        #region Get

        public Book GetBookById(int bookId)
        {
            Book book = null;

            _dbRead.Execute(
                "BooksGetById",
             new[] { 
                new SqlParameter("@bookId", bookId),
             },
                r => book = new Book()
                {
                    Id = Read<int>(r, "Id"),
                    Title = Read<string>(r, "Title"),
                    PublishYear = Read<int?>(r, "PublishYear"),
                    Volume = Read<string>(r, "Volume"),
                    InternalNr = Read<string>(r, "InternalNr"),
                    NrPages = Read<int>(r, "NrPages"),
                    Publisher = new Publisher
                    {
                        Id = Read<int>(r, "Id_Publisher"),
                        Name = Read<string>(r, "PublisherName"),
                    },
                    BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                    BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                    BookLanguage = (Language)Read<int>(r, "Id_Language"),
                    BookSubject = new BookSubject
                    {
                        Id = Read<int>(r, "Id_BookSubject"),
                        Name = Read<string>(r, "SubjectName"),
                    },
                    BookDomain = new BookDomain
                    {
                        Id = Read<int>(r, "Id_BookDomain"),
                        Name = Read<string>(r, "DomainName"),
                    }
                });

            return book;
        }

        public int GetBookCount()
        {
            int user = 0;

            _dbRead.Execute(
                "BooksGetCount",
            null,
                r => user = Read<int>(r, "num"));

            return user;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = new List<Book>();
            _dbRead.Execute(
                "BooksGetAllForManage",
            null,
                r => books.Add(new Book()
                {
                    Id = Read<int>(r, "Id"),
                    Title = Read<string>(r, "Title"),
                    PublishYear = Read<int?>(r, "PublishYear"),
                    Volume = Read<string>(r, "Volume"),
                    InternalNr = Read<string>(r, "InternalNr"),
                    NrPages = Read<int>(r, "NrPages"),
                    Publisher = new Publisher{
                        Id =  Read<int>(r, "Id_Publisher"),
                        Name =  Read<string>(r, "PublisherName"),
                    },
                    BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                    BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                    BookLanguage = (Language)Read<int>(r, "Id_Language"),
                    BookStatus = GetBookLoanStatus(Read<int>(r, "Id"))
                }));

            return books;
        }

        public IEnumerable<Book> GetAllBooksWithDomain()
        {
            var books = new List<Book>();
            _dbRead.Execute(
                "BooksGetAllWithDomain",
            null,
                r => books.Add(new Book()
                {
                    Id = Read<int>(r, "Id"),
                    Title = Read<string>(r, "Title"),
                    PublishYear = Read<int?>(r, "PublishYear"),
                    Volume = Read<string>(r, "Volume"),
                    InternalNr = Read<string>(r, "InternalNr"),
                    NrPages = Read<int>(r, "NrPages"),
                    Publisher = new Publisher
                    {
                        Id = Read<int>(r, "Id_Publisher"),
                        Name = Read<string>(r, "PublisherName"),
                    },
                    BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                    BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                    BookLanguage = (Language)Read<int>(r, "Id_Language"),
                    BookStatus = GetBookLoanStatus(Read<int>(r, "Id")),
                    BookDomain = new BookDomain
                    {
                        Id = Read<int>(r, "Id_BookDomain"),
                        Name = Read<string>(r, "DomainName"),
                        CZU = Read<string>(r, "CZU"),
                    }
                }));

            return books;
        }

        public IEnumerable<Book> GetAllBooksByDomainId(int domainId)
        {

            var books = new List<Book>();
            _dbRead.Execute(
                "BooksGetAllByDomainId",
              new[]
            {
                new SqlParameter("@domainId", domainId),
            },
                r => books.Add(new Book()
                {
                    Id = Read<int>(r, "Id"),
                    Title = Read<string>(r, "Title"),
                    PublishYear = Read<int?>(r, "PublishYear"),
                    Volume = Read<string>(r, "Volume"),
                    InternalNr = Read<string>(r, "InternalNr"),
                    NrPages = Read<int>(r, "NrPages"),
                    Publisher = new Publisher
                    {
                        Id = Read<int>(r, "Id_Publisher"),
                        Name = Read<string>(r, "PublisherName"),
                    },
                    BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                    BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                    BookLanguage = (Language)Read<int>(r, "Id_Language"),
                    BookStatus = GetBookLoanStatus(Read<int>(r, "Id")),
                    BookDomain = new BookDomain
                    {
                        Id = domainId,
                        Name = Read<string>(r, "DomainName"),
                        CZU = Read<string>(r, "CZU"),
                    }
                }));

            return books;
        }

        public IEnumerable<Book> GetBooksLastAdded(int nr)
        {

            var books = new List<Book>();
            _dbRead.Execute(
                "BooksGetLast",
             new[]
            {
                new SqlParameter("@nr", nr),
            },
                r => books.Add(new Book()
                {
                    Id = Read<int>(r, "Id"),
                    Title = Read<string>(r, "Title")
                }));

            return books;
        }

        public IEnumerable<Publisher> GetAllBookPublishersGrouped()
        {
            var publishers = new List<Publisher>();
            _dbRead.Execute(
                "BooksPublishersGetGrouped",
            null,
                r => publishers.Add(new Publisher()
                {
                    Id = Read<int>(r, "NrCount"),
                    Name = Read<string>(r, "PublisherName"),
                }));

            return publishers;
        }

        public List<Book> GetBooksByDay(DateTime dateTime)
        {
            var books = new List<Book>();

            _dbRead.Execute(
                "BooksGetAllByDate",
            new[]
            {
                new SqlParameter("@date", dateTime),
            },
               r => books.Add(new Book()
               {
                   Id = Read<int>(r, "Id"),
                   Title = Read<string>(r, "Title"),
                   PublishYear = Read<int?>(r, "PublishYear"),
                   Volume = Read<string>(r, "Volume"),
                   InternalNr = Read<string>(r, "InternalNr"),
                   NrPages = Read<int>(r, "NrPages"),
                   Publisher = new Publisher
                   {
                       Id = Read<int>(r, "Id_Publisher"),
                       Name = Read<string>(r, "PublisherName"),
                   },
                   BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                   BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                   BookLanguage = (Language)Read<int>(r, "Id_Language"),
                   BookStatus = GetBookLoanStatus(Read<int>(r, "Id"))
               }));

            return books;
        }

        public List<Book> GetBooksByTitlePublisherDomain(string title, int? publisherId, int? domainId)
        {
            var books = new List<Book>();

            _dbRead.Execute(
                "BooksGetByTitlePublisherDomain",
            new[]
            {
                new SqlParameter("@title", title),
                new SqlParameter("@publisherId", publisherId),
                new SqlParameter("@domainId", domainId),
            },
               r => books.Add(new Book()
               {
                   Id = Read<int>(r, "Id"),
                   Title = Read<string>(r, "Title"),
                   PublishYear = Read<int?>(r, "PublishYear"),
                   Volume = Read<string>(r, "Volume"),
                   InternalNr = Read<string>(r, "InternalNr"),
                   NrPages = Read<int>(r, "NrPages"),
                   Publisher = new Publisher
                   {
                       Id = Read<int>(r, "Id_Publisher"),
                       Name = Read<string>(r, "PublisherName"),
                   },
                   BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                   BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                   BookLanguage = (Language)Read<int>(r, "Id_Language"),
                   BookStatus = GetBookLoanStatus(Read<int>(r, "Id"))
               }));

            return books;
        }

        public IEnumerable<Book> GetBooksByAuthorId(int id)
        {
            var books = new List<Book>();

            _dbRead.Execute(
                "BooksGetByAuthorId",
            new[]
            {
                new SqlParameter("@id", id),
            },
               r => books.Add(new Book()
               {
                   Id = Read<int>(r, "Id"),
                   Title = Read<string>(r, "Title"),
                   PublishYear = Read<int?>(r, "PublishYear"),
                   Volume = Read<string>(r, "Volume"),
                   InternalNr = Read<string>(r, "InternalNr"),
                   NrPages = Read<int>(r, "NrPages"),
                   Publisher = new Publisher
                   {
                       Id = Read<int>(r, "Id_Publisher"),
                       Name = Read<string>(r, "PublisherName"),
                   },
                   BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                   BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                   BookLanguage = (Language)Read<int>(r, "Id_Language"),
                   BookStatus = GetBookLoanStatus(Read<int>(r, "Id"))
               }));

            return books;
        }

        public IEnumerable<Book> GetBooksByPublisherId(int id)
        {
            var books = new List<Book>();

            _dbRead.Execute(
                "BooksGetByPublisherId",
            new[]
            {
                new SqlParameter("@id", id),
            },
               r => books.Add(new Book()
               {
                   Id = Read<int>(r, "Id"),
                   Title = Read<string>(r, "Title"),
                   PublishYear = Read<int?>(r, "PublishYear"),
                   Volume = Read<string>(r, "Volume"),
                   InternalNr = Read<string>(r, "InternalNr"),
                   NrPages = Read<int>(r, "NrPages"),
                   Publisher = new Publisher
                   {
                       Id = id,
                       Name = Read<string>(r, "PublisherName"),
                   },
                   BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                   BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                   BookLanguage = (Language)Read<int>(r, "Id_Language"),
                   BookStatus = GetBookLoanStatus(Read<int>(r, "Id"))
               }));

            return books;
        }

        #endregion

        #region Add

        public int AddBook(Book book)
        {
            var bookId = 0;

            _dbRead.Execute(
               "BooksAdd",
           new[] { 
                new SqlParameter("@AddedbyId", book.AddedBy.Id), 
                new SqlParameter("@AddedDate", book.AddedDate), 
                new SqlParameter("@BookConditionId", book.BookCondition), 
                new SqlParameter("@BookDomainId", book.BookDomain.Id), 
                new SqlParameter("@BookFormatId", book.BookFormat), 
                new SqlParameter("@BookLanguageId", book.BookLanguage), 
                new SqlParameter("@BookSubjectId", book.BookSubject.Id), 
                new SqlParameter("@InternalNr", book.InternalNr), 
                new SqlParameter("@LibraryId", book.Library.Id), 
                new SqlParameter("@NrPages", book.NrPages), 
                new SqlParameter("@PublisherId", book.Publisher.Id), 
                new SqlParameter("@PublishYear", book.PublishYear), 
                new SqlParameter("@Title", book.Title), 
                new SqlParameter("@Volume", book.Volume), 
            },
                r => bookId = Read<int>(r, "id")
            );

            return bookId;
        }

        #endregion

        #region Update

        public void UpdateBook(Book book)
        {
            _dbRead.Execute(
               "BooksUpdate",
           new[] { 
                new SqlParameter("@Id", book.Id), 
                new SqlParameter("@BookConditionId", book.BookCondition), 
                new SqlParameter("@BookDomainId", book.BookDomain.Id), 
                new SqlParameter("@BookFormatId", book.BookFormat), 
                new SqlParameter("@BookLanguageId", book.BookLanguage), 
                new SqlParameter("@BookSubjectId", book.BookSubject.Id), 
                new SqlParameter("@InternalNr", book.InternalNr), 
                new SqlParameter("@LibraryId", book.Library.Id), 
                new SqlParameter("@NrPages", book.NrPages), 
                new SqlParameter("@PublisherId", book.Publisher.Id), 
                new SqlParameter("@PublishYear", book.PublishYear), 
                new SqlParameter("@Title", book.Title), 
                new SqlParameter("@Volume", book.Volume)
            });
        }

        #endregion

        public void RemoveAuthors(int bookId)
        {
             _dbRead.Execute(
               "BookAuthorsRemoveById",
           new[] { 
                new SqlParameter("@bookId", bookId), 
            });
        }
    }
}
