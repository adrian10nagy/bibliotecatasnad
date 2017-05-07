
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

        public int GetUserCount(int libraryId)
        {
            return _repository.GetUserCount(libraryId);
        }

        #endregion

        public int AddUser(User user)
        {
            return _repository.InsertUser(user);
        }

        public IEnumerable<User> GetUsersAll(int libraryId)
        {
            return _repository.GetAllUsers(libraryId);
        }

        public void UpdateUser(User user)
        {
            _repository.UpdateUser(user);
        }

        public int UpdateUser(int userId, string passwordOld, string passwordNew)
        {
            return _repository.UpdateUser(userId, passwordOld, passwordNew);
        }

        public User GetUserForLogin(string userName, string password)
        {
            return _repository.GetUserForLogin(userName, password);
        }

        public IEnumerable<User> GetUsersByDay(System.DateTime dateTime, int libraryId)
        {
            return _repository.GetUsersByDay(dateTime, libraryId);
        }

        public User GetUserByEmail(string userEmail)
        {
            return _repository.GetUserByEmail(userEmail);
        }
    }
}
