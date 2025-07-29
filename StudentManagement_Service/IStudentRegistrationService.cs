using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStudentRegistrationPageService" in both code and config file together.
    [ServiceContract]
    public interface IStudentRegistrationService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "GetCountOfStudent?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        StudentData.ErrorData GetCountOfStudent(StudentData.StudentData studentData);


        [OperationContract]
        [WebInvoke(UriTemplate = "GetCoursesFromCoursestbl", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        List<StudentData.CoursesData> GetAllCourses();


        //[OperationContract]
        //bool InsertDataSelectedCoursestbl(StudentData.StudentData studentData);
    }
}
