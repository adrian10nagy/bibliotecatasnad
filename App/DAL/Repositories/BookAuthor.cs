
namespace DAL.Repositories
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IBookAuthorsRepository
    {
        void AddBookAuthor(Author author, int bookId);
        IEnumerable<Author> GetAllBookAuthorsByBookId(int id);
    }

    public partial class Repository : IBookAuthorsRepository
    {
        #region Get

        public IEnumerable<Author> GetAllBookAuthorsByBookId(int id)
        {
            List<Author> publishers = new List<Author>();

            _dbRead.Execute(
                "BookAuthorsGetByBookId",
             new[] { 
                new SqlParameter("@bookId", id),
             },
                r => publishers.Add(new Author()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                    BookAthorType = (BookAthorType)Read<int>(r, "Id_BookAuthorType"),
                }));

            return publishers;
        }

        #endregion

        public void AddBookAuthor(Author author, int bookId)
        {
            _dbRead.Execute(
               "BookAuthorsAdd",
           new[] { 
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@authorId", author.Id),
                new SqlParameter("@BookAthorType", author.BookAthorType),
            });
        }
    }
}
