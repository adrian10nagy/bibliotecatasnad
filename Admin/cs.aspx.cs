using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class cs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static string[] GetFruits(string prefix)
        {
            List<string> fruits = new List<string>();
            fruits.Add("Mango");
            fruits.Add("Apple");
            fruits.Add("Banana");
            fruits.Add("Orange");
            fruits.Add("Pineapple");
            fruits.Add("Guava");
            fruits.Add("Grapes");
            fruits.Add("Papaya");
            return fruits.Where(f => f.ToLower().IndexOf(prefix.ToLower()) != -1).ToArray();
        }

        protected void Submit(object sender, EventArgs e)
        {
            string customerName = Request.Form[txtSearch.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Name: " + customerName + "');", true);
        }

    }
}