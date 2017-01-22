
namespace DAL.Repositories
{
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthorsAll();
        int AddAuthor(Author author);
    }

    public partial class Repository : IAuthorRepository
    {
        public IEnumerable<Author> GetAuthorsAll()
        {
            List<Author> authors = new List<Author>();

            _dbRead.Execute(
                "AuthorsGetAll",
            null,
                r => authors.Add(new Author()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                }));

            return authors;
        }

        public int AddAuthor(Author author)
        {
            int authorNr = 1;

            _dbRead.Execute(
               "AuthorsAdd",
           new[] { 
                new SqlParameter("@name", author.Name),
            },
            r => authorNr = Read<int>(r, "Id"));

            return authorNr;
        }
    }
}
