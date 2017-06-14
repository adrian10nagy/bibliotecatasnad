
namespace DAL.SDK
{
    using DAL.Entities;
    using DAL.Repositories;
    using System;
    using System.Collections.Generic;

    public class Loans
    {
        private static ILoanRepository _repository;

        static Loans()
        {
            _repository = new Repository();
        }

        #region Get

        public int GetLoanCount(int libraryId)
        {
            return _repository.GetLoanCount(libraryId);
        }

        #endregion

        public void AddLoan(int bookId, int UserId, System.DateTime loanDate)
        {
            _repository.AddLoan(bookId, UserId, loanDate);
        }

        public IEnumerable<Loan> GetAllActiveLoans(int libraryId)
        {
            return _repository.GetAllActiveLoans(libraryId);
        }

        public IEnumerable<Loan> GetAllFinishedLoans(int libraryId)
        {
            return _repository.GetAllFinishedLoans(libraryId);
        }

        public void ReturnBook(int loanId, DateTime dateTime)
        {
            _repository.ReturnBook(loanId, dateTime);
        }

        public void Remove(int loanId)
        {
            _repository.Remove(loanId);
        }

        public List<Loan> GetLoansByDay(DateTime datetime, int libraryId)
        {
            return _repository.GetLoansByDay(datetime, libraryId);
        }

        public IEnumerable<Loan> GetLoansByUserId(int userId)
        {
            return _repository.GetLoansByUserId(userId);
        }

        public IEnumerable<Loan> GetLoansByBookId(int bookId)
        {
            return _repository.GetLoansByBookId(bookId);
        }

        public LoanReservedBookStatus GetBookLoanStatus(int bookId)
        {
            return _repository.GetBookLoanStatus(bookId);
        }
    }
}
