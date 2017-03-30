
namespace BL.Managers
{
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchManager
    {
        public static List<Book> GetBooksBySimpleSearch(string searchterm)
        {
            var books = new List<Book>();
            if (string.IsNullOrEmpty(searchterm))
            {
                return books;
            }

            var allBooks = BooksManager.GetAllBooks();
            foreach (var item in allBooks)
            {
                if(item.Title.ToLower().Contains(searchterm.ToLower()))
                {
                    books.Add(item);
                }
            }

            return books;
        }

        public static List<Author> GetAuthorsBySimpleSearch(string searchterm)
        {
            var authors = new List<Author>();
            if (string.IsNullOrEmpty(searchterm))
            {
                return authors;
            }

            var allAuthors = AuthorsManager.GetAllAuthors();
            foreach (var item in allAuthors)
            {
                if (item.Name.ToLower().Contains(searchterm.ToLower()))
                {
                    authors.Add(item);
                }
            }

            return authors;
        }

        public static List<BookDomain> GetBookDomainsBySimpleSearch(string searchterm)
        {
            var bookDomains = new List<BookDomain>();
            if (string.IsNullOrEmpty(searchterm))
            {
                return bookDomains;
            }

            var allBookDomains = BookDomainsManager.GetAllBookDomains();
            foreach (var item in allBookDomains)
            {
                if (item.Name.ToLower().Contains(searchterm.ToLower()))
                {
                    bookDomains.Add(item);
                }
            }

            return bookDomains;
        }

        public static List<Book> GetBooksByMultiFieldSearch(string title, int? publisherId, int? domainId, int? authorId)
        {
            var resultBoks = new List<Book>();

            int? publisherIdNullable = (publisherId == 0 || publisherId == null)?null : (int?)publisherId;
            int? domainIdNullable = (domainId == 0 || domainId == null)?null : (int?)domainId;

            var books = Kit.Instance.Books.GetBooksByTitlePublisherDomain(title, publisherIdNullable, domainIdNullable);

            if (authorId.HasValue)
            {
                foreach (var book in books)
                {
                    var authors = BooksManager.GetBookAuthorsByBookId(book.Id) as List<Author>;
                    if (authors.Select(a => a.Id).Contains(authorId.Value))
                    {
                        book.Authors = authors;
                        resultBoks.Add(book);
                    }
                }
            }
            else
            {
                resultBoks = books;
            }

            return resultBoks;
        }
    }
}
