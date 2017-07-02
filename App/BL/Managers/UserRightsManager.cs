using DAL.Entities;
using DAL.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public static class UserRightsManager
    {
        public static void UpdateRight(UserRight userRight)
        {
            Kit.Instance.UserRights.UpdateRight(userRight);
        }

        public static IEnumerable<UserRight> UsersWithRights()
        {
            return Kit.Instance.UserRights.UsersWithRights();
        }

        public static bool UserHasAccess(UserRight userRight)
        {
            return Kit.Instance.UserRights.UserHasAccess(userRight);
        }

        public static bool CanLogin(int userId)
        {
            return UserRightsManager.UserHasAccess(
                new UserRight
                {
                    User = new User
                    {
                        Id = userId
                    },
                    Functionality = Functionality.AdminLogin
                });
        }

        public static bool CanAccessBooksModule(int userId)
        {
            return UserRightsManager.UserHasAccess(
                new UserRight
                {
                    User = new User
                    {
                        Id = userId
                    },
                    Functionality = Functionality.ManageBooks
                });
        }

        public static bool CanAccesUsersModule(int userId)
        {
            return UserRightsManager.UserHasAccess(
                new UserRight
                {
                    User = new User
                    {
                        Id = userId
                    },
                    Functionality = Functionality.ManageUsers
                });
        }

        public static bool CanAccessRaportsModule(int userId)
        {
            return UserRightsManager.UserHasAccess(
                new UserRight
                {
                    User = new User
                    {
                        Id = userId
                    },
                    Functionality = Functionality.Raports
                });
        }
    }
}
