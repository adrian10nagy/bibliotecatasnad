
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections;
    using System.Collections.Generic;

    public class Authors
    {
        private static IAuthorRepository _repository;

        static Authors()
        {
            _repository = new Repository();
        }

        #region Get

        public IEnumerable<Author> GetAllAuthors()
        {
            return _repository.GetAuthorsAll();
        }

        #endregion
    }
}
