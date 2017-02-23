
namespace Admin.Loans
{
    using Admin.Helpers;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Web.UI.WebControls;

    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializeLoansTable();
            }
        }

        private void InitializeLoansTable()
        {
            var loans = LoansManager.GetFinishedLoans();

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