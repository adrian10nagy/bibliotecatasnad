
namespace Admin
{
    using BL.Entities;
    using BL.Managers;
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
            // instantiate a serializer
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var publishers = BooksManager.GetBookPublisherForChart();
            // var jsonValue = TheSerializer.Serialize(products);
            
            return publishers;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string cellText = " <p><i class='fa fa-square {0}'></i>{1} </p>";

            txtTotalUsers.Text = UsersManager.GetUserNrAll().ToString();
            txtTotalBooks.Text = BooksManager.GetBooksNrAll().ToString();
            txtTotalLoans.Text = LoansManager.GetLoansNrAll().ToString();
            txtTotalLoansToday.Text = LoansManager.GetLoansByDay(DateTime.Now).Count().ToString();
            txtTotalBooksToday.Text = BooksManager.GetBooksByDay(DateTime.Now).Count().ToString();
            txtTotalUsersToday.Text = UsersManager.GetUsersByDay(DateTime.Now).Count().ToString();

            var publishers = BooksManager.GetBookPublisherForChart();

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
    }
}