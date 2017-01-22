
namespace DAL.Entities
{
    using System;

    public class Loan
    {
        public int Id;
        public User User;
        public Book Book;
        public DateTime LoanDate;
        public DateTime ReturnedDate;
        public BookCondition BookCondition;
    }
}
