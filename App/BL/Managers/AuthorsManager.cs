
namespace BL.Managers
{
    using BL.Constants;
    using BL.Cache;
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;

    public static class AuthorsManager
    {
        public static IEnumerable<Author> GetAllAuthors()
        {
            var authors = CacheHelper.Instance.GetMyCachedItem(CacheConstants.AuthorsGetAll) as List<Author>;
            if (authors == null || authors.Count == 0)
            {
                authors = Kit.Instance.Authors.GetAllAuthors() as List<Author>;
                CacheHelper.Instance.AddToMyCache(CacheConstants.AuthorsGetAll, authors, MyCachePriority.Default);
            }

            return authors;
        }

        public static IEnumerable<Author> GetAllAuthorsByInput(string input)
        {
            var authors = (List<Author>)GetAllAuthors();
            if(!string.IsNullOrEmpty(input))
            {
                authors = authors.FindAll(a => a.Name.ToLower().Contains(input.ToLower()));
            }

            return authors;
        }

        public static Author Add(string name)
        {
            CacheHelper.Instance.RemoveMyCachedItem(CacheConstants.AuthorsGetAll);

            var author = new Author
            {
                Id = Kit.Instance.Authors.AddAuthor(name),
                Name = name
            };

            return author;
        }

        public static Author GetAllById(int id)
        {
            var authors = GetAllAuthors() as List<Author>;
            var author = authors.Find(a => a.Id == id);

            return author;
        }
    }
}
