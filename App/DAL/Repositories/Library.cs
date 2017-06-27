
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ILibraryRepository
    {
        Library GetLibraryById(int id);
        void Add(Library library);
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

        public void Add(Library library)
        {
            _dbRead.ExecuteNonQuery(
                "LibrariesAdd",
                new[] { 
                    new SqlParameter("@Name", library.Name), 
                    new SqlParameter("@Description", library.Description), 
                    new SqlParameter("@Domain", library.Domain), 
                    new SqlParameter("@Contact", library.Contact), 
                });
        }
    }
}