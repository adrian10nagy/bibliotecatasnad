
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IUserRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserByEmailPass(string email, string pass);
        int InsertUser(User user);
        User GetUserByEmail(string email);
        int GetUserCount();
    }

    public partial class Repository : IUserRepository
    {
        #region Get

        public User GetUserById(int id)
        {
            User user = null;

            _dbRead.Execute(
                "UsersGetById",
            new[] { 
                new SqlParameter("@Id", id), 
            },
                r => user = new User()
                {
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Username = Read<string>(r, "Username"),
                });

            return user;
        }

        public int GetUserCount()
        {
            int user = 0;

            _dbRead.Execute(
                "UsersGetCount",
            null,
                r => user = Read<int>(r, "num"));

            return user;
        }


        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();

            User user = null;

            _dbRead.Execute(
                "UserGetByEmail",
            new[] { 
                new SqlParameter("@Email", email), 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                });

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();

            var users = new List<User>();
            _dbRead.Execute(
                "UserGetAll",
            null,
                r => users.Add(new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                }));

            return users;
        }

        public User GetUserByEmailPass(string email, string pass)
        {
            throw new NotImplementedException();
            var user = new User();

            _dbRead.Execute(
                "UserGetByNamePass",
            new[] { 
                new SqlParameter("@Password", pass), 
                new SqlParameter("@Email", email) 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    UserType = Read<UserType>(r, "Id_type"),
                });

            return user;
        }

        #endregion

        #region Insert

        public int InsertUser(User user)
        {
            int userId = 0;
            _dbRead.Execute(
                "UsersAdd",
            new[] { 
                new SqlParameter("@FirstName", user.FirstName), 
                new SqlParameter("@LastName", user.LastName), 
                new SqlParameter("@Username", user.Username), 
                new SqlParameter("@HomeAddress", user.HomeAddress), 
                new SqlParameter("@Birthdate", user.Birthdate), 
                new SqlParameter("@Phone", user.Phone), 
                new SqlParameter("@Email", user.Email), 
                new SqlParameter("@FacebookAddress", user.FacebookAddress), 
                new SqlParameter("@JoinDate", user.JoinDate), 
                new SqlParameter("@Flags", user.Flags), 
                new SqlParameter("@Gender", user.Gender), 
                new SqlParameter("@LocalityId", user.Locality.Id), 
                new SqlParameter("@UserType", user.UserType), 
            },
                r =>
                userId = Read<int>(r, "Id")
            );

            return userId;
        }

        #endregion
    }
}
