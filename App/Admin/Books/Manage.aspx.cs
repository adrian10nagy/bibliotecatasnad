
namespace Admin.Books
{
    using Admin.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Linq;
    using BL.Constants;

    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["message"]) && Request["message"] == "BookUpdatedSuccess")
            {
                lblMessage.Text = FlowMessages.BookUpdatedSuccess;
                lblMessage.CssClass = "SuccessBox";
            }

            if (!Page.IsPostBack)
            {
                InitializeUsersTable();
            }
        }

        private void InitializeUsersTable()
        {
            var user = Session[SessionConstants.LoginUser] as User;

            var books = BooksManager.GetAllBooks(user.Library.Id);

            foreach (Book book in books)
            {
                TableRow row = new TableRow();
                HyperLink link = new HyperLink
                {
                    NavigateUrl = "~/Loans/Search.aspx?bookId=" + book.Id,
                    CssClass = "toClickOn",
                    Text = "Împrumuturi"
                };
                TableCell bookId = new TableCell();
                bookId.Controls.Add(link);
                row.Cells.Add(bookId);

                HyperLink linkEditUser = new HyperLink
                {
                    NavigateUrl = "~/Books/Add.aspx?bookId=" + book.Id,
                    CssClass = "toClickOn",
                    Text = "Editează"
                };
                TableCell userEditCell = new TableCell();
                userEditCell.Controls.Add(linkEditUser);
                row.Cells.Add(userEditCell);

                TableCell bookStatus = new TableCell
                {
                    Text = book.BookStatus.ToString()
                };
                row.Cells.Add(bookStatus);

                TableCell bookInternalNr = new TableCell
                {
                    Text = book.InternalNr
                };
                row.Cells.Add(bookInternalNr);

                var bookIsbns = book.ISBNs.Select(b => b.Value);
                TableCell bookIsbn = new TableCell
                {
                    Text = string.Join(", ", bookIsbns)
                };
                row.Cells.Add(bookIsbn);

                TableCell bookTitle = new TableCell
                {
                    Text = book.Title
                };
                row.Cells.Add(bookTitle);

                TableCell bookPublishYear = new TableCell
                {
                    Text = (book.PublishYear != null) ? book.PublishYear.ToString() : string.Empty
                };
                row.Cells.Add(bookPublishYear);

                string authors = "Fără autor(i)";
                if(book.Authors.Count >= 1)
                {
                    authors = book.Authors[0].Name;
                }

                if(book.Authors.Count > 1)
                {
                    var count = book.Authors.Count() - 1;
                    authors = authors + " + " + count; 
                }

                TableCell bookAuthors = new TableCell
                {
                    Text = authors
                };
                row.Cells.Add(bookAuthors);

                TableCell bookNrPages = new TableCell
                {
                    Text = book.NrPages.ToString()
                };
                row.Cells.Add(bookNrPages);

                TableCell bookPublisher = new TableCell
                {
                    Text = book.Publisher.Name
                };
                row.Cells.Add(bookPublisher);

                TableCell bookVolume = new TableCell
                {
                    Text = book.Volume
                };
                row.Cells.Add(bookVolume);

                TableCell bookCondition = new TableCell
                {
                    Text = book.BookCondition.ToString()
                };
                row.Cells.Add(bookCondition);

                TableCell bookFormat = new TableCell
                {
                    Text = book.BookFormat.ToString()
                };
                row.Cells.Add(bookFormat);

                TableCell userLanguage = new TableCell
                {
                    Text = book.BookLanguage.ToString()
                };
                row.Cells.Add(userLanguage);

                datatableResponsive.Rows.Add(row);
            }
        }
    }
}