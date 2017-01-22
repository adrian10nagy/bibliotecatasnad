
namespace BL.Managers
{
    using DAL.SDK;

    public static class BooksManager
    {
        public static int GetBooksNrAll()
        {
            return Kit.Instance.Books.GetBookCount();
        }

        public static void AddBook(DAL.Entities.Book book)
        {
            Kit.Instance.Books.AddBook(book);
        }
    }
}
