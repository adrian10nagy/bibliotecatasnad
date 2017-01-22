
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;

    public class Books
    {
        private static IBookRepository _repository;

        static Books()
        {
            _repository = new Repository();
        }

        #region Get

        public int GetBookCount()
        {
            return _repository.GetBookCount();
        }

        #endregion

        public void AddBook(Book book)
        {
            _repository.AddBook(book);
        }
    }
}
