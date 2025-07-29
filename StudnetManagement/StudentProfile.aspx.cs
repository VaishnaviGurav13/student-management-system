using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudnetManagement
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string webServicePath = ConfigurationManager.AppSettings["WebServicePath"].TrimEnd('/');

                Page.ClientScript.RegisterStartupScript(this.GetType(), "PageLoadFunction", "javascript:PageLoadFunction('" + webServicePath + "');", true);
            }
        }
    }
}