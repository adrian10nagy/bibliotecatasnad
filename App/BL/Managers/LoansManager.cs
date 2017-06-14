
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
        public static int GetLoansNrAll(int libraryId)
        {
            return Kit.Instance.Loans.GetLoanCount(libraryId);
        }

        public static List<Loan> GetActiveLoans(int libraryId)
        {
            var activeLoansCacheKey = string.Format(CacheConstants.LoansGetAllActive, libraryId);

            var loans = CacheHelper.Instance.GetMyCachedItem(activeLoansCacheKey) as List<Loan>;
            if (loans == null || loans.Count == 0)
            {
                loans = Kit.Instance.Loans.GetAllActiveLoans(libraryId) as List<Loan>;
                CacheHelper.Instance.AddToMyCache(activeLoansCacheKey, loans, MyCachePriority.Default);
            }

            return loans;
        }

        public static List<Loan> GetFinishedLoans(int libraryId)
        {
            var finishedLoansCacheKey = string.Format(CacheConstants.LoansGetAllFinished, libraryId);
            var loans = CacheHelper.Instance.GetMyCachedItem(finishedLoansCacheKey) as List<Loan>;

            if (loans == null || loans.Count == 0)
            {
                loans = Kit.Instance.Loans.GetAllFinishedLoans(libraryId) as List<Loan>;
                CacheHelper.Instance.AddToMyCache(finishedLoansCacheKey, loans, MyCachePriority.Default);
            }

            return loans;
        }

        public static List<Loan> GetLoansByDay(DateTime datetime, int libraryId)
        {
            var loans = Kit.Instance.Loans.GetLoansByDay(datetime, libraryId);

             return loans;
        }

        public static void Add(Loan loan, int libraryId)
        {
            var activeLoansCacheKey = string.Format(CacheConstants.LoansGetAllFinished, libraryId);

            CacheHelper.Instance.RemoveMyCachedItem(activeLoansCacheKey);
            foreach (var item in loan.Books)
            {
                Kit.Instance.Loans.AddLoan(item.Id, loan.User.Id, loan.LoanDate);
            }
        }

        public static void ReturnBook(int loanId, DateTime dateTime, int libraryId)
        {
            var activeLoansCacheKey = string.Format(CacheConstants.LoansGetAllActive, libraryId);
            var finishedLoansCacheKey = string.Format(CacheConstants.LoansGetAllFinished, libraryId);

            CacheHelper.Instance.RemoveMyCachedItem(activeLoansCacheKey);
            CacheHelper.Instance.RemoveMyCachedItem(finishedLoansCacheKey);
            Kit.Instance.Loans.ReturnBook(loanId, dateTime);
        }

        public static void Remove(int loanId, int libraryId)
        {
            var activeLoansCacheKey = string.Format(CacheConstants.LoansGetAllActive, libraryId);
            var finishedLoansCacheKey = string.Format(CacheConstants.LoansGetAllFinished, libraryId);

            CacheHelper.Instance.RemoveMyCachedItem(activeLoansCacheKey);
            CacheHelper.Instance.RemoveMyCachedItem(finishedLoansCacheKey);
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

        public static LoanReservedBookStatus GetBookLoanStatus(int bookId)
        {
            return Kit.Instance.Loans.GetBookLoanStatus(bookId);
        }
    }
}
