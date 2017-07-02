
namespace BL.Managers
{
    using BL.Cache;
    using BL.Constants;

    public static class CacheManager
    {
        public static void ClearAllCache()
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.AuthorsGetAll);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BookDomainsGetAll);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BookPublishersGetAll);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BooksGetAll);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllActive);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LoansGetAllFinished);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.LocalitiesGetAll);
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.UsersGetAll);

        }
    }
}
