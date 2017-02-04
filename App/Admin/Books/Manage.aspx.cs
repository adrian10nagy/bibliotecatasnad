
namespace Admin.Books
{
    using Admin.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

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
            var books = BooksManager.GetAllBooks();

            foreach (Book book in books)
            {
                TableRow row = new TableRow();
                HyperLink link = new HyperLink
                {
                    NavigateUrl = "~/Loans/ByBook.aspx?bookId=" + book.Id,
                    CssClass = "toClickOn",
                    Text = "Vezi împrumuturi"
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

                TableCell bookTitle = new TableCell
                {
                    Text = book.Title
                };
                row.Cells.Add(bookTitle);

                TableCell bookPublishYear = new TableCell
                {
                    Text = book.PublishYear.ToString()
                };
                row.Cells.Add(bookPublishYear);

                TableCell bookVolume = new TableCell
                {
                    Text = book.Volume
                };
                row.Cells.Add(bookVolume);

                TableCell bookIsbn = new TableCell
                {
                    Text = book.ISBN
                };
                row.Cells.Add(bookIsbn);

                TableCell bookInternalNr = new TableCell
                {
                    Text = book.InternalNr
                };
                row.Cells.Add(bookInternalNr);

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