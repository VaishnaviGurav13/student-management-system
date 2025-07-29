using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using StudentManagement_Service.StudentData;

namespace StudnetManagement.WebServices
{
    /// <summary>
    /// Summary description for LoginPageService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LoginPageService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public StudentData GetUserData(StudentData LoginData)
        {
            StudentData studentData= BLL.LoginPageBLL.GetUserData(LoginData);
            return studentData;
            //return BLL.LoginPageBLL.GetUserData(LoginData);
        }
    }
}
