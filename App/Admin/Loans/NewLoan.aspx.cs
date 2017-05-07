
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
    using System.Text;
    using BL.Constants;

    public partial class NewLoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var user = Session[SessionConstants.LoginUser] as User;

                InitializeBooks(user.Library.Id);
                InitializeUsers(user.Library.Id);
                btnLoanBookRemove.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!ValidateInput())
            {
                return;
            }

            if (!this.ValidateReserved())
            {
                btnForceSubmit.Visible = true;
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

            //Add Loan
            var user = Session[SessionConstants.LoginUser] as User;
            LoansManager.Add(loan, user.Library.Id);

            //update reservations to completed
            var reservations = GetAllReservedBooksFromSelectedByUserId(drdLoanUserAll.SelectedValue.ToNullableInt().Value);
            foreach (var item in reservations)
            {
                ReservationsManager.UpdateReservationByBookId(item.Book.Id, ReservationFlags.Completată);    
            }

            Response.Redirect("~/Loans/InProgress.aspx?Message=LoanAddedSuccess");
        }

        protected void btnForceSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
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

            var user = Session[SessionConstants.LoginUser] as User;

            //Add Loan
            LoansManager.Add(loan, user.Library.Id);

            //update reservations to canceled if user that loand is not the one who reserved
            var reservationsCanceled = GetAllReservedBooksFromSelectedByNotUserId(drdLoanUserAll.SelectedValue.ToNullableInt().Value);
            foreach (var item in reservationsCanceled)
            {
                ReservationsManager.UpdateReservationByBookId(item.Book.Id, ReservationFlags.Anulată);
            }

            //update reservations to completed 
            var reservationsCompleted = GetAllReservedBooksFromSelectedByUserId(drdLoanUserAll.SelectedValue.ToNullableInt().Value);
            foreach (var item in reservationsCompleted)
            {
                ReservationsManager.UpdateReservationByBookId(item.Book.Id, ReservationFlags.Completată);
            }

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

        private bool ValidateReserved()
        {
            var isValid = true;
            StringBuilder sb = new StringBuilder();

            foreach (ListItem bookItem in bltLoanBooksSelected.Items)
            {
                var reservation = ReservationsManager.GetActiveReservationByBookId(bookItem.Value.ToNullableInt().Value);
                if (reservation != null)
                {
                    if (reservation.User.Id != drdLoanUserAll.SelectedValue.ToNullableInt().Value)
                    {
                        sb.Append("Cartea " + bookItem.Text + " este rezervată pentru " + reservation.User.FirstName + " " + reservation.User.LastName + "<br>");

                        isValid = false;
                    }
                }
            }
            lblErrorMessage.Text = sb.ToString();
            lblErrorMessage.Visible = !isValid;

            return isValid;
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

        private List<Reservation> GetAllReservedBooksFromSelectedByUserId(int userId)
        {
            var reservations = new List<Reservation>();

            foreach (ListItem bookItem in bltLoanBooksSelected.Items)
            {
                var reservation = ReservationsManager.GetActiveReservationByBookId(bookItem.Value.ToNullableInt().Value);
                if (reservation != null)
                {
                    if (reservation.User.Id == userId)
                    {
                        reservation.Book = new Book
                        {
                            Id = bookItem.Value.ToNullableInt().Value,
                            Title = bookItem.Text
                        };
                        reservations.Add(reservation);
                    }
                }
            }

            return reservations;
        }

        private List<Reservation> GetAllReservedBooksFromSelectedByNotUserId(int userId)
        {
            var reservations = new List<Reservation>();

            foreach (ListItem bookItem in bltLoanBooksSelected.Items)
            {
                var reservation = ReservationsManager.GetActiveReservationByBookId(bookItem.Value.ToNullableInt().Value);
                if (reservation != null)
                {
                    if (reservation.User.Id != userId)
                    {
                        reservation.Book = new Book
                        {
                            Id = bookItem.Value.ToNullableInt().Value,
                            Title = bookItem.Text
                        };
                        reservations.Add(reservation);
                    }
                }
            }

            return reservations;
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

        private void InitializeBooks(int libraryId)
        {
            var books = BooksManager.GetAllBooks(libraryId);
            var listItemBooks = books.Select(c => new ListItem
            {
                Text = c.Title + "[" + c.InternalNr + "]",
                Value = c.Id.ToString(),
            }).ToArray();

            drdLoanBooksAll.Items.AddRange(listItemBooks);
            drdLoanBooksAll.DataBind();
        }

        private void InitializeUsers(int libraryId)
        {
            var users = UsersManager.GetUsersAll(libraryId);
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