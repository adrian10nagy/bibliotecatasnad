
namespace Admin.Loans
{
    using BL.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BL.Helpers;
    using DAL.Entities;

    public partial class NewLoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializeBooks();
                InitializeUsers();
                btnLoanBookRemove.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!ValidateInput())
            {
                return;
            }

            var loan = new Loan
            {
                User = new User
                {
                    Id = drdLoanUserAll.SelectedValue.ToNullableInt().Value
                },
                LoanDate = DateTime.Now,
                Books = GetLoanBooks()
            };

            LoansManager.Add(loan);
            Response.Redirect("~/Loans/InProgress.aspx?Message=LoanAddedSuccess");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Loans/InProgress.aspx");
        }

        protected void drdLoanBooksAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drdLoanBooksAll.SelectedValue.ToNullableInt() != 0)
            {
                var book = BooksManager.GetBookById(drdLoanBooksAll.SelectedValue.ToNullableInt().Value);
                if (book != null)
                {
                    var listItem = new ListItem
                    {
                        Text = book.Title + "[ " + book.InternalNr + " ]",
                        Value = book.Id.ToString()
                    };

                    bltLoanBooksSelected.Items.Add(listItem);
                    bltLoanBooksSelected.DataBind();
                    foreach (ListItem item in bltLoanBooksSelected.Items)
                    {
                        drdLoanBooksAll.Items.FindByValue(item.Value).Attributes.Add("Disabled", "Disabled");
                        
                    }
                }
            }
            drdLoanBooksAll.SelectedIndex = 0;
            btnLoanBookRemove.Visible = true;
        }

        protected void btnLoanBookRemove_Click(object sender, EventArgs e)
        {
            bltLoanBooksSelected.Items.Clear();
            drdLoanBooksAll.SelectedIndex = 0;
            drdLoanBooksAll.DataBind();
            btnLoanBookRemove.Visible = false;
        }

        private List<Book> GetLoanBooks()
        {
            var books = new List<Book>();

            foreach (ListItem bookItem in bltLoanBooksSelected.Items)
            {
                if (bookItem.Value.ToNullableInt() != 0)
                {
                    books.Add(new Book
                    {
                        Id = bookItem.Value.ToNullableInt().Value
                    });
                }
            }

            return books;
        }

        private bool ValidateInput()
        {
            bool userIsValid = true;
            bool bookIsValid = false;
            lblErrorMessage.Text = string.Empty;

            if (drdLoanUserAll.SelectedValue.ToNullableInt() == null ||  drdLoanUserAll.SelectedValue.ToNullableInt() == 0)
            {
                userIsValid = false;
                lblErrorMessage.Text = lblErrorMessage.Text + "Selectați utilizatorul<br>";
            }

            if (bltLoanBooksSelected.Items.Count != 0)
            {
                foreach (ListItem bookItem in bltLoanBooksSelected.Items)
                {
                    if (bookItem.Value.ToNullableInt() != 0)
                    {
                        bookIsValid = true;
                    }
                }
            }
            else
            {
                lblErrorMessage.Text = lblErrorMessage.Text + "Selectați cel puțin o carte";
            }

            return userIsValid && bookIsValid;
        }

        private void InitializeBooks()
        {
            var books = BooksManager.GetAllBooks();
            var listItemBooks = books.Select(c => new ListItem
            {
                Text = c.Title + "[" + c.InternalNr + "]",
                Value = c.Id.ToString(),
            }).ToArray();

            drdLoanBooksAll.Items.AddRange(listItemBooks);
            drdLoanBooksAll.DataBind();
        }

        private void InitializeUsers()
        {
            var users = UsersManager.GetUsersAll();
            var listItemBooks = users.Select(c => new ListItem
            {
                Text = c.FirstName + " " + c.LastName + "[" + c.Username + "]",
                Value = c.Id.ToString(),
            }).ToArray();

            drdLoanUserAll.Items.AddRange(listItemBooks);
            drdLoanUserAll.DataBind();
        }
    }
}