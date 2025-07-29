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
    /// Summary description for AdminDashboardService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdminDashboardService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public CountData GetCountOfStudent()
        {
            return BLL.AdminDashboardBLL.GetCountOfStudent();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public CountData GetDataFromUsertbl(StudentData student, int offSet, int limit)
        {
            return BLL.AdminDashboardBLL.GetDataFromUsertbl(student, offSet, limit);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool UpdateStudentStatus(StudentData studentData)
        {
            return BLL.AdminDashboardBLL.UpdateStudentStatus(studentData);
        }

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public bool DeleteStudent(StudentData studentData)
        //{
        //    return BLL.AdminDashboardBLL.DeleteStudent(studentData);
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ErrorData InsertDataCoursetbl(CoursesData coursesData)
        {
            return BLL.AdminDashboardBLL.InsertDataCoursetbl(coursesData);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool DeleteCourse(CoursesData coursesData)
        {
            return BLL.AdminDashboardBLL.DeleteCourse(coursesData);
        }
    }
}
