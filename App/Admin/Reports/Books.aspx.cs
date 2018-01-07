
namespace Admin.Reports
{
    using BL.Constants;
    using BL.Managers;
    using DAL.Entities;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System.Web.UI;
    using System.Text;
    using iTextSharp.text.html;
    using System.Collections.Generic;
    using iTextSharp.tool.xml;
    using System.Data;
    using ClosedXML.Excel;

    public partial class Books : RaportsBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cellText = " <p><i class='fa fa-square {0}'></i>{1} </p>";

            var user = Session[SessionConstants.LoginUser] as User;
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static int AsyncGetBooksNr()
        {
            var user = HttpContext.Current.Session[SessionConstants.LoginUser] as User;

            int booksNr = BooksManager.GetBooksNrAll(user.Library.Id);

            return booksNr;
        }

        protected void btnBooksDownloadPdf_Click(object sender, EventArgs e)
        {
            var cssText = @"table {
                    border-collapse: collapse;
                    width : 100%;
                }

                th{
                    font-weight: 900;
                    background-color:#f9f9f9;
                    padding:8px;
                }
                table, th, td {
                    border: 1px solid black;
                    padding: 3px;
                }";

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string content = this.CreateBooksPDFContent();
            StringReader sr = new StringReader(content);
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            var memoryStream = new MemoryStream();

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText));
            var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, htmlMemoryStream, cssMemoryStream);

            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        private string CreateBooksPDFContent()
        {
            var user = Session[SessionConstants.LoginUser] as User;
            var books = BooksManager.GetAllBooks(user.Library.Id);

            StringBuilder sb = new StringBuilder();
            sb.Append("<h2> Biblioteca " + user.Library.Name + "</h2>");
            sb.Append("<table>");
            sb.Append("<tr><th>Titlul</th><th>ISBNs</th><th>Autor/i</th><th>Editura</th><th>An apariție</th></tr>");

            foreach (var book in books)
            {
                var authors = (book.Authors.Count > 0) ? book.Authors.First().Name : string.Empty;
                var isbns = (book.ISBNs.Count > 0) ? book.ISBNs.First().Value : string.Empty;

                sb.Append("<tr>");
                sb.Append("<td>" + book.Title + "</td>");
                sb.Append("<td>" + isbns + "</td>");
                sb.Append("<td>" + authors + "</td>");
                sb.Append("<td>" + book.Publisher.Name + "</td>");
                sb.Append("<td>" + book.PublishYear + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }

        protected void btnBooksDownloadExcel_Click(object sender, EventArgs e)
        {
            var user = Session[SessionConstants.LoginUser] as User;
            var books = BooksManager.GetAllBooks(user.Library.Id);


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] 
            { 
                new DataColumn("Titlul", typeof(string)),
                new DataColumn("ISBN", typeof(string)),
                new DataColumn("Autor/i",typeof(string)),
                new DataColumn("Editură",typeof(string)), 
                new DataColumn("An apariție",typeof(string)) 
            });

            foreach (var book in books)
            {
                var authors = (book.Authors.Count > 0) ? book.Authors.First().Name : string.Empty;
                var isbns = (book.ISBNs.Count > 0) ? book.ISBNs.First().Value : string.Empty;
                dt.Rows.Add(book.Title, isbns, authors, book.Publisher.Name,book.PublishYear);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Carti");
                string myName = Server.UrlEncode("CartiBiblioteca" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
                MemoryStream stream = GetStream(wb);
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + myName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.SuppressContent = true;
            }
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
    }
}