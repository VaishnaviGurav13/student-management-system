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
    /// Summary description for StudentProfileService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StudentProfileService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public StudentData GetStudentData(StudentData studentData)
        {
            return BLL.StudentProfileBLL.GetStudentData(studentData);
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<int> GetSelectedCourses(StudentData studentData)
        //{
        //    return BLL.StudentProfileBLL.GetSelectedCourses(studentData);
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool UpdateStudentData(StudentData studentData)
        {
            return BLL.StudentProfileBLL.UpdateStudentData(studentData);
        }
    }
}
