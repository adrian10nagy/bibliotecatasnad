
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

        public int GetLoanCount()
        {
            return _repository.GetLoanCount();
        }

        #endregion

        public void AddLoan(int bookId, int UserId, System.DateTime loanDate)
        {
            _repository.AddLoan(bookId, UserId, loanDate);
        }

        public IEnumerable<Loan> GetAllActiveLoans()
        {
            return _repository.GetAllActiveLoans();
        }

        public IEnumerable<Loan> GetAllFinishedLoans()
        {
            return _repository.GetAllFinishedLoans();
        }

        public void ReturnBook(int loanId, DateTime dateTime)
        {
            _repository.ReturnBook(loanId, dateTime);
        }

        public void Remove(int loanId)
        {
            _repository.Remove(loanId);
        }

        public List<Loan> GetLoansByDay(DateTime datetime)
        {
            return _repository.GetLoansByDay(datetime);
        }

        public IEnumerable<Loan> GetLoansByUserId(int userId)
        {
            return _repository.GetLoansByUserId(userId);
        }

        public IEnumerable<Loan> GetLoansByBookId(int bookId)
        {
            return _repository.GetLoansByBookId(bookId);
        }
    }
}
