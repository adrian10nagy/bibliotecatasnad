
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class Libraries
    {
        private static ILibraryRepository _repository;

        static Libraries()
        {
            _repository = new Repository();
        }

        public Library GetLibraryById(int id)
        {
            return _repository.GetLibraryById(id);
        }

        public void Add(Library library)
        {
            _repository.Add(library);
        }
    }
}
