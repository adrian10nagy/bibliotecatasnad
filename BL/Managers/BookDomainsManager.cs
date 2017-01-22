
using DAL.Entities;
using DAL.SDK;
using System.Collections.Generic;

namespace BL.Managers
{
    public static class BookDomainsManager
    {
        public static IEnumerable<BookDomain> GetAllBookDomains()
        {
            return Kit.Instance.BookDomains.GetAllBookDomains();
        }
    }
}
