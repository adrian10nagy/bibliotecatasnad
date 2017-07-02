
namespace Admin.Books
{
    using Helpers.Constants;
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using BL.Services;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Diagnostics;

    public partial class Add : BooksBase
    {
        private const string DoesNotExist = "Nu există? Click aici";
        private const string ExistGoBack = "Revino la lista inițială";

        #region WebMethods

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

        #endregion

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
                        txtBookSubject.Text = book.BookSubject.Name;
                        drpBookLanguage.SelectedValue = ((int)book.BookLanguage).ToString();
                        txtBookDescription.InnerText = book.Description;
                        txtBookImage.Text = book.ImageUrl;
                        txtBookPreviewLink.Text = book.PreviewLink;
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
                        }
                        else if (book.ISBNs.Count == 1)
                        {
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

        protected void btnBookPublisherAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookPublisherAddNew.Text.Trim() == string.Empty)
            {
                txtBookPublisherAddNew.CssClass = "requiredFieldError";
            }
            else
            {
                var existentPublisher = PublishersManager.GetOnePublisherByInput(txtBookPublisherAddNew.Text);
                var listItem = new ListItem
                {
                    Text = txtBookPublisherAddNew.Text
                };

                if (existentPublisher != null)
                {
                    listItem.Value = existentPublisher.Id.ToString();
                }
                else
                {
                    var publisher = PublishersManager.Add(txtBookPublisherAddNew.Text);
                    listItem.Value = publisher.Id.ToString();
                }

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
            lblBookIsbn.Text = "test 1";
            if (txtBookIsbn.Text.Trim() != string.Empty)
            {
                bltBookIsbnSelected.Items.Add(new ListItem
                {
                    Text = txtBookIsbn.Text,
                });
                txtBookIsbn.Text = string.Empty;
                txtBookIsbn.CssClass = txtBookIsbn.CssClass.Replace("requiredFieldError", "");
                btnBookIsbnRemove.Visible = true;
            }
            else
            {
                txtBookIsbn.CssClass += " requiredFieldError";
            }
        }

        protected void btnBookIsbnRemove_Click(object sender, EventArgs e)
        {
            btnBookIsbnRemove.Visible = false;
            bltBookIsbnSelected.Items.Clear();
        }

        protected void txtBookInternalNr_TextChanged(object sender, EventArgs e)
        {
            lblBookInternalNr.Text = "Număr de inventar*";
            lblBookInternalNr.Attributes.CssStyle.Remove("color");
            if (!string.IsNullOrEmpty(txtBookInternalNr.Text))
            {
                if (ValidateInternalNr(txtBookInternalNr.Text))
                {
                    lblBookInternalNr.Text = "Număr de inventar*- Numărul de inventar există deja!";
                    lblBookInternalNr.Attributes.CssStyle.Add("color", "red");
                }
            }
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvBookSuggestions, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click pentru a aduce în editare";
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            gvBookSuggestions.Visible = false;
            ChangeBookAuthorAddNewVisibility(true);
            ChangeBookPublisherAddNewVisibility(true);
            bltBooksAuthorsSelected.Items.Clear();
            drdBooksAutors.SelectedIndex = 0;
            drdBooksAutors.DataBind();
            btnBookAuthorsRemove.Visible = false;
            lnkIsbnSuggestionsRemove.Visible = false;
            lnkTitleSuggestionsRemove.Visible = false;
            lblBooksSuggestions.Visible = false;

            foreach (GridViewRow row in gvBookSuggestions.Rows)
            {
                if (row.RowIndex == gvBookSuggestions.SelectedIndex)
                {
                    txtBookTitle.Text = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[1].Text);
                    txtBookPublisherAddNew.Text = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[2].Text);
                    txtBookAuthorsAddNew.Text = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[3].Text);
                    txtBookPublishYear.Text = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[4].Text);
                    txtBookNrPages.Text = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[5].Text);
                    var language = Server.HtmlDecode(gvBookSuggestions.Rows[row.RowIndex].Cells[6].Text);
                    if (drpBookLanguage.Items.FindByText(language) != null)
                    {
                        var selectedValue = drpBookLanguage.Items.FindByText(language);
                        drpBookLanguage.SelectedValue = selectedValue.Value;
                    }
                    HiddenField bookDescriereFull = row.Cells[0].FindControl("DescriereFull") as HiddenField;
                    txtBookDescription.InnerText = bookDescriereFull != null ? bookDescriereFull.Value : string.Empty;

                    HiddenField bookImageUrl = row.Cells[0].FindControl("ImageUrl") as HiddenField;
                    txtBookImage.Text = bookImageUrl != null ? bookImageUrl.Value : string.Empty;

                    HiddenField bookPreviewlink = row.Cells[0].FindControl("PreviewLink") as HiddenField;
                    txtBookPreviewLink.Text = bookPreviewlink != null ? bookPreviewlink.Value : string.Empty;

                    break;
                }
            }
        }

        protected void lnkIsbnLookUp_Click(object sender, EventArgs e)
        {
            lnkIsbnSuggestionsRemove.Visible = true;
            var isbns = GetIsbns();
            List<Book> books = new List<Book>();
            foreach (var item in isbns)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    var book = GoogleBookWarehouseService.GetBookByIsbn(item.Value);

                    if (book != null && book.Count() != 0 && book[0] != null)
                    {
                        books.AddRange(book);
                    }
                }
            }

            lblBooksSuggestions.Text = books.Count().ToString() + " sugestii";
            lblBooksSuggestions.Visible = true;

            DataTable dt = MapBooksToDataTable(books);
            BindDataBookPanel(dt);
        }

        protected void lnkIsbnSuggestionsRemove_Click(object sender, EventArgs e)
        {
            gvBookSuggestions.Visible = false;
            lnkIsbnSuggestionsRemove.Visible = false;
            lnkTitleSuggestionsRemove.Visible = false;
            lblBooksSuggestions.Visible = false;
        }

        protected void lnkTitleLookUp_Click(object sender, EventArgs e)
        {
            lnkTitleSuggestionsRemove.Visible = true;
            List<Book> books = new List<Book>();
            if (!string.IsNullOrEmpty(txtBookTitle.Text))
            {
                var book = GoogleBookWarehouseService.GetBookByTitle(txtBookTitle.Text);
                if (book != null && book.Count() != 0 && book[0] != null)
                {
                    books.AddRange(book);
                }
            }

            DataTable dt = MapBooksToDataTable(books);
            BindDataBookPanel(dt);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;

            if (!this.ValidateData(user))
            {
                lblStatus.Visible = false;
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
                AddedBy = new User { Id = user.Id },
                Publisher = PublishersManager.GetPublishersById(drdBookPublisher.SelectedValue.ToNullableInt().Value),
                BookCondition = (BookCondition)drpBookCondition.SelectedValue.ToNullableInt(),
                Library = new Library { Id = user.Library.Id },
                BookFormat = (BookFormat)drpBookFormat.SelectedValue.ToNullableInt(),
                BookDomain = BookDomainsManager.GetAllBookDomainsById(drdBookDomain.SelectedValue.ToNullableInt().Value),
                BookSubject = new BookSubject
                {
                    Name = txtBookSubject.Text,
                    Id = 1
                },
                BookLanguage = (Language)drpBookLanguage.SelectedValue.ToNullableInt(),
                Authors = this.GetAuthors(),
                ISBNs = this.GetIsbns(),
                PreviewLink = txtBookPreviewLink.Text,
                ImageUrl = txtBookImage.Text,
                Description = txtBookDescription.InnerText
            };

            if (!string.IsNullOrEmpty(lblBookId.Text) && lblBookId.Text.ToNullableInt() != null && lblBookId.Text.ToNullableInt() != 0)
            {
                book.Id = lblBookId.Text.ToNullableInt().Value;
                BooksManager.Update(book, user.Library.Id);
                Response.Redirect("~/Books/Manage.aspx?Message=BookUpdatedSuccess");
            }
            else
            {
                if (ValidateInternalNr(txtBookInternalNr.Text))
                {
                    lblBookInternalNr.Text = "Număr de inventar*- Numărul de inventar există deja!";
                    lblBookInternalNr.Attributes.CssStyle.Add("color", "red");
                    return;
                }

                BooksManager.AddBook(book, user.Library.Id);
                int bookNrByAddedUser = BooksManager.GetBooksNrByAddedUser(user.Id, user.Library.Id);
                lblStatus.Text = string.Format(BookConstants.AddSuccess, bookNrByAddedUser);
                lblStatus.CssClass = "SuccessBox";
                lblStatus.Visible = true;
                CleanFields();
                InitializeSources();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Books/Manage.aspx");
        }

        protected void drdBooksAutors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drdBooksAutors.SelectedValue.ToNullableInt() != 0)
            {
                var author = AuthorsManager.GetById(drdBooksAutors.SelectedValue.ToNullableInt().Value);
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
            ChangeBookAuthorAddNewVisibility();
        }

        private void ChangeBookAuthorAddNewVisibility(bool addNewVisible = false)
        {
            if (addNewVisible)
            {
                lnkBookAuthorsAddNew.Text = ExistGoBack;
                drdBooksAutors.Visible = false;
                txtBookAuthorsAddNew.Visible = true;
                btnBookAuthorAddNew.Visible = true;

                return;
            }

            if (txtBookAuthorsAddNew.Visible == false)
            {
                lnkBookAuthorsAddNew.Text = ExistGoBack;
                drdBooksAutors.Visible = false;
                txtBookAuthorsAddNew.Visible = true;
                btnBookAuthorAddNew.Visible = true;
            }
            else
            {
                lnkBookAuthorsAddNew.Text = DoesNotExist;
                drdBooksAutors.Visible = true;
                txtBookAuthorsAddNew.Visible = false;
                btnBookAuthorAddNew.Visible = false;
            }
        }

        protected void btnBookAuthorAddNew_Click(object sender, EventArgs e)
        {
            if (txtBookAuthorsAddNew.Text.Trim() == string.Empty)
            {
                txtBookAuthorsAddNew.CssClass = "";
            }
            else
            {
                var existentAuthor = AuthorsManager.GetOneAuthorByInput(txtBookAuthorsAddNew.Text);
                var listItem = new ListItem
                {
                    Text = txtBookAuthorsAddNew.Text
                };

                if (existentAuthor != null)
                {
                    listItem.Value = existentAuthor.Id.ToString();
                }
                else
                {
                    var author = AuthorsManager.Add(txtBookAuthorsAddNew.Text);
                    listItem.Value = author.Id.ToString();
                }

                bltBooksAuthorsSelected.Items.Add(listItem);
                txtBookAuthorsAddNew.Visible = false;
                btnBookAuthorAddNew.Visible = false;
                drdBooksAutors.Visible = true;
                drdBooksAutors.SelectedIndex = 0;
                btnBookAuthorsRemove.Visible = true;
                lnkBookAuthorsAddNew.Text = DoesNotExist;
            }
        }

        protected void lnkBookPublisherAddNew_Click(object sender, EventArgs e)
        {
            ChangeBookPublisherAddNewVisibility();
        }

        private void ChangeBookPublisherAddNewVisibility(bool addNewVisible = false)
        {
            if (addNewVisible)
            {
                lnkBookPublisherAddNew.Text = ExistGoBack;
                txtBookPublisherAddNew.Visible = true;
                btnBookPublisherAddNew.Visible = true;
                drdBookPublisher.Visible = false;

                return;
            }

            if (txtBookPublisherAddNew.Visible == false)
            {
                lnkBookPublisherAddNew.Text = ExistGoBack;
                txtBookPublisherAddNew.Visible = true;
                btnBookPublisherAddNew.Visible = true;
                drdBookPublisher.Visible = false;
            }
            else
            {
                lnkBookPublisherAddNew.Text = DoesNotExist;
                txtBookPublisherAddNew.Visible = false;
                btnBookPublisherAddNew.Visible = false;
                drdBookPublisher.Visible = true;
            }
        }

        private void InitializeSources()
        {
            this.InitializeLanguages();
            this.InitializeBookCondition();
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

            drdBooksAutors.Items.Clear();
            drdBooksAutors.Items.Add(new ListItem());
            drdBooksAutors.Items.AddRange(listItemAuthors);
            drdBooksAutors.DataBind();
        }

        private void InitializeBookCondition()
        {
            var bookConditions = EnumUtil.GetValues<BookCondition>();

            var listItemBookConditions = bookConditions.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookCondition.Items.Clear();
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

            drpBookFormat.Items.Clear();
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

            drdBookPublisher.Items.Clear();
            drdBookPublisher.Items.Add(new ListItem());
            drdBookPublisher.Items.AddRange(listItemPublishers);
            drdBookPublisher.DataBind();
        }

        private void InitializeDomains()
        {
            var domains = BookDomainsManager.GetAllBookDomains();

            var listItemDomains = domains.Select(c => new ListItem
            {
                Text = "[" + c.CZU + "] " + c.Name,
                Value = c.Id.ToString(),
            }).ToArray();

            drdBookDomain.Items.Clear();
            drdBookDomain.Items.Add(new ListItem());
            drdBookDomain.Items.AddRange(listItemDomains);
            drdBookDomain.DataBind();
        }

        private void InitializeLanguages()
        {
            var languages = EnumUtil.GetValues<Language>().ToList();
            var listItemLanguages = languages.Select(c => new ListItem
            {
                Text = c.ToString(),
                Value = ((int)c).ToString()
            }).ToArray();

            drpBookLanguage.Items.Clear();
            drpBookLanguage.Items.AddRange(listItemLanguages);
            drpBookLanguage.DataBind();
        }

        private bool ValidateData(User user)
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

            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                lblBookTitle.Attributes.CssStyle.Add("color", "red");
                isValid = false;
            }


            if (string.IsNullOrWhiteSpace(txtBookInternalNr.Text))
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

            if (user == null || user.Id == 0)
            {
                isValid = false;
                Response.Redirect("~/Account/Login.aspx?Message=Timeout");
            }

            return isValid;
        }

        private List<ISBN> GetIsbns()
        {
            var isbns = new List<ISBN>();
            if (btnBookIsbnAddNew.Visible == true)
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
            // clear fields
            txtBookTitle.Text = string.Empty;
            txtBookPublishYear.Text = string.Empty;
            txtBookVolume.Text = string.Empty;
            txtBookIsbn.Text = string.Empty;
            txtBookInternalNr.Text = string.Empty;
            txtBookNrPages.Text = string.Empty;
            lnkBookAuthorsAddNew.Text = DoesNotExist;
            lblBooksSuggestions.Text = string.Empty;
            txtBookImage.Text = string.Empty;
            txtBookPreviewLink.Text = string.Empty;
            txtBookDescription.InnerText = string.Empty;
            txtBookAuthorsAddNew.Text = string.Empty;
            txtBookPublisherAddNew.Text = string.Empty;
            bltBooksAuthorsSelected.Items.Clear();
            drdBookDomain.SelectedIndex = 0;
            drdBookPublisher.SelectedIndex = 0;

            // ajust visibility
            drdBookPublisher.Visible = true;
            txtBookPublisherAddNew.Visible = false;
            lnkBookPublisherAddNew.Text = DoesNotExist;
            txtBookAuthorsAddNew.Visible = false;
            drdBooksAutors.Visible = true;

            btnBookAuthorsRemove.Visible = false;
            btnBookAuthorAddNew.Visible = false;
            btnBookPublisherAddNew.Visible = false;
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

        bool ValidateInternalNr(string internalNr)
        {
            var result = true;
            var user = Session[SessionConstants.LoginUser] as User;

            var books = BooksManager.GetAllBooks(user.Library.Id);
            var bookWithSameInternalNr = books.Where(b => b.InternalNr.ToLower() == internalNr.ToLower()).ToList();

            if (bookWithSameInternalNr != null && bookWithSameInternalNr.Count() < 1)
            {
                result = false;
            }

            return result;
        }

        private void BindDataBookPanel(DataTable dt)
        {
            gvBookSuggestions.DataSource = dt;
            gvBookSuggestions.DataBind();
            gvBookSuggestions.Visible = true;
        }

        private DataTable MapBooksToDataTable(List<Book> books)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[10] 
            { 
                new DataColumn("Title"), 
                new DataColumn("Editura"), 
                new DataColumn("Autor(i)"), 
                new DataColumn("An Apariție"), 
                new DataColumn("Nr. pagini"), 
                new DataColumn("Limbă"), 
                new DataColumn("DescrierePreview"),
                new DataColumn("DescriereFull"),
                new DataColumn("ImageUrl"),
                new DataColumn("PreviewLink")
            });


            foreach (Book book in books)
            {
                // authors
                string authors = "Fără autor(i)";
                if (book.Authors.Count >= 1)
                {
                    authors = book.Authors[0].Name;
                }

                // book description
                var bookDescriptionPreview = book.Description != null ? book.Description.Substring(0, Math.Min(book.Description.Length, 250)) + "..." : string.Empty;

                // book publisher
                book.Publisher = new Publisher
                {
                    Name = (book.Publisher != null) ? book.Publisher.Name : string.Empty
                };

                DataRow dr = dt.NewRow();
                DataColumn dcol = new DataColumn();
                dr["Title"] = book.Title;
                dr["Editura"] = book.Publisher.Name;
                dr["Autor(i)"] = authors;
                dr["An Apariție"] = book.PublishYear;
                dr["Nr. pagini"] = book.NrPages;
                dr["Limbă"] = book.BookLanguage;
                dr["DescrierePreview"] = bookDescriptionPreview;
                dr["DescriereFull"] = book.Description;
                dr["ImageUrl"] = book.ImageUrl;
                dr["PreviewLink"] = book.PreviewLink;

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}