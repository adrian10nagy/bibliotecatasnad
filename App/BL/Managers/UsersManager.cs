
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using DAL.SDK;
    using System;
    using System.Collections.Generic;

    public static class UsersManager
    {
        public static IEnumerable<User> GetUsersAll(int libraryId)
        {
            var usersCacheKey = string.Format(CacheConstants.UsersGetAll, libraryId);
            var users = CacheHelper.Instance.GetMyCachedItem(usersCacheKey) as IEnumerable<User>;

            if (users == null)
            {
                users = Kit.Instance.Users.GetUsersAll(libraryId);
                CacheHelper.Instance.AddToMyCache(usersCacheKey, users, MyCachePriority.Default);
            }

            return users;
        }

        public static User GetUserById(int id)
        {
            User user = null;

            if (id > 0)
            {
                user = Kit.Instance.Users.GetUserById(id);
            }

            return user;
        }

        public static int GetUserNrAll(int libraryId)
        {
            return Kit.Instance.Users.GetUserCount(libraryId);
        }

        public static int Add(User user, int libraryId)
        {
            var usersCacheKey = string.Format(CacheConstants.UsersGetAll, libraryId);
            CacheHelper.Instance.RemoveMyCachedItem(usersCacheKey);

            return Kit.Instance.Users.AddUser(user);
        }

        public static void Update(User user, int libraryId)
        {
            var usersCacheKey = string.Format(CacheConstants.UsersGetAll, libraryId);
            CacheHelper.Instance.RemoveMyCachedItem(usersCacheKey);

            Kit.Instance.Users.UpdateUser(user);
        }

        public static bool UpdateUserPassword(int userId, string passwordOld, string passwordNew, int libraryId)
        {
            bool result = false;
            int resultStatus = 0;

            if (userId != 0 && !string.IsNullOrEmpty(passwordOld) && !string.IsNullOrEmpty(passwordNew))
            {
                var usersCacheKey = string.Format(CacheConstants.UsersGetAll, libraryId);

                CacheHelper.Instance.RemoveMyCachedItem(usersCacheKey);

                resultStatus = Kit.Instance.Users.UpdateUser(userId, passwordOld, passwordNew);
            }

            if (resultStatus == 1)
            {
                result = true;
            }

            return result;
        }

        public static User GetUserForLogin(string userName, string password)
        {
            return Kit.Instance.Users.GetUserForLogin(userName, password);
        }

        public static List<User> GetUsersByDay(DateTime dateTime, int libraryId)
        {
            var users = Kit.Instance.Users.GetUsersByDay(dateTime, libraryId) as List<User>;

            return users;
        }

        public static User GetUserByEmail(string userEmail)
        {
            var users = Kit.Instance.Users.GetUserByEmail(userEmail);

            return users;
        }
    }
}
