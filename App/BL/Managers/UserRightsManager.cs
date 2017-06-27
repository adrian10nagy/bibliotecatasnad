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
    }
}
