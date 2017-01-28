
namespace Admin
{
    using BL.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTotalUsers.Text = UsersManager.GetUserNrAll().ToString();
            txtTotalBooks.Text = BooksManager.GetBooksNrAll().ToString();
            txtTotalLoans.Text = LoansManager.GetLoansNrAll().ToString();
        }
    }
}