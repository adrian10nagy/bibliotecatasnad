
namespace IsbnLookupService.Models
{
    public class GoogleApiResponse
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public BooksApiResponse[] items { get; set; }
    }
}
