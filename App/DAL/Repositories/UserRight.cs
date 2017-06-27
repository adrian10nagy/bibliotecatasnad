using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IUserRightRepository
    {
        void UpdateRight(UserRight userRight);
        IEnumerable<UserRight> GetAllUsersWithRights();
        bool UserHasAccess(UserRight userRight);
    }

    public partial class Repository : IUserRightRepository
    {
        public void UpdateRight(UserRight userRight)
        {
            _dbRead.ExecuteNonQuery(
                "UserRightsUpdate",
            new[] 
            { 
                new SqlParameter("@userName", userRight.User.Username),
                new SqlParameter("@hasRight", userRight.hasRight),
                new SqlParameter("@functionalityId", (int)userRight.Functionality),
            });
        }

        public IEnumerable<UserRight> GetAllUsersWithRights()
        {
            var userRight = new List<UserRight>();
            _dbRead.Execute(
                "UserRightsGetAll",
                null,
                r => userRight.Add(new UserRight()
                {
                    Functionality = (Functionality)Read<int>(r, "Id_functionality"),
                    hasRight = true,
                    User = new User
                    {
                        Id = Read<int>(r, "Id_user")
                    }
                }));

            return userRight;
        }

        public bool UserHasAccess(UserRight userRight)
        {
            var hasAccess = false;

            _dbRead.Execute(
                "UserRightsHasAccess",
                new[]
                {
                    new SqlParameter("@functionalityId", (int)userRight.Functionality),
                    new SqlParameter("@userId", userRight.User.Id),
                },
                r => hasAccess = (Read<int>(r, "hasValue") == 1) ? true : false);

            return hasAccess;
        }
    }
}
