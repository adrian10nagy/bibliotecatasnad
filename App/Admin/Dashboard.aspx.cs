
namespace Admin
{
    using BL.Constants;
    using BL.Entities;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Linq;
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class _Default : Page
    {
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static BookPublishersChart AsyncGetBookDomains()
        {
            var user = System.Web.HttpContext.Current.Session[SessionConstants.LoginUser] as User;

            // instantiate a serializer
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var publishers = BooksManager.GetBookPublisherForChart(user.Library.Id);
            // var jsonValue = TheSerializer.Serialize(products);
            
            return publishers;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;

            string cellText = " <p><i class='fa fa-square {0}'></i>{1} </p>";

            txtTotalUsers.Text = UsersManager.GetUserNrAll(user.Library.Id).ToString();
            txtTotalBooks.Text = BooksManager.GetBooksNrAll(user.Library.Id).ToString();
            txtTotalLoans.Text = LoansManager.GetLoansNrAll(user.Library.Id).ToString();
            txtTotalLoansToday.Text = LoansManager.GetLoansByDay(DateTime.Now, user.Library.Id).Count().ToString();
            txtTotalBooksToday.Text = BooksManager.GetBooksByDay(DateTime.Now, user.Library.Id).Count().ToString();
            txtTotalUsersToday.Text = UsersManager.GetUsersByDay(DateTime.Now, user.Library.Id).Count().ToString();

            var publishers = BooksManager.GetBookPublisherForChart(user.Library.Id);

            for (int i = 0; i < publishers.Labels.Count(); i++)
			{
                TableRow row = new TableRow();
                TableCell cellName = new TableCell
                {
                    Text = string.Format(cellText, publishers.Dataset.SquareColors[i], publishers.Labels[i])
                };
                row.Cells.Add(cellName);

                TableCell cellPercentage = new TableCell
                {
                    Text = publishers.Dataset.Percentage[i] + "%"
                };
                row.Cells.Add(cellPercentage);

                tblTopPulishers.Rows.Add(row);
            }

        }

        protected void lnkMainSearch_Click(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            // var book = BooksManager.GetBookByISBN(txtMainSearch.Text, user.Library.Id);
        }
    }
}