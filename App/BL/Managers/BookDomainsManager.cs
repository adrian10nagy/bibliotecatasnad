
using BL.Cache;
using BL.Constants;
using DAL.Entities;
using DAL.SDK;
using System.Collections.Generic;

namespace BL.Managers
{
    public static class BookDomainsManager
    {
        public static IEnumerable<BookDomain> GetAllBookDomains()
        {
            var bookDomains = CacheHelper.Instance.GetMyCachedItem(CacheConstants.BookDomainsGetAll) as List<BookDomain>;
            if (bookDomains == null || bookDomains.Count == 0)
            {
                bookDomains = Kit.Instance.BookDomains.GetAllBookDomains() as List<BookDomain>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.BookDomainsGetAll, bookDomains, MyCachePriority.Default);
            }

            return bookDomains;
        }

        public static IEnumerable<BookDomain> GetAllBookDomainsByInput(string input)
        {
            var publishers = (List<BookDomain>)GetAllBookDomains();
            if (!string.IsNullOrEmpty(input))
            {
                publishers = publishers.FindAll(a => a.Name.ToLower().Contains(input.ToLower()));
            }

            return publishers;
        }

        public static BookDomain GetAllBookDomainsById(int id)
        {
            var domains = (List<BookDomain>)GetAllBookDomains();
            BookDomain bookDomain = null;
            if (id != 0)
            {
                domains = domains.FindAll(a => a.Id == id);
                if(domains.Count >= 0)
                {
                    bookDomain = domains[0];
                }
            }

            return bookDomain;
        }

        public static BookDomain Add(string name)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.BookDomainsGetAll);

            var bookDomain = new BookDomain
            {
                Id = Kit.Instance.BookDomains.AddBookDomain(name),
                Name = name
            };

            return bookDomain;
        }
    }
}
