using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace StudnetManagement
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string webServicePath=ConfigurationManager.AppSettings["WebServicePath"].TrimEnd('/');

            Page.ClientScript.RegisterStartupScript(this.GetType(), "pageLoadFunction", "javascript:pageLoadFunction('" + webServicePath + "');", true);

        }
    }
}