
namespace IsbnLookupService
{
    using DAL.Entities;
    using DAL.SDK;
    using IsbnLookupService.Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public static class RequestManager
    {
        public static Book GetcontentFromLibrarieNet(string isbn)
        {
            var book = GetBookFromLibrarieNet(isbn);
            return book;
        }

        /// <summary>
        /// Gets the book data from Google API ex 9789736898655, 9789734622870
        ///  goodreads api
        // https://www.goodreads.com/book/isbn/0441172717?callback=myCallback&format=xml&key=syXyeRzn3oRl7gdY1hR2Fg
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="address"></param>
        /// <param name="addedBooks"></param>
        /// <returns></returns>
        public static List<Book> GetBookFromGoogleApi(string isbn, string address, List<Book> addedBooks = null)
        {
            var books = new List<Book>();

            // validate input
            if (string.IsNullOrEmpty(isbn))
            {
                return books;
            }

            if (addedBooks == null)
            {
                addedBooks = new List<Book>();
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var downloadString = client.DownloadString(string.Format(address, isbn));

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    GoogleApiResponse googleBooks = js.Deserialize<GoogleApiResponse>(downloadString);
                    if (googleBooks.totalItems > 0)
                    {
                        foreach (var item in googleBooks.items)
                        {
                            if (addedBooks == null || addedBooks.FindAll(b => b.SelfLink == item.selfLink).Count < 1)
                            {
                                var bookDownloadString = client.DownloadString(item.selfLink);
                                BooksApiResponse bookResponse = js.Deserialize<BooksApiResponse>(bookDownloadString);

                                // map to Book
                                var book = MapGoogleApiBookToDalBook(bookResponse, isbn);
                                books.Add(book);
                                addedBooks.Add(book);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog er = new ErrorLog();
                er.CreatedDate = DateTime.Now;
                er.Message = "Method: GetBookFromGoogleApi: Address: " + address +
                    " isbn: " + isbn +
                    " InnerException: " + e.InnerException +
                    " Message: " + e.Message;
                Kit.Instance.ErrorLogs.AddErrorLog(er);
            }

            return books;
        }

        /// <summary>
        /// Gets the book data from Google API ex 9789736898655, 9789734622870
        ///  goodreads api
        // https://www.goodreads.com/book/isbn/0441172717?callback=myCallback&format=xml&key=syXyeRzn3oRl7gdY1hR2Fg
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="address"></param>
        /// <param name="addedBooks"></param>
        /// <returns></returns>
        public static async Task<List<Book>> GetBookFromGoogleApiAsync(string isbn, string address)
        {
            var books = new List<Book>();
            // validate input
            if (string.IsNullOrEmpty(isbn))
            {
                return books;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var downloadString = client.DownloadString(string.Format(address, isbn));

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    GoogleApiResponse googleBooks = js.Deserialize<GoogleApiResponse>(downloadString);
                    if (googleBooks.totalItems > 0)
                    {
                        foreach (var item in googleBooks.items)
                        {
                            var bookDownloadString = client.DownloadString(item.selfLink);
                            BooksApiResponse bookResponse = js.Deserialize<BooksApiResponse>(bookDownloadString);

                            // map to Book
                            var book = MapGoogleApiBookToDalBook(bookResponse, isbn);
                            books.Add(book);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog er = new ErrorLog();
                er.CreatedDate = DateTime.Now;
                er.Message = "Method: GetBookFromGoogleApi: Address: " + address +
                    " isbn: " + isbn +
                    " InnerException: " + e.InnerException +
                    " Message: " + e.Message;
                Kit.Instance.ErrorLogs.AddErrorLog(er);
                return books;
            }

            return books;
        }

        private static Book MapGoogleApiBookToDalBook(BooksApiResponse item, string isbn)
        {
            DateTime bookPublishedDate = DateTime.MinValue;
            var publishedDate = item.volumeInfo.publishedDate;
            if (publishedDate != null)
            {
                publishedDate = publishedDate.Replace("*", "");

                if (DateTime.TryParseExact(publishedDate,
                       "yyyy-dd-MM",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out bookPublishedDate))
                {
                }
                else if (DateTime.TryParseExact(publishedDate,
                       "yyyy-MM-dd",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out bookPublishedDate))
                {
                }
                else if (DateTime.TryParseExact(publishedDate,
                      "yyyy",
                      CultureInfo.InvariantCulture,
                      DateTimeStyles.None,
                      out bookPublishedDate))
                {
                }
                else if (DateTime.TryParseExact(publishedDate,
                      "dd-MM-yyyy",
                      CultureInfo.InvariantCulture,
                      DateTimeStyles.None,
                      out bookPublishedDate))
                {
                }
                else if (DateTime.TryParseExact(publishedDate,
                 "yyyy-MM",
                 CultureInfo.InvariantCulture,
                 DateTimeStyles.None,
                 out bookPublishedDate))
                {
                }
                else
                {
                    ErrorLog er = new ErrorLog();
                    er.CreatedDate = DateTime.Now;
                    er.Message = "Method: MapGoogleApiBookToDalBook Error: date not in correct format" +
                        " date: " + publishedDate;

                    Kit.Instance.ErrorLogs.AddErrorLog(er);
                }
            }

            var book = new Book
            {
                Authors = getAuthorsFromGoogleApi(item.volumeInfo.authors),
                BookLanguage = getLanguageFromGoogleApi(item.volumeInfo.language),
                BookDomain = new BookDomain
                {
                    Name = ((item.volumeInfo.categories != null) && (item.volumeInfo.categories.Length != 0)) ? item.volumeInfo.categories[0] : string.Empty
                },
                Description = item.volumeInfo.description,
                ImageUrl = (item.volumeInfo.imageLinks != null) ? item.volumeInfo.imageLinks.smallThumbnail : string.Empty,
                NrPages = item.volumeInfo.pageCount,
                PreviewLink = item.volumeInfo.previewLink + "&printsec=frontcover",
                Publisher = new Publisher
                {
                    Name = item.volumeInfo.publisher
                },
                PublishYear = (bookPublishedDate != DateTime.MinValue) ? (int?)bookPublishedDate.Year : null,
                Title = item.volumeInfo.title,
                BookFormat = BookFormat.Hârtie,
                BookCondition = BookCondition.Standard,
                SelfLink = item.selfLink,
                ISBNs = new List<ISBN>
                    {
                        new ISBN
                        {
                            Value = isbn
                        }
                    }
            };

            return book;
        }

        private static List<Author> getAuthorsFromGoogleApi(string[] authorStrings)
        {
            var authors = new List<Author>();

            if (authorStrings == null)
            {
                authors.Add(new Author
                {
                    Id = 1
                });

                return authors;
            }

            foreach (var item in authorStrings)
            {
                authors.Add(new Author
                {
                    Name = item
                });
            }

            return authors;
        }

        private static Language getLanguageFromGoogleApi(string lang)
        {
            Language language;
            if (lang == "ro")
            {
                language = Language.Română;
            }
            else if (lang == "en")
            {
                language = Language.Engleză;
            }
            else
            {
                language = Language.Română;
            }

            return language;
        }

        #region ubb

        public static Book GetcontentFromUbb(string isbn)
        {
            var book = GetBookFromUbb(isbn);
            return book;
        }

        private static Book GetBookFromUbb(string isbn)
        {
            Book book = null;
            var address = "http://aleph.bcucluj.ro:8991/F/ER1NL9K1P935I64BSAV442MH1XTXKTYY8LCGMH3CML4A1BJ8Q1-22499?func=find-b&request={0}&find_code=ISB&local_base=CUC01&adjacent=Y&x=43&y=7";
            var bookUrl = string.Empty;

            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(string.Format(address, isbn));
                if (downloadString.Contains("Trucuri în căutare") && downloadString.Contains("Cu cât introduceţi mai multe cuvinte cu atât mai precisă va fi informaţia primită"))
                {
                    return null;
                }

                bookUrl = GetBookUrlFromSearchUbb(downloadString);

            }

            if (bookUrl != null)
            {
                book = GetBookDetailsFromUbb(bookUrl);
            }

            return book;
        }

        private static string GetBookUrlFromSearchUbb(string page)
        {
            string result = null;

            try
            {

                var splited = page.IndexOf("<A HREF=http://aleph.bcucluj.ro:8991");
                var splited2 = page.Remove(0, splited + 8);
                var splited3 = splited2.IndexOf(">");
                if (splited3 >= 0)
                {
                    result = splited2.Remove(splited3);
                }
            }
            catch (Exception e)
            {
                // to do log error
                return null;
            }

            return result;
        }

        private static Book GetBookDetailsFromUbb(string bookUrl)
        {
            //var recordLinkRaw = '
            Book book = null;

            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(bookUrl);
                var index1 = downloadString.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ").Split(new string[] { "<div class=\"produs_campuri\">" }, StringSplitOptions.None);
                var title = GetDetailsByNameFromLibrarieNet(index1, "title");
                var editura = GetDetailsByNameFromLibrarieNet(index1, "Editura:");
                if (string.IsNullOrEmpty(editura))
                {
                    editura = GetDetailsByNameFromLibrarieNet(index1, "Producator:");
                }
                var autor = GetDetailsByNameFromLibrarieNet(index1, "Autor(i):");
                var an = GetDetailsByNameFromLibrarieNet(index1, "Anul aparitiei:");
                var nrPag = GetDetailsByNameFromLibrarieNet(index1, "Nr. pagini:");

                book = new Book
                {
                    Title = title,
                    Publisher = new Publisher
                    {
                        Name = editura
                    },
                    Authors = new System.Collections.Generic.List<Author>
                    {
                        new Author
                        {
                            Name = autor
                        }
                    },
                    PublishYear = an.ToNullableInt(),
                    NrPages = nrPag.ToNullableInt()
                };
            }

            return book;
        }

        #endregion

        #region librarieNet

        public static Book GetBookFromLibrarieNet(string isbn)
        {
            Book book = null;
            var address = "http://www.librarie.net/cautare-rezultate.php?t={0}";
            var bookUrl = string.Empty;

            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(string.Format(address, isbn));
                if (downloadString.Contains("Cautare esuata") && downloadString.Contains("Nu exista rezultate la cautarea dupa"))
                {
                    return null;
                }

                bookUrl = GetBookUrlFromSearchLibrarieNet(downloadString);

            }

            if (bookUrl != null)
            {
                book = GetBookDetailsFromLibrarieNet(bookUrl);
            }

            return book;
        }

        private static Book GetBookDetailsFromLibrarieNet(string url)
        {
            Book book = null;

            if (string.IsNullOrEmpty(url))
            {
                return book;
            }

            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(url);
                var contentSplit = downloadString.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ").Split(new string[] { "<div class=\"produs_campuri\">" }, StringSplitOptions.None);
                var title = GetDetailsByNameFromLibrarieNet(contentSplit, "title");
                var editura = GetDetailsByNameFromLibrarieNet(contentSplit, "Editura:");
                if (string.IsNullOrEmpty(editura))
                {
                    editura = GetDetailsByNameFromLibrarieNet(contentSplit, "Producator:");
                }
                var autor = GetDetailsByNameFromLibrarieNet(contentSplit, "Autor(i):");
                var an = GetDetailsByNameFromLibrarieNet(contentSplit, "Anul aparitiei:");
                var nrPag = GetDetailsByNameFromLibrarieNet(contentSplit, "Nr. pagini:");

                book = new Book
                {
                    Title = title,
                    Publisher = new Publisher
                    {
                        Name = editura
                    },
                    Authors = new System.Collections.Generic.List<Author>
                    {
                        new Author
                        {
                            Name = autor
                        }
                    },
                    PublishYear = an.ToNullableInt(),
                    NrPages = nrPag.ToNullableInt(),
                    BookLanguage = Language.Română
                };
            }

            return book;
        }

        public static string GetDetailsByNameFromLibrarieNet(string[] source, string property)
        {
            //"\n<!DOCTYPE html>\r\n<html lang=\"ro\">\r\n<head>\r\n<title>Poezii pentru copii</title>\r\n<!--PARAMETRII AFILIATI: nia=0 niaadgr= niascr=COOKIE -->\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"http://www.librarie.net/css/style.css\">\r\n<link rel=\"shortcut icon\" href=\"http://www.librarie.net/favicon.ico\">\r\n<link rel=\"canonical\" href=\"http://www.librarie.net/p/265281/poezii-pentru-copii\">\r\n<meta name=\"description\" content=\"Poezii pentru copii - comanda online - Pret: 12,80 (20% reducere)\">\r\n\r\n<meta name=\"language\" content=\"RO\">\r\n<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\">\r\n<meta name=\"verify-v1\" content=\"T7T5q8DaMRE8olqQYL2cSGHzvsrbe38v3fujOMw6O4s=\" />\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n<script src=\"http://www.librarie.net/js/cookies2.js\"></script>\r\n<script src=\"http://www.librarie.net/js/javascript.js\"></script>\r\n<script src=\"http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js\" ></script>\r\n<script src=\"http://www.librarie.net/js/stickynote.js\" ></script>\r\n<script>\r\nvar cookiesNote=new stickynote({\r\n    content:{divid:'stickynote4cookies', source:'http://www.librarie.net/inc/cookies.txt'},\r\n    pos:['right', 'bottom'],\r\n showfrequency:\"always\",\r\n hidebox:0\r\n})\r\n</script>\r\n</head>\r\n\r\n<body>\r\n  \r\n<!-- Google Analytics -->\r\n<script>\r\nwindow.ga=window.ga||function(){(ga.q=ga.q||[]).push(arguments)};ga.l=+new Date;\r\nga('create', 'UA-375865-1', 'auto');\r\nga('send', 'pageview');\r\n</script>\r\n<script async src='https://www.google-analytics.com/analytics.js'></script>\r\n<!-- End Google Analytics -->  \r\n  \r\n<!-- Facebook Pixel Code -->\r\n<script>\r\n!function(f,b,e,v,n,t,s){if(f.fbq)return;n=f.fbq=function(){n.callMethod?\r\nn.callMethod.apply(n,arguments):n.queue.push(arguments)};if(!f._fbq)f._fbq=n;\r\nn.push=n;n.loaded=!0;n.version='2.0';n.queue=[];t=b.createElement(e);t.async=!0;\r\nt.src=v;s=b.getElementsByTagName(e)[0];s.parentNode.insertBefore(t,s)}(window,\r\ndocument,'script','//connect.facebook.net/en_US/fbevents.js');\r\n\r\nfbq('init', '1566832486900499');\r\nfbq('track', \"PageView\");</script>\r\n<noscript><img height=\"1\" width=\"1\" style=\"display:none\"\r\nsrc=\"https://www.facebook.com/tr?id=1566832486900499&ev=PageView&noscript=1\"\r\n/></noscript>\r\n<!-- End Facebook Pixel Code -->            \r\n<div class=\"container\" align=\"center\">\r\n    <div class=\"top_line\" align=\"right\">\r\n        <a title=\"Clienti - administrare\" href=\"http://www.librarie.net/clienti\">LOGIN</a>\r\n            | <a title=\"Formular de contact, numere de telefon, adresa noastra\" href=\"http://www.librarie.net/informatii-contact.php\">CONTACT</a>\r\n            | <a title=\"Informatii despre noi\" href=\"http://www.librarie.net/informatii.php\">INFO</a>\r\n    </div>\r\n</div>\r\n\r\n<table class=\"container\" cellspacing=\"0\" cellpadding=\"2\" align=\"center\">\r\n    <tr>\r\n        <td width=\"1%\" valign=\"top\" class=\"logo\">\r\n            <a title=\"LIBRARIE.NET\" href=\"http://www.librarie.net\"><img vspace=\"0\" hspace=\"0\" alt=\"LIBRARIE.NET\" src=\"http://www.librarie.net/images/librarie.png\" width=\"240\" height=\"71\"></a>\r\n        </td>\r\n\r\n        <td valign=\"middle\" align=\"center\">\r\n            <div class=\"css_cautare_form_top\">\r\n    <form name=\"cautare_form\" action=\"http://www.librarie.net/cautare-rezultate.php\">\r\n        <input class=\"css_cautare_form_input\" type=\"text\" name=\"t\" value=\"\" size=\"40\">\r\n        <input class=\"css_cautare_form_submit\" type=\"submit\" value=\"Cautare\">\r\n    </form>\r\n</div>\r\n        </td>\r\n        <td valign=\"middle\">\r\n            <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" class=\"cos\">\r\n                <tr>\r\n                    <td valign=\"top\" onMouseOver=\"this.cursor='pointer';this.style.color='#880000';this.style.background='#FFFFE0'\" onMouseOut=\"this.style.color='#000000';this.style.background='#ffffff'\" onClick=\"window.location='http://www.librarie.net/cos.php'\">\r\n                        <strong>Co&#537; de cump&#259;r&#259;turi</strong>\r\n                        <br><span><b>0 produse</b></span>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t<td width=\"1%\">\r\n\t\t\t\t\t\t\t<a title=\"COS CUMPARATURI\" href=\"http://www.librarie.net/cos.php\"><img alt=\"COS CUMPARATURI\" border=\"0\" src=\"http://www.librarie.net/images/cos.gif\"></a>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\r\n\t\t</tr>\r\n\t</table>\r\n\r\n<div align=\"center\">\r\n<nav>\r\n  <ul>\r\n    <li ><a href=\"http://www.librarie.net/carti\">Carti</a></li><li ><a href=\"http://www.librarie.net/jucarii\">Jucarii</a></li><li ><a href=\"http://www.librarie.net/filme\">Filme</a></li><li ><a href=\"http://www.librarie.net/muzica\">Muzica</a></li><li ><a href=\"http://www.librarie.net/diverse\">Diverse</a></li>\r\n    <li ><a href=\"http://www.librarie.net/promotii\">PromoĹŁii</a></li>\r\n    <li> <a href=\"http://www.librarie.net/top\">Top</a></li>\r\n  </ul>\r\n</nav>\r\n</div>        \r\n    \r\n\r\n\n<table align=\"center\" class=\"container\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"margin-top:3px;\">\n    <tr>\n        <td valign=\"top\" align=\"left\" class=\"center_panel\">\n                        <table width=\"100%\">\n    <tr>\n        <td width=\"1%\" valign=\"top\">\n            <img src=\"http://www.librarie.net/coperti2/2/6/5/2/8/1/300x300.jpg\" width=\"300\" height=\"300\" border=\"0\" alt=\"\">\n        </td>\n        <td class=\"carte\" valign=\"top\">\n            <div class=\"css_carte_titlu\">\n                <h1><b>Poezii pentru copii</b></h1>\n            </div>\n            <table width=\"100%\">\n                <tr>\n                    <td valign=\"top\">\n                        "
            foreach (var item in source)
            {
                if (item.Contains(property))
                {
                    var itemChanged = item.Replace(property, string.Empty);
                    var textWithoutHtmlTags = Regex.Replace(itemChanged, "<.*?>", String.Empty);
                    if (property.Contains("title"))
                    {
                        var ii = textWithoutHtmlTags.IndexOf("var cookiesNote=new stickynote");
                        if (ii >= 0)
                        {
                            var title = textWithoutHtmlTags.Remove(ii).Trim();
                            return title;
                        }
                    }
                    else if (property.Contains("Nr. pagini:"))
                    {
                        textWithoutHtmlTags = textWithoutHtmlTags.Replace("pagini", string.Empty);
                    }

                    return textWithoutHtmlTags.Trim();
                }
            }

            return null;
        }

        public static string GetBookUrlFromSearchLibrarieNet(string page)
        {
            var splited = page.IndexOf("product_grid_text_top");
            var splited2 = page.Remove(0, splited + 40);
            var splited3 = splited2.IndexOf("href=\"http://www.librarie.net/p");
            var splited4 = splited2.Remove(0, splited3 + 6);
            var splited5 = splited4.IndexOf("\">");
            if (splited5 >= 0)
            {
                var splited6 = splited4.Remove(splited5);
                return splited6;
            }
            else
            {
                return null;
            }
        }
    }

        #endregion

    public static class StringExtensions
    {
        public static int? ToNullableInt(this string input)
        {
            int i;
            if (int.TryParse(input, out i)) return i;
            return null;
        }
    }
}
