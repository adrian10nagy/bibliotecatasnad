
namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public int Id;
        public string Title;
        public int? PublishYear;
        public string Volume;
        public List<ISBN> ISBNs;
        public string InternalNr;
        public int? NrPages;
        public DateTime AddedDate;
        public User AddedBy;
        public Publisher Publisher;
        public BookCondition BookCondition;
        public Library Library;
        public BookFormat BookFormat;
        public BookDomain BookDomain;
        public BookSubject BookSubject;
        public Language BookLanguage;
        public List<Author> Authors;
        public LoanReservedBookStatus BookStatus;
        public DateTime? ReservedUntil;
    }
}
