
using DAL.Entities;
using DAL.SDK;
using System.Collections.Generic;

namespace BL.Managers
{
    public static class PublishersManager
    {
        public static IEnumerable<Publisher> GetAllPublishers()
        {
            return Kit.Instance.Publishers.GetAll();
        }
    }
}
