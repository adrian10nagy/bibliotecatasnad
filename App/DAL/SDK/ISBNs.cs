
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

    public class ISBNs
    {
         private static IISBNsRepository _repository;

        static ISBNs()
        {
            _repository = new Repository();
        }

        #region Get

        public IEnumerable<ISBN> GetAllByBookId(int bookId)
        {
            return _repository.GetAllByBookId(bookId);
        }

        #endregion

        public int AddISBN(int bookId, string value)
        {
            return _repository.AddISBN(bookId, value);
        }

        public void RemoveISBNsByBookId(int bookId)
        {
            _repository.RemoveISBNsByBookId(bookId);
        }
    }
    
}
        
