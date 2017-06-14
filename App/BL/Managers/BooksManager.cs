
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using BL.Entities;
    using DAL.Entities;
    using DAL.SDK;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BooksManager
    {
        public static int GetBooksNrAll(int libraryId)
        {
            return Kit.Instance.Books.GetBookCount(libraryId);
        }

        public static IEnumerable<Book> GetAllBooks(int libraryId)
        {
            var bookcacheKey = string.Format(CacheConstants.BooksGetAll, libraryId);
            var books = CacheHelper.Instance.GetMyCachedItem(bookcacheKey) as List<Book>;

            if (books == null || books.Count == 0)
            {
                books = Kit.Instance.Books.GetAllBooks(libraryId) as List<Book>;
                books = books.OrderBy(b => b.InternalNr).ToList();
                foreach (var item in books)
                {
                    item.ISBNs = Kit.Instance.ISBNs.GetAllByBookId(item.Id) as List<ISBN>;
                    item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
                }
                CacheHelper.Instance.AddToMyCache(bookcacheKey, books, MyCachePriority.Default);
            }

            return books;
        }

        public static IEnumerable<Book> GetAllBooksWithDomain(int libraryId)
        {
            var books = Kit.Instance.Books.GetAllBooksWithDomain(libraryId);
            books = books.OrderBy(b => b.InternalNr).ToList();
            foreach (var item in books)
            {
                item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
            }

            return books;
        }

        public static IEnumerable<Book> GetAllBooksByDomainId(int domainId, int libraryId)
        {
            var books = Kit.Instance.Books.GetAllBooksByDomainId(domainId, libraryId);
            foreach (var item in books)
            {
                item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
            }

            return books;
        }

        public static BookPublishersChart GetBookPublisherForChart(int libraryId)
        {
            var maxPublishers = 4;
            var bookPublishers = Kit.Instance.Books.GetAllBookPublishersGrouped(libraryId) as List<Publisher>;

            var topPublishers = new List<Publisher>();
            for (int i = 0; i < maxPublishers; i++)
            {
                topPublishers.Add(bookPublishers[i]);
            }

            var otherPublishers = new List<Publisher>();
            for (int i = maxPublishers; i < bookPublishers.Count; i++)
            {
                otherPublishers.Add(bookPublishers[i]);
            }

            topPublishers.Add(new Publisher
            {
                Name = "Altele",
                Id = otherPublishers.Sum(p => p.Id)
            });

            var result = new BookPublishersChart()
            {
                Dataset = new Dataset(),
                Labels = new List<string>()
            };

            var labels = new List<string>();
            string[] datas = new string[maxPublishers + 1];
            string[] percentage = new string[maxPublishers + 1];
            var booksNumber = (double)bookPublishers.Sum(p => p.Id);
            for (int i = 0; i < maxPublishers + 1; i++)
            {
                labels.Add(topPublishers[i].Name);
                datas[i] = topPublishers[i].Id.ToString();
                var percentageValue = (((double)topPublishers[i].Id / (double)booksNumber) * 100);
                percentageValue = System.Math.Round(percentageValue, 2);
                percentage[i] = percentageValue.ToString();
            }

            result.Labels = labels;
            result.Dataset.Data = datas;
            result.Dataset.Percentage = percentage;
            result.Dataset.BackgroundColor = new List<string>
            { 
                "#3498DB", 
                "#9B59B6",
                "#E74C3C",
                "#26B99A",
                "#BDC3C7"
            }.ToArray();
            result.Dataset.SquareColors = new List<string>
            {
                "blue",
                "purple",
                "red",
                "green",
                "aero"
            }.ToArray();

            return result;
        }

        public static IEnumerable<Author> GetBookAuthorsByBookId(int bookId)
        {
            return Kit.Instance.BookAuthors.GetAllBookAuthorsByBookId(bookId);
        }

        public static Book GetBookById(int bookId)
        {
            Book book = Kit.Instance.Books.GetBookById(bookId);
            if (book != null)
            {
                book.Authors = GetBookAuthorsByBookId(bookId) as List<Author>;
                book.ISBNs = Kit.Instance.ISBNs.GetAllByBookId(book.Id) as List<ISBN>;
            }

            return book;
        }

        public static List<Book> GetBooksByDay(DateTime dateTime, int libraryId)
        {
            var books = Kit.Instance.Books.GetBooksByDay(dateTime, libraryId);

            return books;
        }

        public static IEnumerable<Book> GetBooksByAuthorId(int id, int libraryId)
        {
            var books = Kit.Instance.Books.GetBooksByAuthorId(id, libraryId);
            foreach (var item in books)
            {
                item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
            }

            return books;
        }

        public static IEnumerable<Book> GetBooksByPublisherId(int id, int libraryId)
        {
            var books = Kit.Instance.Books.GetBooksByPublisherId(id, libraryId);
            foreach (var item in books)
            {
                item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
            }

            return books;
        }

        public static void AddBook(Book book, int libraryId)
        {
            var bookcacheKey = string.Format(CacheConstants.BooksGetAll, libraryId);

            CacheHelper.Instance.RemoveMyCachedItem(bookcacheKey);

            book.Id = Kit.Instance.Books.AddBook(book);
            AddBookAuthors(book);
            AddBookIsbns(book);
        }

        private static void AddBookIsbns(Book book)
        {
            foreach (var isbn in book.ISBNs)
            {
                Kit.Instance.ISBNs.AddISBN(book.Id, isbn.Value);
            }
        }

        public static void AddBookAuthors(Book book)
        {
            foreach (var author in book.Authors)
            {
                author.BookAthorType = BookAthorType.Author;
                Kit.Instance.BookAuthors.AddBookAuthor(author, book.Id);
            }
        }

        public static void Update(Book book, int libraryId)
        {
            var bookcacheKey = string.Format(CacheConstants.BooksGetAll, libraryId);

            CacheHelper.Instance.RemoveMyCachedItem(bookcacheKey);

            //Update Book
            Kit.Instance.Books.UpdateBook(book);

            // Update Authors
            var bookAuthorsBeforeChange = BooksManager.GetBookAuthorsByBookId(book.Id) as List<Author>;
            var authorsAreTheSame = (string.Join(", ", bookAuthorsBeforeChange.Select(i => i.Id)) == string.Join(", ", book.Authors.Select(i => i.Id)));

            // Update ISBNS
            var bookIsbnsBeforeChange = Kit.Instance.ISBNs.GetAllByBookId(book.Id) as List<ISBN>;
            var isbnsAreTheSame = (string.Join(", ", bookIsbnsBeforeChange.Select(i => i.Value)) == string.Join(", ", book.ISBNs.Select(i => i.Value)));

            if (!authorsAreTheSame)
            {
                Kit.Instance.Books.RemoveAuthors(book.Id);
                AddBookAuthors(book);
            }

            if (!isbnsAreTheSame)
            {
                Kit.Instance.ISBNs.RemoveISBNsByBookId(book.Id);
                AddBookIsbns(book);
            }
        }

        public static List<Book> GetBooksLastAdded(int nr, int libraryId)
        {
            var books = Kit.Instance.Books.GetBooksLastAdded(nr, libraryId) as List<Book>;
            foreach (var item in books)
            {
                item.Authors = GetBookAuthorsByBookId(item.Id) as List<Author>;
            }

            return books;
        }

        public static Book GetBookByISBN(string isbn, int libraryId)
        {
            Book book = Kit.Instance.Books.GetBookByISBN(isbn, libraryId);

            return book;
        }
    }
}
