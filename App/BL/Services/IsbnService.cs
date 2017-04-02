
using IsbnLookupService;
namespace BL.Services
{
    public static class IsbnService
    {
        public static void GetBook(string isbn)
        {
            var result = RequestManager.Getcontent(isbn.Replace("-", string.Empty).Replace("(",  string.Empty).Replace(")", string.Empty));

        }
    }
}
