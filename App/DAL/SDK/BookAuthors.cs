
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class BookAuthors
    {
        private static IBookAuthorsRepository _repository;

        static BookAuthors()
        {
            _repository = new Repository();
        }

        #region Get

        public IEnumerable<Author> GetAllBookAuthorsByBookId(int id)
        {
            return _repository.GetAllBookAuthorsByBookId(id);
        }

        #endregion

        public void AddBookAuthor(Author item, int bookId)
        {
            _repository.AddBookAuthor(item, bookId);
        }
    }
}
