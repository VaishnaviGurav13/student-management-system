using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace StudnetManagement
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string webServicePath = ConfigurationManager.AppSettings["WebServicePath"].TrimEnd('/');
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PageLoad", "javascript:PageLoad('" + webServicePath + "');", true);
            }

        }
    }
}