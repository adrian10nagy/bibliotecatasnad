
namespace Admin.Books
{
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeSources();
        }

        private void InitializeSources()
        {
            if (!this.Page.IsPostBack)
            {
                this.InitializeLanguages();
                this.InitializePublisher();
                this.InitializeCondition();
                this.InitializeBookFormat();
                this.InitializeBookDomain();
                this.InitializeBookAuthors();
            }
        }

        private void InitializeBookAuthors()
        {
            var authors = AuthorsManager.GetAllAuthors();

            var listItemAuthors = TransformAuthorsToListItem(authors);

            drpBookAuthors.Items.AddRange(listItemAuthors);
            drpBookAuthors.SelectedIndex = 0;
            drpBookAuthors.DataBind();
        }

        private void InitializeBookDomain()
        {
            var counties = BookDomainsManager.GetAllBookDomains();

            var x = counties.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToArray();

            drpBookDomain.Items.AddRange(x);
            drpBookDomain.DataBind();
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
            var bookFrmats = EnumUtil.GetValues<BookFormat>();

            var listItemBookFormats = bookFrmats.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookFormat.Items.AddRange(listItemBookFormats);
            drpBookFormat.DataBind();
        }

        private void InitializePublisher()
        {
            var publishers = PublishersManager.GetAllPublishers();

            var x = publishers.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToArray();

            drpBookPublisher.Items.AddRange(x);
            drpBookPublisher.DataBind();
        }

        private void InitializeLanguages()
        {
            var languages = EnumUtil.GetValues<Language>();

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
                Publisher = new Publisher { Id = 1 },
                BookCondition = (BookCondition)drpBookCondition.SelectedValue.ToInt(),
                Library = new Library { Id = 1 },
                BookFormat = (BookFormat)drpBookFormat.SelectedValue.ToInt(),
                BookDomain = new BookDomain { Id = 1 },
                BookSubject = new BookSubject { Id = 1 },
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
            var x = drpBookAuthors.Items;

            return authors;
        }

        protected void lnkBookAuthorsAdd_Click(object sender, EventArgs e)
        {
            drpBookAuthors.Visible = true;
        }

        protected void drpBookAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            var bookAuthors = (DropDownList)sender;
            var item = new ListItem
                {
                    Text = bookAuthors.SelectedItem.Text,
                    Value = bookAuthors.SelectedItem.Value,
                };

            bulBookAuthorsItems.Items.Add(item);
            drpBookAuthors.Items.Remove(item);
            drpBookAuthors.Visible = false;
            txtBookAuthorsSearch.Text = string.Empty;
        }

        protected void bulBookAuthorsItems_Click(object sender, BulletedListEventArgs e)
        {
            var bookAuthors = (BulletedList)sender;
            bookAuthors.Items.RemoveAt(e.Index);
        }

        protected void txtBookAuthorsSearch_TextChanged(object sender, EventArgs e)
        {
            drpBookAuthors.Visible = true;
            var authors = AuthorsManager.GetAllAuthorsByInput(txtBookAuthorsSearch.Text); //
            var listItemAuthors = TransformAuthorsToListItem(authors);
            drpBookAuthors.Items.Clear();
            drpBookAuthors.SelectedIndex = 0;
            drpBookAuthors.Items.AddRange(listItemAuthors);
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
    }
}