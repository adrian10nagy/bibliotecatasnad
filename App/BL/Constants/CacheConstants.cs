
namespace BL.Constants
{
    public static class CacheConstants
    {
        // Authors cache
        public static string AuthorsGetAll = "AuthorsGetAll";
        
        // Books cache
        public static string BookDomainsGetAll = "BookDomainsGetAll";
        public static string BookPublishersGetAll = "BookPublishersGetAll";
        public static string BooksGetAll = "BooksGetAll-libraryId={0}";

        // Localities cache
        public static string LocalitiesGetAll = "LocalitiesGetAll";

        // User cache
        public static string UsersGetAll = "UsersGetAll";


        // Loans cache
        public static string LoansGetAllActive = "LoansGetAllActive-libraryId={0}";
        public static string LoansGetAllFinished = "LoansGetAllFinished-libraryId={0}";
    }
}
