
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class Publishers
    {
        private static IPublisherRepository _repository;

        static Publishers()
        {
            _repository = new Repository();
        }

        #region Get

        public IEnumerable<Publisher> GetAll()
        {
            return _repository.GetAllPublishers();
        }

        #endregion
    }
}
