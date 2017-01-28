
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
        void AddBook(Book book);
    }

    public partial class Repository : IBookRepository
    {
        #region Get

        public User GetBookById(int id)
        {
            throw new NotImplementedException();

            User user = null;

            _dbRead.Execute(
                "BooksGetById",
            new[] { 
                new SqlParameter("@Id", id), 
            },
                r => user = new User()
                {
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Username = Read<string>(r, "Username"),
                });

            return user;
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

        #endregion

        #region Add

        public void AddBook(Book book)
        {
            _dbRead.ExecuteNonQuery(
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
            });
        }

        #endregion
    }
}
