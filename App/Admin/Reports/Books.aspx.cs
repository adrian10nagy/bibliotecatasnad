
namespace Admin.Reports
{
    using BL.Constants;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class Books : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;

            string cellText = " <p><i class='fa fa-square {0}'></i>{1} </p>";

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
    }
}