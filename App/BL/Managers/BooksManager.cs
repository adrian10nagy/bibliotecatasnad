
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;
    using BL.Entities;
    using DAL.Entities;
    using DAL.SDK;
    using System;
    using System.Collections.Generic;

    public static class BooksManager
    {
        public static int GetBooksNrAll()
        {
            return Kit.Instance.Books.GetBookCount();
        }


        public static IEnumerable<Book> GetAllBooks()
        {
            var books = CacheHelper.Instance.GetMyCachedItem(CacheConstants.BooksGetAll) as List<Book>;
            if (books == null || books.Count == 0)
            {
                books = Kit.Instance.Books.GetAllBooks() as List<Book>;
                foreach (var item in books)
                {
                    item.ISBNs = Kit.Instance.ISBNs.GetAllByBookId(item.Id)as List<ISBN>;
                    item.Authors = BooksManager.GetBookAuthorsByBookId(item.Id) as List<Author>;
                }
                CacheHelper.Instance.AddToMyCache(CacheConstants.BooksGetAll, books, MyCachePriority.Default);
            }

            return books;
        }

        public static BookPublishersChart GetBookPublisherForChart()
        {
            var bookPublishers = Kit.Instance.Books.GetAllBookPublishersGrouped() as List<Publisher>;
            bookPublishers.Sort((x, y) => x.Id.CompareTo(y.Id));
            var result = new BookPublishersChart()
            {
                Dataset = new Dataset(),
                Labels = new List<string>()
            };

            var labels = new List<string>();
            string[] datas = new string[bookPublishers.Count];

            for (int i = 0; i < bookPublishers.Count; i++)
            {
                labels.Add(bookPublishers[i].Name);
                datas[i] = bookPublishers[i].Id.ToString();
            }
            result.Labels = labels;
            result.Dataset.Data = datas;
            result.Dataset.BackgroundColor = new List<string>
            { 
                "#9B59B6",
                "#E74C3C",
                "#26B99A",
                "#3498DB", 
                "#BDC3C7"
            }.ToArray();
            result.Dataset.HoverBackgroundColor = new List<string>
            {
                "#B370CF",
                "#E95E4F",
                "#36CAAB",
                "#49A9EA",
                "#CFD4D8"
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

        public static void AddBook(DAL.Entities.Book book)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BooksGetAll);

            book.Id = Kit.Instance.Books.AddBook(book);
            AddBookAuthor(book);
        }

        public static void AddBookAuthor(Book book)
        {
            foreach (var author in book.Authors)
            {
                author.BookAthorType = BookAthorType.Author;
                Kit.Instance.BookAuthors.AddBookAuthor(author, book.Id);
            }
        }

        public static void Update(Book book)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BooksGetAll);

            //Update Book
            Kit.Instance.Books.UpdateBook(book);
            // Update Authors
            Kit.Instance.Books.RemoveAuthors(book.Id);
            AddBookAuthor(book);
        }
    }
}
