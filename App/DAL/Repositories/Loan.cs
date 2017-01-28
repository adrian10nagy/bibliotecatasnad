
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ILoanRepository
    {
        User GetLoanById(int id);
        int GetLoanCount();
    }

    public partial class Repository : ILoanRepository
    {
        #region Get

        public User GetLoanById(int id)
        {
            throw new NotImplementedException();

            User user = null;

            _dbRead.Execute(
                "LoansGetById",
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

        public int GetLoanCount()
        {
            int user = 0;

            _dbRead.Execute(
                "LoansGetCount",
            null,
                r => user = Read<int>(r, "num"));

            return user;
        }

        #endregion
    }
}
