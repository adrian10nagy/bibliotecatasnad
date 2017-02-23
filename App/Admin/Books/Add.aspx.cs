
namespace Admin.Books
{
    using Admin.Helpers;
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
            if (!this.Page.IsPostBack)
            {
                InitializeSources();
                btnBookAuthorsRemove.Visible = false;

                if (!string.IsNullOrEmpty(Request["bookId"]) && Request["bookId"].ToNullableInt() != null)
                {
                    var book = BooksManager.GetBookById(Request["bookId"].ToNullableInt().Value);
                    if (book != null)
                    {
                        txtBookTitle.Text = book.Title;
                        txtBookPublishYear.Text = (book.PublishYear != null) ? book.PublishYear.ToString() : string.Empty;
                        txtBookVolume.Text = book.Volume;
                        txtBookInternalNr.Text = book.InternalNr;
                        txtBookNrPages.Text = book.NrPages.ToString();
                        drdBookPublisher.SelectedValue = book.Publisher.Id.ToString();
                        drpBookCondition.SelectedValue = ((int)book.BookCondition).ToString();
                        drpBookFormat.SelectedValue = ((int)book.BookFormat).ToString();
                        drdBookDomain.SelectedValue = book.BookDomain.Id.ToString();
                        txtBookSubject.Value = book.BookSubject.Name;
                        drpBookLanguage.SelectedValue = ((int)book.BookLanguage).ToString();
                        foreach (var item in book.Authors)
                        {
                            bltBooksAuthorsSelected.Items.Add(new ListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                            btnBookAuthorsRemove.Visible = true;
                        }
                        if (book.ISBNs.Count > 1)
                        {
                            foreach (var item in book.ISBNs)
                            {
                                bltBookIsbnSelected.Items.Add(new ListItem
                                {
                                    Text = item.Value,
                                    Value = item.Id.ToString()
                                });
                            }
                            bltBookIsbnSelected.Visible = true;
                            btnBookIsbnRemove.Visible = true;
                            btnBookIsbnAddNew.Visible = true;
                        }else if(book.ISBNs.Count == 1){
                            txtBookIsbn.Text = book.ISBNs[0].Value;
                        }

                        lblBookId.Text = Request["bookId"];
                        btnSubmit.Text = "Modifică datele cărții";
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?errorId=" + ErrorMessages.BookInvalid);
                    }
                }
            }
        }

        private void InitializeSources()
        {
            this.InitializeLanguages();
            this.InitializeCondition();
            this.InitializeBookFormat();
            this.InitializePublishers();
            this.InitializeDomains();
            this.InitializeAuthors();   
        }

        private void InitializeAuthors()
        {
            var authors = AuthorsManager.GetAllAuthors();

            var listItemAuthors = authors.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToArray();

            drdBooksAutors.Items.AddRange(listItemAuthors);
            drdBooksAutors.DataBind();
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

        private void InitializePublishers()
        {
            var publishers = PublishersManager.GetAllPublishers();

            var listItemPublishers = publishers.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToArray();

            drdBookPublisher.Items.AddRange(listItemPublishers);
            drdBookPublisher.DataBind();
        }
        
        private void InitializeDomains()
        {
            var domains = BookDomainsManager.GetAllBookDomains();

            var listItemDomains = domains.Select(c => new ListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToArray();

            drdBookDomain.Items.AddRange(listItemDomains);
            drdBookDomain.DataBind();
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
            if(!this.ValidateData())
            {
                return;
            }

            var book = new Book
            {
                Title = txtBookTitle.Text,
                PublishYear = txtBookPublishYear.Text.ToNullableInt(),
                Volume = txtBookVolume.Text,
                InternalNr = txtBookInternalNr.Text,
                NrPages = txtBookNrPages.Text.ToNullableInt().Value,
                AddedDate = DateTime.Now,
                AddedBy = new User { Id = 1 },
                Publisher = PublishersManager.GetAllPublishersById(drdBookPublisher.SelectedValue.ToNullableInt().Value),
                BookCondition = (BookCondition)drpBookCondition.SelectedValue.ToNullableInt(),
                Library = new Library { Id = 1 },
                BookFormat = (BookFormat)drpBookFormat.SelectedValue.ToNullableInt(),
                BookDomain = BookDomainsManager.GetAllBookDomainsById(drdBookDomain.SelectedValue.ToNullableInt().Value),
                BookSubject = new BookSubject {
                   Name = txtBookSubject.Value,
                   Id = 1
                },
                BookLanguage = (Language)drpBookLanguage.SelectedValue.ToNullableInt(),
                Authors = this.GetAuthors(),
                ISBNs = this.GetIsbns()
            };

            if (!string.IsNullOrEmpty(lblBookId.Text) && lblBookId.Text.ToNullableInt() != 0)
            {
                book.Id = lblBookId.Text.ToNullableInt().Value;
                BooksManager.Update(book);
                Response.Redirect("~/Books/Manage.aspx?Message=BookUpdatedSuccess");
            }
            else
            {
                BooksManager.AddBook(book);
                lblStatus.Text = BookConstants.AddSuccess;
                lblStatus.CssClass = "SuccessBox";
                CleanFields();
            }
        }

        private bool ValidateData()
        {
            lblBookPublishYear.Attributes.CssStyle.Remove("color");
            lblBookTitle.Attributes.CssStyle.Remove("color");
            lblBookInternalNr.Attributes.CssStyle.Remove("color");
            lblBookDomain.Attributes.CssStyle.Remove("color");
            lblBookPublisher.Attributes.CssStyle.Remove("color");
            lblBooksAutors.Attributes.CssStyle.Remove("color");
            lblBookNrPages.Attributes.CssStyle.Remove("color");

            bool isValid = true;

            if (txtBookNrPages.Text.ToNullableInt() == null || txtBookNrPages.Text.ToNullableInt().Value == 0)
            {
                lblBookNrPages.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            if(string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                lblBookTitle.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            
            if(string.IsNullOrWhiteSpace(txtBookInternalNr.Text))
            {
                lblBookInternalNr.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            if (drdBookDomain.SelectedIndex < 1)
            {
                lblBookDomain.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            if (drdBookPublisher.SelectedIndex < 1)
            {
                lblBookPublisher.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            if (bltBooksAuthorsSelected.Items.Count < 1)
            {
                lblBooksAutors.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }

            return isValid;
        }

        private List<ISBN> GetIsbns()
        {
            var isbns = new List<ISBN>();
            if (btnBookIsbnAddNew.Visible == false)
            {
                foreach (ListItem item in bltBookIsbnSelected.Items)
                {
                    var author = new ISBN
                    {
                        Value = item.Text,
                    };
                    isbns.Add(author);
                }
            }
            else
            {
                var author = new ISBN
                {
                    Value = txtBookIsbn.Text,
                };
                isbns.Add(author);
            }
            return isbns;
        }

        private void CleanFields()
        {
            txtBookTitle.Text = string.Empty;
            txtBookPublishYear.Text = string.Empty;
            txtBookVolume.Text = string.Empty;
            txtBookIsbn.Text = string.Empty;
            txtBookInternalNr.Text = string.Empty;
            txtBookNrPages.Text = string.Empty;
            bltBooksAuthorsSelected.Items.Clear();
            drdBookDomain.SelectedIndex = 0;
            drdBookPublisher.SelectedIndex = 0;
            btnBookAuthorsRemove.Visible = false;
        }

        private List<Author> GetAuthors()
        {
            var authors = new List<Author>();
            foreach (ListItem item in bltBooksAuthorsSelected.Items)
            {
                var author = new Author
                {
                    Name = item.Text,
                    Id = item.Value.ToNullableInt().Value
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Manage.aspx");
        }

        protected void drdBooksAutors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drdBooksAutors.SelectedValue.ToNullableInt() != 0)
            {
                var author = AuthorsManager.GetAllById(drdBooksAutors.SelectedValue.ToNullableInt().Value);
                if (author != null)
                {
                    var listItem = new ListItem
                    {
                        Text = author.Name,
                        Value = author.Id.ToString()
                    };

                    bltBooksAuthorsSelected.Items.Add(listItem);
                    bltBooksAuthorsSelected.DataBind();
                    foreach (ListItem item in bltBooksAuthorsSelected.Items)
                    {
                        drdBooksAutors.Items.FindByValue(item.Value).Attributes.Add("Disabled", "Disabled");
                    }
                }
            }
            drdBooksAutors.SelectedIndex = 0;
            btnBookAuthorsRemove.Visible = true;
        }

        protected void btnBookAuthorsRemove_Click(object sender, EventArgs e)
        {
            bltBooksAuthorsSelected.Items.Clear();
            drdBooksAutors.SelectedIndex = 0;
            drdBooksAutors.DataBind();
            btnBookAuthorsRemove.Visible = false;
        }

        protected void lnkBookAuthorsAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookAuthorsAddNew.Visible == false)
            {
                lnkBookAuthorsAddNew.Text = "Revino la lista inițială";
                drdBooksAutors.Visible = false;
                txtBookAuthorsAddNew.Visible = true;
                btnBookAuthorAddNew.Visible = true;
            }
            else
            {
                lnkBookAuthorsAddNew.Text = "Nu există? Click aici";
                drdBooksAutors.Visible = true;
                txtBookAuthorsAddNew.Visible = false;
                btnBookAuthorAddNew.Visible = false;
            }
        }

        protected void btnBookAuthorAddNew_Click(object sender, EventArgs e)
        {
            if(txtBookAuthorsAddNew.Text.Trim() == string.Empty)
            {
                txtBookAuthorsAddNew.CssClass = "";

            }
            else
            {
                var author = AuthorsManager.Add(txtBookAuthorsAddNew.Text);
                var listItem = new ListItem
                {
                   Text = author.Name,
                   Value = author.Id.ToString()
                };

                bltBooksAuthorsSelected.Items.Add(listItem);
                txtBookAuthorsAddNew.Visible = false;
                btnBookAuthorAddNew.Visible = false;
                drdBooksAutors.Visible = true;
                drdBooksAutors.SelectedIndex = 0;
                btnBookAuthorsRemove.Visible = true;
            }
        }

        protected void lnkBookPublisherAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookPublisherAddNew.Visible == false)
            {
                txtBookPublisherAddNew.Visible = true;
                btnBookPublisherAddNew.Visible = true;
                lnkBookPublisherAddNew.Text = "Revino la lista inițială";
                drdBookPublisher.Visible = false;
            }
            else
            {
                txtBookPublisherAddNew.Visible = false;
                btnBookPublisherAddNew.Visible = false;
                lnkBookPublisherAddNew.Text = "Nu există? Click aici";
                drdBookPublisher.Visible = true;
            }
        }

        protected void btnBookPublisherAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookPublisherAddNew.Text.Trim() == string.Empty)
            {
                txtBookPublisherAddNew.CssClass = "requiredFieldError";
            }
            else
            {
                var publisher = PublishersManager.Add(txtBookPublisherAddNew.Text);
                var listItem = new ListItem
                {
                    Text = publisher.Name,
                    Value = publisher.Id.ToString()
                };

                drdBookPublisher.Visible = true;
                InitializePublishers();
                drdBookPublisher.SelectedValue = listItem.Value;
                txtBookPublisherAddNew.Visible = false;
                btnBookPublisherAddNew.Visible = false;
            }
        }

        protected void lnkBookIsbnAddNew_Click(object sender, EventArgs e)
        {
            if (btnBookIsbnAddNew.Visible == false)
            {
                lnkBookIsbnAddNew.Text = "Un singur ISBN? Click aici";
                btnBookIsbnAddNew.Visible = true;
                bltBookIsbnSelected.Visible = true;
                btnBookIsbnRemove.Visible = true;
                txtBookIsbn.Text = string.Empty;
            }
            else
            {
                lnkBookIsbnAddNew.Text = "Mai multe ISBN-uri? Click aici";
                btnBookIsbnAddNew.Visible = false;
                bltBookIsbnSelected.Visible = false;
                btnBookIsbnRemove.Visible = false;
                bltBookIsbnSelected.Items.Clear();
            }
        }

        protected void btnBookIsbnAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookIsbn.Text.Trim() != string.Empty)
            {
                bltBookIsbnSelected.Items.Add(new ListItem
                {
                    Text = txtBookIsbn.Text,
                });
                txtBookIsbn.Text = string.Empty;
                txtBookIsbn.CssClass = txtBookIsbn.CssClass.Replace("requiredFieldError", "");
            }
            else
            {
                txtBookIsbn.CssClass += " requiredFieldError";
            }
        }

        protected void btnBookIsbnRemove_Click(object sender, EventArgs e)
        {
            bltBookIsbnSelected.Items.Clear();
        }
    }
}