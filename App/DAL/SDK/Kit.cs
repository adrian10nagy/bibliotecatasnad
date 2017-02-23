namespace DAL.SDK
{
    using System;

    public interface IKit : IDisposable
    {
        // Users Users { get; }
    }

    public class Kit : IKit
    {
        private static Kit _instance = new Kit();
        public static Kit Instance
        {
            get
            {
                return _instance ?? getInstance();
            }
        }

        private static Kit getInstance()
        {
            return new Kit();
        }

        public Authors Authors { get { return new Authors(); } }
        public Users Users { get { return new Users(); } }
        public Books Books { get { return new Books(); } }
        public BookAuthors BookAuthors { get { return new BookAuthors(); } }
        public BookDomains BookDomains { get { return new BookDomains(); } }
        public Loans Loans { get { return new Loans(); } }
        public Localities Localities { get { return new Localities(); } }
        public Publishers Publishers { get { return new Publishers(); } }
        public ISBNs ISBNs { get { return new ISBNs(); } }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
