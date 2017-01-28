
using BL.Cache;
using BL.Constants;
using DAL.Entities;
using DAL.SDK;
using System.Collections.Generic;

namespace BL.Managers
{
    public static class PublishersManager
    {
        public static IEnumerable<Publisher> GetAllPublishers()
        {
            var publishers = CacheHelper.Instance.GetMyCachedItem(CacheConstants.BookPublishersGetAll) as List<Publisher>;
            if (publishers == null || publishers.Count == 0)
            {
                publishers = Kit.Instance.Publishers.GetAll() as List<Publisher>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.BookPublishersGetAll, publishers, MyCachePriority.Default);
            }

            return publishers;
        }

        public static IEnumerable<Publisher> GetAllPublishersByInput(string input)
        {
            var publishers = (List<Publisher>)GetAllPublishers();
            if (!string.IsNullOrEmpty(input))
            {
                publishers = publishers.FindAll(a => a.Name.ToLower().Contains(input.ToLower()));
            }

            return publishers;
        }

        public static Publisher Add(string name)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BookPublishersGetAll);

            var publisher = new Publisher
            {
                Id = Kit.Instance.Publishers.AddPublisher(name),
                Name = name
            };

            return publisher;
        }
    }
}
