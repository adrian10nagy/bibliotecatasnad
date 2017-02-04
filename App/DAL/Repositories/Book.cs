
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IBookRepository
    {
        User GetBookById(int id);
        int GetBookCount();
        IEnumerable<Book> GetAllBooks();
        int AddBook(Book book);
    }

    public partial class Repository : IBookRepository
    {
        #region Get

        public User GetBookById(int id)
        {
            throw new NotImplementedException();
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
                    PublishYear = Read<int>(r, "PublishYear"),
                    Volume = Read<string>(r, "Volume"),
                    ISBN = Read<string>(r, "ISBN"),
                    InternalNr = Read<string>(r, "InternalNr"),
                    NrPages = Read<int>(r, "NrPages"),
                    Publisher = new Publisher{
                        Id =  Read<int>(r, "Id_Publisher"),
                        Name =  Read<string>(r, "PublisherName"),
                    },
                    BookCondition = (BookCondition)Read<int>(r, "Id_BookCondition"),
                    BookFormat = (BookFormat)Read<int>(r, "Id_BookFormat"),
                    BookLanguage = (Language)Read<int>(r, "Id_Language"),
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
                new SqlParameter("@Isbn", book.ISBN), 
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
    }
}
