using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using StudentManagement_Service.StudentData;
using StudnetManagement.BLL;

namespace StudnetManagement.WebServices
{
    /// <summary>
    /// Summary description for StudentRegistrationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StudentRegistrationService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ErrorData RegisterStudent(StudentData student)
        {
            return StudentRegistrationBLL.GetCountOfStudent(student);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<CoursesData> LoadCourses()
        {
            return StudentRegistrationBLL.GetAllCourses();
        }
    }
}
