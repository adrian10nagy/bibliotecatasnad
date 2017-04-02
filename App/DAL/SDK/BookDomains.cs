
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

        public BookDomain GetBookDomainById(int id)
        {
            return _repository.GetBookDomainById(id);
        }

        #endregion

        public int AddBookDomain(string name)
        {
            return _repository.AddBookDomain(name);
        }
    }
}

