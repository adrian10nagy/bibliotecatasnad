
namespace Admin.Books
{
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;

    public partial class Add : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod]
        public static string AsyncUpdateBookAuthorsSearch(string name)
        {
            StringBuilder sb = new StringBuilder();
            var authors = AuthorsManager.GetAllAuthorsByInput(name);
            sb.Append(string.Format("<div class='searchMainSuggestions'>{0} rezultate pentru <b>{1}</b></br>", authors.Count(), name));
            if (authors.Count() > 0)
            {
                foreach (var item in authors)
                {
                    var divStructure = "<div class='searchMainItem' onclick = \"BookAuthorAddtoSuggestions('{0}', '{1}') \">";
                    sb.Append(string.Format(divStructure, item.Id, item.Name));
                    sb.Append(item.Name);
                    sb.Append("</div>");
                }
            }
            else
            {
                var divStructure = "<div id='bookAuthorsAddNewAuthor' onclick = \"bookAuthorsAddNewAuthor('{0}') \">Adaugă autorul <b>{0}</b></div>";
                sb.Append(string.Format(divStructure, name));
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string AsyncUpdatePublishersSearch(string name)
        {
            var publishers = PublishersManager.GetAllPublishersByInput(name);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class='searchMainSuggestions'>{0} rezultate pentru <b>{1}</b></br>", publishers.Count(), name));
            if (publishers.Count() > 0)
            {
                foreach (var item in publishers)
                {
                    var divStructure = "<div class='searchMainItem' onclick = \"BookPublisherAddToSuggestions('{0}', '{1}') \">";
                    sb.Append(string.Format(divStructure, item.Id, item.Name));
                    sb.Append(item.Name);
                    sb.Append("</div>");
                }
            }
            else
            {
                var divStructure = "<div class='bookAddNew' onclick = \"bookAddNewPublisher('{0}') \">Adaugă Editura <b>{0}</b></div>";
                sb.Append(string.Format(divStructure, name));
            }
            sb.Append("</div>");
            return sb.ToString();
        }
        
        [System.Web.Services.WebMethod]
        public static string AsyncUpdateDomainSearch(string name)
        {
            var bookDomains = BookDomainsManager.GetAllBookDomainsByInput(name);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class='searchMainSuggestions'>{0} rezultate pentru <b>{1}</b></br>", bookDomains.Count(), name));
            if (bookDomains.Count() > 0)
            {
                foreach (var item in bookDomains)
                {
                    var divStructure = "<div class='searchMainItem' onclick = \"BookDomainAddToSuggestions('{0}', '{1}') \">";
                    sb.Append(string.Format(divStructure, item.Id, item.Name));
                    sb.Append(item.Name);
                    sb.Append("</div>");
                }
            }
            else
            {
                var divStructure = "<div class='bookAddNew' onclick = \"bookAddNewBookDomain('{0}') \">Adaugă domeniul <b>{0}</b></div>";
                sb.Append(string.Format(divStructure, name));
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        [System.Web.Services.WebMethod]
        public static Author AsyncAddAuthor(string name)
        {
            Author author = AuthorsManager.Add(name);

            return author;
        }

        [System.Web.Services.WebMethod]
        public static Publisher AsyncAddPublisher(string name)
        {
            Publisher publisher = PublishersManager.Add(name);

            return publisher;
        }

         [System.Web.Services.WebMethod]
        public static BookDomain AsyncAddDomain(string name)
        {
            BookDomain bookDomain = BookDomainsManager.Add(name);

            return bookDomain;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeSources();
        }

        private void InitializeSources()
        {
            if (!this.Page.IsPostBack)
            {
                this.InitializeLanguages();
                this.InitializeCondition();
                this.InitializeBookFormat();
            }
        }

        private void InitializeCondition()
        {
            var bookConditions = EnumUtil.GetValues<BookCondition>();

            var listItemBookConditions = bookConditions.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookCondition.Items.AddRange(listItemBookConditions);
            drpBookCondition.DataBind();
        }

        private void InitializeBookFormat()
        {
            var bookFrmats = EnumUtil.GetValues<BookFormat>().ToList();
            bookFrmats = bookFrmats.Swap(0, 1).ToList();
            var listItemBookFormats = bookFrmats.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookFormat.Items.AddRange(listItemBookFormats);
            drpBookFormat.DataBind();
        }

        private void InitializeLanguages()
        {
            var languages = EnumUtil.GetValues<Language>().ToList();
            languages = languages.Swap(0, 1).ToList();
            var listItemLanguages = languages.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookLanguage.Items.AddRange(listItemLanguages);
            drpBookLanguage.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var book = new Book
            {
                Title = txtBookTitle.Text,
                PublishYear = txtBookPublishYear.Text.ToInt(),
                Volume = txtBookVolume.Text,
                ISBN = txtBookIsbn.Text,
                InternalNr = txtBookInternalNr.Text,
                NrPages = txtBookNrPages.Text.ToInt(),
                AddedDate = DateTime.Now,
                AddedBy = new User { Id = 1 },
                Publisher = PublishersManager.GetAllPublishersByInput(txtBookPublisher.Value).First(),
                BookCondition = (BookCondition)drpBookCondition.SelectedValue.ToInt(),
                Library = new Library { Id = 1 },
                BookFormat = (BookFormat)drpBookFormat.SelectedValue.ToInt(),
                BookDomain = BookDomainsManager.GetAllBookDomainsByInput(txtBookDomain.Value).First(),
                BookSubject = new BookSubject {
                   Name = txtBookSubject.Value 
                },
                BookLanguage = (Language)drpBookLanguage.SelectedValue.ToInt(),
                Authors = this.GetAuthors()
            };

            BooksManager.AddBook(book);
            lblStatus.Text = BookConstants.AddSuccess;
            lblStatus.CssClass = "SuccessBox";
            CleanFields();
        }

        private void CleanFields()
        {
            txtBookTitle.Text = string.Empty;
            txtBookPublishYear.Text = string.Empty;
            txtBookVolume.Text = string.Empty;
            txtBookIsbn.Text = string.Empty;
            txtBookInternalNr.Text = string.Empty;
            txtBookNrPages.Text = string.Empty;
        }

        private List<Author> GetAuthors()
        {
            var authors = new List<Author>();
            foreach (ListItem item in bltBookAuthorsSelected.Items)
            {
                var author = new Author
                {
                    Name = item.Text,
                    Id = item.Value.ToInt()
                };
                authors.Add(author);
            }

            return authors;
        }

        private ListItem[] TransformAuthorsToListItem(IEnumerable<Author> authors)
        {
            var listItems = authors.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToArray();

            return listItems;
        }

        protected void btnBookAuthorsAdd_Click(object sender, EventArgs e)
        {
            txtBookAuthorsSearch.Visible = true;
            btnBookauthorAddSelected.Visible = true;
        }

        protected void btnBookauthorAddSelected_Click(object sender, EventArgs e)
        {
            bltBookAuthorsSelected.Items.Add(new ListItem
            {
                Text = txtBookAuthorsSearch.Value
            });

            lblBookAuthorsStatus.Text = "Autori adăugați:" + bltBookAuthorsSelected.Items.Count.ToString();
            txtBookAuthorsSearch.Value = string.Empty;
            txtBookAuthorsSearch.Visible = false;
            btnBookauthorAddSelected.Visible = false;
        }
    }
}