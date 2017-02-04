﻿
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;

    public static class UsersManager
    {
        public static List<User> GetUsersAll()
        {
            var users = CacheHelper.Instance.GetMyCachedItem(CacheConstants.UsersGetAll) as List<User>;
            if (users == null || users.Count == 0)
            {
                users = Kit.Instance.Users.GetUsersAll() as List<User>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.UsersGetAll, users, MyCachePriority.Default);
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

        public static int GetUserNrAll()
        {
            return Kit.Instance.Users.GetUserCount();
        }

        public static int Add(User user)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.UsersGetAll);

            return Kit.Instance.Users.AddUser(user);
        }

        public static void Update(User user)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.UsersGetAll);

            Kit.Instance.Users.UpdateUser(user);
        }
    }
}
