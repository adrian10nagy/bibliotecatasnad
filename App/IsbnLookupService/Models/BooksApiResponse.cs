
namespace IsbnLookupService.Models
{
    public class BooksApiResponse
    {
        public string id { get; set; }
        public string kind { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public BookApiResponse volumeInfo { get; set; }
    }
}
