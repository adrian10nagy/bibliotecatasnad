
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IPublisherRepository
    {
        IEnumerable<Publisher> GetAllPublishers();
        int AddPublisher(string name);
    }

    public partial class Repository : IPublisherRepository
    {
        #region Get

        public IEnumerable<Publisher> GetAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            _dbRead.Execute(
                "PublishersGetAll",
            null,
                r => publishers.Add(new Publisher()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                }));

            return publishers;
        }

        #endregion
        public int AddPublisher(string name)
        {
            int publisherNr = 0;

            _dbRead.Execute(
               "PublishersAdd",
           new[] { 
                new SqlParameter("@name", name),
            },
            r => publisherNr = Read<int>(r, "Id"));

            return publisherNr;
        }
    }
}