namespace Admin.Loans
{
    using Admin.Helpers;
    using BL.Constants;
    using BL.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class InProgress : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            List<Loan> loans = null;
            var user = Session[SessionConstants.LoginUser] as User;

            if (!Page.IsPostBack)
            {
                if (user != null)
                {
                    loans = LoansManager.GetActiveLoans(user.Library.Id);
                }
            }

            InitializeLoansTable(user.Library.Id, loans);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request["message"]) && Request["message"] == "LoanAddedSuccess")
            {
                lblMessage.Text = FlowMessages.LoanAddedSuccess;
                lblMessage.CssClass = "SuccessBox";
            }


        }

        protected void btnReturnBook_Click(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            LoansManager.ReturnBook(lblLoanId.Text.ToNullableInt().Value, DateTime.Now, user.Library.Id);
            Response.Redirect("~/Loans/InProgress.aspx");
        }

        protected void btnLoanRemove_Click(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;

            LoansManager.Remove(lblLoanId.Text.ToNullableInt().Value, user.Library.Id);
            Response.Redirect("~/Loans/InProgress.aspx");
        }

        private void loanReturnBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            lblPageTitle.InnerText = btn.CommandArgument;
            lblLoanId.Text = btn.CommandName;
            datatableResponsive.Visible = false;
            btnLoanCancel.Visible = true;
            btnReturnBook.Visible = true;
        }

        private void loanRemoveBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            lblPageTitle.InnerText = btn.CommandArgument;
            lblLoanId.Text = btn.CommandName;
            datatableResponsive.Visible = false;
            btnLoanCancel.Visible = true;
            btnLoanRemove.Visible = true;
        }

        private void InitializeLoansTable(int libraryId, List<Loan> loans = null)
        {
            var inputLoans = new List<Loan>();

            if (loans == null)
            {
                inputLoans = LoansManager.GetActiveLoans(libraryId);
            }
            else
            {
                inputLoans = loans;
            }

            foreach (Loan loan in inputLoans)
            {
                TableRow row = new TableRow();
                var btnLoanReturn = new Button
                {
                    ID = "BtnLoan" + loan.Id,
                    Text = "Returnează cartea",
                    CssClass = "btn btn-primary",
                    CausesValidation = false,
                    CommandName = loan.Id.ToString(),
                    CommandArgument = "Sigur dorești să returneazi cartea " + loan.Books[0].Title + "[ " + loan.Books[0].InternalNr + " ] închiriată de " + loan.User.FirstName + " " + loan.User.LastName + " ?"
                };
                btnLoanReturn.Click += this.loanReturnBtn_Click;
                TableCell loanIdCell = new TableCell();
                loanIdCell.Controls.Add(btnLoanReturn);
                row.Cells.Add(loanIdCell);

                var btnLoanRemove = new Button
                {
                    ID = "BtnRemoveLoan" + loan.Id,
                    Text = "Anulează împrumut",
                    CssClass = "btn btn-danger",
                    CausesValidation = false,
                    CommandName = loan.Id.ToString(),
                    CommandArgument = "Sigur dorești să anulezi împrumutul cărții " + loan.Books[0].Title + "[ " + loan.Books[0].InternalNr + " ] de către " + loan.User.FirstName + " " + loan.User.LastName + " ?"
                };
                btnLoanRemove.Click += this.loanRemoveBtn_Click;
                TableCell loanRemoveCell = new TableCell();
                loanRemoveCell.Controls.Add(btnLoanRemove);
                row.Cells.Add(loanRemoveCell);

                TableCell loanBook = new TableCell
                {
                    Text = loan.Books[0].Title + "[ " + loan.Books[0].InternalNr + " ]"
                };
                row.Cells.Add(loanBook);

                TableCell loanUser = new TableCell
                {
                    Text = loan.User.FirstName + " " + loan.User.LastName + "[" + loan.User.Id + "]"
                };
                row.Cells.Add(loanUser);

                TableCell loanCreatedDate = new TableCell
                {
                    Text = loan.LoanDate.ToString()
                };
                row.Cells.Add(loanCreatedDate);

                datatableResponsive.Rows.Add(row);
            }
        }

        protected void btnLoanReturnCancel_Click(object sender, EventArgs e)
        {
            this.ReturntoInitalState();
        }

        private void ReturntoInitalState()
        {
            btnLoanCancel.Visible = false;
            btnLoanRemove.Visible = false;
            btnReturnBook.Visible = false;
            datatableResponsive.Visible = true;
            lblPageTitle.InnerText = "Împrumuturi nereturnate";
        }
    }
}