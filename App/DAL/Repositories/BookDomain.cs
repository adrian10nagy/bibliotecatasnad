namespace DAL.Repositories
{
    using DAL.Entities;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IBookDomainsRepository
    {
        IEnumerable<BookDomain> GetAllBookDomains();
        BookDomain GetBookDomainById(int id);
        int AddBookDomain(string name);
    }

    public partial class Repository : IBookDomainsRepository
    {
        #region Get

        public IEnumerable<BookDomain> GetAllBookDomains()
        {
            List<BookDomain> publishers = new List<BookDomain>();

            _dbRead.Execute(
                "BookDomainsGetAll",
            null,
                r => publishers.Add(new BookDomain()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                    CZU = Read<string>(r, "CZU"),
                }));

            return publishers;
        }

        public BookDomain GetBookDomainById(int id)
        {
            BookDomain domain = null;

            _dbRead.Execute(
                "BookDomainsGetById",
            new[]
            {
                new SqlParameter("@id", id),
                
            },
                r => domain = new BookDomain()
                {
                    Id = id,
                    Name = Read<string>(r, "Name"),
                    CZU = Read<string>(r, "CZU"),
                });

            return domain;
        }

        #endregion

        public int AddBookDomain(string author)
        {
            int bookDomainNr = 0;

            _dbRead.Execute(
               "BookDomainsAdd",
           new[] { 
                new SqlParameter("@name", author),
            },
            r => bookDomainNr = Read<int>(r, "Id"));

            return bookDomainNr;
        }
    }
}