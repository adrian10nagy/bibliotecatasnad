
namespace DAL.Repositories
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IISBNsRepository
    {
        IEnumerable<ISBN> GetAllByBookId(int bookId);
        int AddISBN(int bookId, string value);
        void RemoveISBNsByBookId(int bookId);
    }

    public partial class Repository : IISBNsRepository
    {
        #region Get

        public IEnumerable<ISBN> GetAllByBookId(int bookId)
        {
            List<ISBN> isbns = new List<ISBN>();

            _dbRead.Execute(
                "ISBNGetByBookId",
            new[] 
            { 
                new SqlParameter("@bookId", bookId),
            }
            ,
                r => isbns.Add(new ISBN()
                {
                    Id = Read<int>(r, "Id"),
                    Value = Read<string>(r, "Value"),
                }));

            return isbns;
        }

        #endregion

        public int AddISBN(int bookId, string value)
        {
            int bookDomainNr = 0;

            _dbRead.Execute(
               "ISBNAdd",
           new[] { 
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@value", value)
            },
            r => bookDomainNr = Read<int>(r, "Id"));

            return bookDomainNr;
        }

        public void RemoveISBNsByBookId(int bookId)
        {
            _dbRead.Execute(
              "ISBNsRemoveByBookId",
          new[] { 
                new SqlParameter("@bookId", bookId), 
            });
        }

    }
}
