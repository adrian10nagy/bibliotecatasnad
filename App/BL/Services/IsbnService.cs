
namespace BL.Services
{
    using DAL.Entities;
    using IsbnLookupService;
    using System.Collections.Generic;

    public static class IsbnService
    {
        public static List<Book> GetBook(string isbn)
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
    }
}
