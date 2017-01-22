
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class BookDomains
    {
        private static IBookDomainsRepository _repository;

        static BookDomains()
        {
            _repository = new Repository();
        }

        #region Get

        public IEnumerable<BookDomain> GetAllBookDomains()
        {
            return _repository.GetAllBookDomains();
        }

        #endregion
    }
}

