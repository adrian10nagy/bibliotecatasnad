
namespace BL.Managers
{
    using DAL.SDK;

    public static class ISBNsManager
    {
        public static int? GetBookIdByISBN(string isbn)
        {
            return Kit.Instance.ISBNs.GetBookIdByISBN(isbn);
        }
    }
}
