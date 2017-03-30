
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ILoanRepository
    {
        int GetLoanCount();
        void AddLoan(int bookId, int userId, DateTime loanDate);
        IEnumerable<Loan> GetAllActiveLoans();
        IEnumerable<Loan> GetAllFinishedLoans();
        void ReturnBook(int loanId, DateTime dateTime);
        void Remove(int loanId);
        List<Loan> GetLoansByDay(DateTime datetime);
        IEnumerable<Loan> GetLoansByUserId(int userId);
        IEnumerable<Loan> GetLoansByBookId(int bookId);
        LoanReservedBookStatus GetBookLoanStatus(int bookId);
    }

    public partial class Repository : ILoanRepository
    {
        #region Get

        public int GetLoanCount()
        {
            int user = 0;

            _dbRead.Execute(
                "LoansGetCount",
            null,
                r => user = Read<int>(r, "num"));

            return user;
        }

        public IEnumerable<Loan> GetAllActiveLoans()
        {
            var loans = new List<Loan>();

            _dbRead.Execute(
                "LoansGetAllActive",
            null,
                r => loans.Add(new Loan()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User 
                    {
                        Id = Read<int>(r, "UserId"),
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },
                    Books = new List<Book>()
                    {
                        new Book
                        {
                            Id = Read<int>(r, "BookId"),
                            Title = Read<string>(r, "Title"),
                            InternalNr = Read<string>(r, "InternalNr"),
                        }
                    },
                    LoanDate = Read<DateTime>(r, "loandate")
                }));

            return loans;
        }

        public IEnumerable<Loan> GetAllFinishedLoans()
        {
            var loans = new List<Loan>();

            _dbRead.Execute(
                "LoansGetAllFinished",
            null,
                r => loans.Add(new Loan()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User
                    {
                        Id = Read<int>(r, "UserId"),
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },
                    Books = new List<Book>()
                    {
                        new Book
                        {
                            Id = Read<int>(r, "BookId"),
                            Title = Read<string>(r, "Title"),
                            InternalNr = Read<string>(r, "InternalNr"),
                        }
                    },
                    LoanDate = Read<DateTime>(r, "loandate"),
                    ReturnedDate = Read<DateTime>(r, "returnedDate")
                }));

            return loans; 
        }

        public List<Loan> GetLoansByDay(DateTime datetime)
        {
            var loans = new List<Loan>();

            _dbRead.Execute(
                "LoansGetAllByDate",
            new[]
            {
                new SqlParameter("@date", datetime),
            },
                r => loans.Add(new Loan()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User
                    {
                        Id = Read<int>(r, "UserId"),
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },
                    Books = new List<Book>()
                    {
                        new Book
                        {
                            Id = Read<int>(r, "BookId"),
                            Title = Read<string>(r, "Title"),
                            InternalNr = Read<string>(r, "InternalNr"),
                        }
                    },
                    LoanDate = Read<DateTime>(r, "loandate"),
                    ReturnedDate = Read<Nullable<DateTime>>(r, "returnedDate")
                }));

            return loans;
        }

        public IEnumerable<Loan> GetLoansByUserId(int userId)
        {
            var loans = new List<Loan>();

            _dbRead.Execute(
                "LoansGetAllByUserId",
            new[]
            {
                new SqlParameter("@userId", userId),
            },
                r => loans.Add(new Loan()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User
                    {
                        Id = userId,
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },
                    Books = new List<Book>()
                    {
                        new Book
                        {
                            Id = Read<int>(r, "BookId"),
                            Title = Read<string>(r, "Title"),
                            InternalNr = Read<string>(r, "InternalNr"),
                        }
                    },
                    LoanDate = Read<DateTime>(r, "loandate"),
                    ReturnedDate = Read <Nullable<DateTime>>(r, "returnedDate")
                }));

            return loans;
        }

        public IEnumerable<Loan> GetLoansByBookId(int bookId)
        {
            var loans = new List<Loan>();

            _dbRead.Execute(
                "LoansGetAllByBookId",
            new[]
            {
                new SqlParameter("@bookId", bookId),
            },
                r => loans.Add(new Loan()
                {
                    Id = Read<int>(r, "Id"),
                    User = new User
                    {
                        Id = Read<int>(r, "UserId"),
                        FirstName = Read<string>(r, "FirstName"),
                        LastName = Read<string>(r, "LastName")
                    },
                    Books = new List<Book>()
                    {
                        new Book
                        {
                            Id = bookId,
                            Title = Read<string>(r, "Title"),
                            InternalNr = Read<string>(r, "InternalNr"),
                        }
                    },
                    LoanDate = Read<DateTime>(r, "loandate"),
                    ReturnedDate = Read <Nullable<DateTime>>(r, "returnedDate")
                }));

            return loans;
        }

        public LoanReservedBookStatus GetBookLoanStatus(int bookId)
        {
            var bookstatus = LoanReservedBookStatus.Necunoscută;

            _dbRead.Execute(
                "LoansGetBookSatus",
            new[]
            {
                new SqlParameter("@bookId", bookId),

            },
                r => bookstatus = (LoanReservedBookStatus)Read<int>(r, "bookstatus")
                );

            return bookstatus;
        }

        #endregion

        #region Add

        public void AddLoan(int bookId, int userId, DateTime loanDate)
        {
            _dbRead.Execute(
              "LoansAdd",
          new[] { 
                new SqlParameter("@bookId", bookId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@loandate", loanDate),
            });

        }

        #endregion

        #region Update

        public void ReturnBook(int loanId, DateTime dateTime)
        {
            _dbRead.ExecuteNonQuery(
              "LoansReturn",
          new[] { 
                new SqlParameter("@loanId", loanId),
                new SqlParameter("@returnedDate", dateTime),
            });
        }

        #endregion

        public void Remove(int loanId)
        {
            _dbRead.ExecuteNonQuery(
              "LoansRemove",
          new[] { 
                new SqlParameter("@loanId", loanId),
            });
        }

    }
}
