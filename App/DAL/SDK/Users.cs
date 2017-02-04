
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Collections.Generic;

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

        public int AddUser(User user)
        {
            return _repository.InsertUser(user);
        }

        public IEnumerable<User> GetUsersAll()
        {
            return _repository.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            _repository.UpdateUser(user);
        }
    }
}
