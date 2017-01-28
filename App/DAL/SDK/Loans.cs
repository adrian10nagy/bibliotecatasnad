
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;

    public class Loans
    {
        private static ILoanRepository _repository;

        static Loans()
        {
            _repository = new Repository();
        }

        #region Get

        public int GetLoanCount()
        {
            return _repository.GetLoanCount();
        }

        #endregion
    }
}
