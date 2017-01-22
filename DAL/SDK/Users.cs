
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;

    public class Users
    {
        private static IUserRepository _repository;

        static Users()
        {
            _repository = new Repository();
        }

        #region Get

        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public int GetUserCount()
        {
            return _repository.GetUserCount();
        }

        #endregion
    }
}
