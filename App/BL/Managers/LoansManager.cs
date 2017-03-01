
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using DAL.Entities;
    using DAL.SDK;
    using System;
    using System.Collections.Generic;

    public static class LoansManager
    {
        public static int GetLoansNrAll()
        {
            return Kit.Instance.Loans.GetLoanCount();
        }

        public static List<Loan> GetActiveLoans()
        {
            var loans = CacheHelper.Instance.GetMyCachedItem(CacheConstants.LoansGetAllActive) as List<Loan>;
            if (loans == null || loans.Count == 0)
            {
                loans = Kit.Instance.Loans.GetAllActiveLoans() as List<Loan>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.LoansGetAllActive, loans, MyCachePriority.Default);
            }

            return loans;
        }

        public static List<Loan> GetFinishedLoans()
        {
            var loans = CacheHelper.Instance.GetMyCachedItem(CacheConstants.LoansGetAllFinished) as List<Loan>;
            if (loans == null || loans.Count == 0)
            {
                loans = Kit.Instance.Loans.GetAllFinishedLoans() as List<Loan>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.LoansGetAllFinished, loans, MyCachePriority.Default);
            }

            return loans;
        }

        public static List<Loan> GetLoansByDay(DateTime datetime)
        {
             var loans = Kit.Instance.Loans.GetLoansByDay(datetime);

             return loans;
        }

        public static void Add(Loan loan)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllActive);
            foreach (var item in loan.Books)
            {
                Kit.Instance.Loans.AddLoan(item.Id, loan.User.Id, loan.LoanDate);
            }
        }

        public static void ReturnBook(int loanId, DateTime dateTime)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllActive);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllFinished);
            Kit.Instance.Loans.ReturnBook(loanId, dateTime);
        }

        public static void Remove(int loanId)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllActive);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllFinished);
            Kit.Instance.Loans.Remove(loanId);
        }

        public static List<Loan> GetLoansByUserId(int userId)
        {
            return Kit.Instance.Loans.GetLoansByUserId(userId) as List<Loan>;
        }

        public static List<Loan> GetLoansByBookId(int bookId)
        {
            return Kit.Instance.Loans.GetLoansByBookId(bookId) as List<Loan>;
        }
    }
}
