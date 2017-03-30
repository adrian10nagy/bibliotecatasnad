
namespace BL.Managers
{
    using BL.Constants;
    using BL.Cache;
    using DAL.Entities;
    using DAL.SDK;
    using System.Collections.Generic;
    using System.Linq;

    public static class AuthorsManager
    {
        #region Get
        public static IEnumerable<Author> GetAllAuthors()
        {
            var authors = CacheHelper.Instance.GetMyCachedItem(CacheConstants.AuthorsGetAll) as List<Author>;
            if (authors == null || authors.Count == 0)
            {
                authors = Kit.Instance.Authors.GetAllAuthors() as List<Author>;
                authors = authors.OrderBy(a => a.Name).ToList();
                CacheHelper.Instance.AddToMyCache(CacheConstants.AuthorsGetAll, authors, MyCachePriority.Default);
            }

            return authors;
        }

        public static IEnumerable<Author> GetAllAuthorsByInput(string input)
        {
            var authors = (List<Author>)GetAllAuthors();
            if (!string.IsNullOrEmpty(input))
            {
                authors = authors.FindAll(a => a.Name.ToLower().Contains(input.ToLower()));
            }

            return authors;
        }

        public static Author GetById(int id)
        {
            var authors = GetAllAuthors() as List<Author>;
            var author = authors.Find(a => a.Id == id);

            return author;
        }

        public static Author GetOneAuthorByInput(string input)
        {
            List<Author> authors = (List<Author>)GetAllAuthors();

            Author author = null;
            if (!string.IsNullOrEmpty(input))
            {
                var initialAuthors = authors.Where(a => a.Name.ToLower() == input.ToLower()).ToList();
                if (initialAuthors != null && initialAuthors.Count() > 0)
                {
                    author = initialAuthors[0];
                }
            }

            return author;
        }

        #endregion

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
    }
}
