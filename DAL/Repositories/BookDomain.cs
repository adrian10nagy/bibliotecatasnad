namespace DAL.Repositories
{
    using DAL.Entities;
    using System.Collections.Generic;

    public interface IBookDomainsRepository
    {
        IEnumerable<BookDomain> GetAllBookDomains();
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
                }));

            return publishers;
        }

        #endregion
    }
}