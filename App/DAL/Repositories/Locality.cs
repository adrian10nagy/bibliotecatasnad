
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ILocalityRepository
    {
        IEnumerable<Locality> GetLocalitiesAll();
    }

    public partial class Repository : ILocalityRepository
    {
        public IEnumerable<Locality> GetLocalitiesAll()
        {
            List<Locality> Localites = new List<Locality>();

            _dbRead.Execute(
                "LocalitiesGetAll",
            null,
                r => Localites.Add(new Locality()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                }));

            return Localites;
        }
    }
}
