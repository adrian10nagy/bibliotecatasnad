
namespace Admin.Loans
{
    using BL.Managers;
    using BL.Helpers;
    using DAL.Entities;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BL.Constants;

    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var user = Session[SessionConstants.LoginUser] as User;
                var loans = new List<Loan>();

                if (!string.IsNullOrEmpty(Request["day"]))
                {
                    loans = LoansManager.GetLoansByDay(Convert.ToDateTime(Request["day"]), user.Library.Id);
                }
                else if (!string.IsNullOrEmpty(Request["userId"]) && Request["userId"].ToNullableInt() != null)
                {
                    loans = LoansManager.GetLoansByUserId(Request["userId"].ToNullableInt().Value);
                }
                else if (!string.IsNullOrEmpty(Request["bookId"]) && Request["bookId"].ToNullableInt() != null)
                {
                    loans = LoansManager.GetLoansByBookId(Request["bookId"].ToNullableInt().Value);
                }
                else
                {
                    Response.Redirect("~/");
                }

                InitializeLoansTable(loans);
            }
        }

        private void InitializeLoansTable(List<Loan> loans)
        {
            foreach (Loan loan in loans)
            {
                TableRow row = new TableRow();

                TableCell loanBook = new TableCell
                {
                    Text = loan.Books[0].Title + "[" + loan.Books[0].InternalNr + "]"
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

                TableCell loanReturnedDate = new TableCell
                {
                    Text = loan.ReturnedDate.ToString()
                };
                row.Cells.Add(loanReturnedDate);

                datatableResponsive.Rows.Add(row);
            }
        }
    }
}