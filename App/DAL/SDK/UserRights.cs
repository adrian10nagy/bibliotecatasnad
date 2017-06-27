
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System;
    using System.Collections.Generic;

    public class UserRights
    {
        private static IUserRightRepository _repository;

        static UserRights()
        {
            _repository = new Repository();
        }

        public void UpdateRight(UserRight userRight)
        {
            _repository.UpdateRight(userRight);
        }

        public IEnumerable<UserRight> UsersWithRights()
        {
            return _repository.GetAllUsersWithRights();
        }

        public bool UserHasAccess(UserRight userRight)
        {
            return _repository.UserHasAccess(userRight);
        }
    }
}
