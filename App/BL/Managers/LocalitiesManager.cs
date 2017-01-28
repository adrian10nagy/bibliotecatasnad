
namespace BL.Managers
{
    using BL.Constants;
    using BL.Cache;
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;

    public static class LocalitiesManager
    {
        public static IEnumerable<Locality> GetAllLocalities()
        {
            var localities = CacheHelper.Instance.GetMyCachedItem(CacheConstants.LocalitiesGetAll) as List<Locality>;
            if (localities == null || localities.Count == 0)
            {
                localities = Kit.Instance.Localities.GetLocalitiesAll() as List<Locality>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.LocalitiesGetAll, localities, MyCachePriority.Default);
            }

            return localities;
        }
    }
}
