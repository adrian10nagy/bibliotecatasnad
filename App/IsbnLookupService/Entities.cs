
namespace IsbnLookupService
{
    using System;

    public class GoogleApiResponse
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public BooksApiResponse[] items { get; set; }
    }

    public class BooksApiResponse
    {
        public string id { get; set; }
        public string kind { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public BookApiResponse volumeInfo { get; set; }
    }

    public class BookApiResponse
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public int pageCount { get; set; }
        public string[] categories { get; set; }
        public ImageLink imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
    }

    public class ImageLink
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }
}
