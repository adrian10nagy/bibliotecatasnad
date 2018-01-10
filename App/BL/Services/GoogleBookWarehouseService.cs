
namespace BL.Services
{
    using DAL.Entities;
    using IsbnLookupService;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class GoogleBookWarehouseService
    {
        public static List<Book> GetBookByIsbn(string isbn)
        {
            string addressOld = "https://www.googleapis.com/books/v1/volumes?q={0}";
            string addressNew = "https://www.googleapis.com/books/v1/volumes?q=isbn:{0}";
            isbn = isbn.Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

            List<string> selfLinks = new List<string>();
            var result = RequestManager.GetBookFromGoogleApi(isbn, addressOld);
            result.AddRange(RequestManager.GetBookFromGoogleApi(isbn, addressNew, result));

            var bookLibNet = RequestManager.GetcontentFromLibrarieNet(isbn);
            if(bookLibNet != null)
            {
                result.Add(bookLibNet);
            }

            return result;
        }

        public static async Task<List<Book>> GetBookByIsbnAsync(string isbn)
        {
            string addressOld = "https://www.googleapis.com/books/v1/volumes?q={0}";
            string addressNew = "https://www.googleapis.com/books/v1/volumes?q=isbn:{0}";
            isbn = isbn.Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

            List<string> selfLinks = new List<string>();
            var result1 = RequestManager.GetBookFromGoogleApiAsync(isbn, addressOld);
            var result2 = RequestManager.GetBookFromGoogleApiAsync(isbn, addressNew);

            var result = await result1;
            result.AddRange(await result2);

            var bookLibNet = RequestManager.GetcontentFromLibrarieNet(isbn);
            if(bookLibNet != null)
            {
                result.Add(bookLibNet);
            }

            return result;
        }

        public static List<Book> GetBookByTitle(string title)
        {
            string addressOld = "https://www.googleapis.com/books/v1/volumes?q={0}";

            List<string> selfLinks = new List<string>();
            if(!string.IsNullOrEmpty(title))
            {
                title = title.Trim();
            }
            var result = RequestManager.GetBookFromGoogleApi(title, addressOld);

            var bookLibNet = RequestManager.GetcontentFromLibrarieNet(title);
            if (bookLibNet != null)
            {
                result.Add(bookLibNet);
            }

            return result;
        }
    }
}
