
namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;

    public class Loan
    {
        public int Id;
        public User User;
        public List<Book> Books;
        public DateTime LoanDate;
        public Nullable<DateTime> ReturnedDate;
    }
}
