
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ILibraryRepository
    {
        Library GetLibraryById(int id);
    }

    public partial class Repository : ILibraryRepository
    {
        public Library GetLibraryById(int id)
        {
            Library user = null;

            _dbRead.Execute(
                "LibrariesById",
            new[] { 
                new SqlParameter("@Id", id), 
            },
                r => user = new Library()
                {
                    Id = id,
                    Name = Read<string>(r, "Name"),
                    Description = Read<string>(r, "Description"),
                    Contact = Read<string>(r, "Contact"),
                });

            return user;
        }

    }
}