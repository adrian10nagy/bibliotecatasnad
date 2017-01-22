
namespace BL.Managers
{
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;

    public static class AuthorsManager
    {
        public static IEnumerable<Author> GetAllAuthors()
        {
            return Kit.Instance.Authors.GetAllAuthors();
        }

        public static IEnumerable<Author> GetAllAuthorsByInput(string input)
        {
            var authors = (List<Author>)Kit.Instance.Authors.GetAllAuthors();
            if(!string.IsNullOrEmpty(input))
            {
                authors = authors.FindAll(a => a.Name.ToLower().Contains(input));
            }

            return authors;
        }
    }
}
