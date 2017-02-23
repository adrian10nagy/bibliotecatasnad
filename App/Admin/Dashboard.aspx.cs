
namespace Admin
{
    using BL.Entities;
    using BL.Managers;
    using System;
    using System.Linq;
    using System.Web.Script.Serialization;
    using System.Web.Script.Services;
    using System.Web.UI;

    public partial class _Default : Page
    {
        [System.Web.Services.WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static BookPublishersChart AsyncGetBookDomains()
        {
            // instantiate a serializer
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var products = BooksManager.GetBookPublisherForChart();
            // var jsonValue = TheSerializer.Serialize(products);

            return products;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            txtTotalUsers.Text = UsersManager.GetUserNrAll().ToString();
            txtTotalBooks.Text = BooksManager.GetBooksNrAll().ToString();
            txtTotalLoans.Text = LoansManager.GetLoansNrAll().ToString();
            txtTotalLoansToday.Text = LoansManager.GetLoansByDay(DateTime.Now).Count().ToString();
        }
    }
}