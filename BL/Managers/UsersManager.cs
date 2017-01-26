
namespace BL.Managers
{
    using DAL.Entities;
    using DAL.SDK;

    public static class UsersManager
    {
        public static User GetUserById(int id)
        {
            return Kit.Instance.Users.GetUserById(id);
        }

        public static int GetUserNrAll()
        {
            return Kit.Instance.Users.GetUserCount();
        }

        public static int Add(User user)
        {
            return Kit.Instance.Users.AddUser(user);
        }
    }
}
