
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IUserRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers(int libraryId);
        IEnumerable<User> GetUsersByDay(DateTime dateTime, int libraryId);
        int InsertUser(User user);
        User GetUserByEmail(string email);
        int GetUserCount(int libraryId);
        void UpdateUser(User user);
        User GetUserForLogin(string userName, string password);
        int UpdateUser(int userId, string passwordOld, string passwordNew);
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
                    Username = Read<string>(r, "UserName"),
                    HomeAddress = Read<string>(r, "HomeAddress"),
                    Birthdate = Read<DateTime>(r, "Birthdate"),
                    Phone = Read<string>(r, "Phone"),
                    Email = Read<string>(r, "Email"),
                    FacebookAddress = Read<string>(r, "FacebookAddress"),
                    Gender = Read<Gender>(r, "Gender"),
                    Locality = new Locality()
                    {
                        Id = Read<int>(r, "Id_Locality"),
                    },
                    UserType = Read<UserType>(r, "Id_UserType"),
                    Nationality = Read<Nationality>(r, "Id_Nationality")
                });

            return user;
        }

        public int GetUserCount(int libraryId)
        {
            int user = 0;

            _dbRead.Execute(
                "UsersGetCount",
            new[]
            {
                new SqlParameter("@libraryId", libraryId)
            },
                r => user = Read<int>(r, "num"));

            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            _dbRead.Execute(
                "UsersGetByEmail",
            new[] { 
                new SqlParameter("@email", email), 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Username = Read<string>(r, "UserName"),
                    HomeAddress = Read<string>(r, "HomeAddress"),
                    Birthdate = Read<DateTime>(r, "Birthdate"),
                    Phone = Read<string>(r, "Phone"),
                    Email = email,
                    FacebookAddress = Read<string>(r, "FacebookAddress"),
                    Gender = Read<Gender>(r, "Gender"),
                    Locality = new Locality()
                    {
                        Id = Read<int>(r, "Id_Locality"),
                    },
                    UserType = Read<UserType>(r, "Id_UserType"),
                    Nationality = Read<Nationality>(r, "Id_Nationality")
                });

            return user;
        }

        public User GetUserForLogin(string userName, string password)
        {
            User user = null;

            _dbRead.Execute(
                "UserGetForLogin",
            new[] { 
                new SqlParameter("@userName", userName), 
                new SqlParameter("@password", password), 
            },
                r => user = new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Email = Read<string>(r, "Email"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    UserType = Read<UserType>(r, "Id_usertype"),
                    Locality = new Locality()
                    {
                        Id = Read<int>(r, "Id_Locality"),
                        Name = Read<string>(r, "Locality"),
                    },
                    Library = new Library()
                    {
                        Id = Read<int>(r, "LibraryId"),
                        Name = Read<string>(r, "LibraryName"),
                    }
                });

            return user;
        }

        public IEnumerable<User> GetAllUsers(int libraryId)
        {
            var users = new List<User>();
            _dbRead.Execute(
                "UserGetAll",
            new[]
            {
                new SqlParameter("@libraryId", libraryId)
            },
                r => users.Add(new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Username = Read<string>(r, "UserName"),
                    HomeAddress = Read<string>(r, "HomeAddress"),
                    Birthdate = Read<DateTime>(r, "Birthdate"),
                    Phone = Read<string>(r, "Phone"),
                    Email = Read<string>(r, "Email"),
                    FacebookAddress = Read<string>(r, "FacebookAddress"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    Gender = Read<Gender>(r, "Gender"),
                    Locality = new Locality()
                    {
                        Id = Read<int>(r, "Id_Locality"),
                        Name = Read<string>(r, "Locality"),
                    },
                    UserType = Read<UserType>(r, "Id_UserType"),
                    Nationality = Read<Nationality>(r, "Id_Nationality")
                }));

            return users;
        }

        public IEnumerable<User> GetUsersByDay(DateTime dateTime, int libraryId)
        {
            var users = new List<User>();
            _dbRead.Execute(
                "UsersGetAllByDate",
            new[]
            {
                new SqlParameter("@date", dateTime),
                new SqlParameter("@libraryId", libraryId),
            },
                r => users.Add(new User()
                {
                    Id = Read<int>(r, "Id"),
                    FirstName = Read<string>(r, "FirstName"),
                    LastName = Read<string>(r, "LastName"),
                    Username = Read<string>(r, "UserName"),
                    HomeAddress = Read<string>(r, "HomeAddress"),
                    Birthdate = Read<DateTime>(r, "Birthdate"),
                    Phone = Read<string>(r, "Phone"),
                    Email = Read<string>(r, "Email"),
                    FacebookAddress = Read<string>(r, "FacebookAddress"),
                    JoinDate = Read<DateTime>(r, "JoinDate"),
                    Flags = Read<UserFlag>(r, "Flags"),
                    Gender = Read<Gender>(r, "Gender"),
                    Locality = new Locality()
                    {
                        Id = Read<int>(r, "Id_Locality"),
                        Name = Read<string>(r, "Locality"),
                    },
                    UserType = Read<UserType>(r, "Id_UserType"),
                    Nationality = Read<Nationality>(r, "Id_Nationality")
                }));

            return users;
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
                new SqlParameter("@LibraryId", user.Library.Id), 
                new SqlParameter("@UserType", user.UserType), 
                new SqlParameter("@NationalityId", user.Nationality), 
                new SqlParameter("@Password", user.Password), 
            },
                r =>
                userId = Read<int>(r, "Id")
            );

            return userId;
        }

        #endregion

        #region Update

        public void UpdateUser(User user)
        {
            _dbRead.ExecuteNonQuery(
               "UsersUpdate",
           new[] { 
                new SqlParameter("@Id", user.Id), 
                new SqlParameter("@FirstName", user.FirstName), 
                new SqlParameter("@LastName", user.LastName), 
                new SqlParameter("@Username", user.Username), 
                new SqlParameter("@HomeAddress", user.HomeAddress), 
                new SqlParameter("@Birthdate", user.Birthdate), 
                new SqlParameter("@Phone", user.Phone), 
                new SqlParameter("@Email", user.Email), 
                new SqlParameter("@FacebookAddress", user.FacebookAddress), 
                new SqlParameter("@Gender", user.Gender), 
                new SqlParameter("@LocalityId", user.Locality.Id), 
                new SqlParameter("@LibraryId", user.Library.Id), 
                new SqlParameter("@UserType", user.UserType),
                new SqlParameter("@NationalityId", user.Library.Id)
               });
        }

        public int UpdateUser(int userId, string passwordOld, string passwordNew)
        {
            int result = 0;

            _dbRead.Execute(
               "UsersUpdatePassword",
           new[] { 
                new SqlParameter("@id", userId), 
                new SqlParameter("@passwordOld", passwordOld),
                new SqlParameter("@passwordNew", passwordNew)
               },
                r =>
                result = Read<int>(r, "result")
                );

            return result;
        }

        #endregion
    }
}
