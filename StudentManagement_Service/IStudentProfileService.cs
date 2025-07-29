using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStudentProfileService" in both code and config file together.
    [ServiceContract]
    public interface IStudentProfileService
    {
        //[OperationContract]
        //[WebInvoke(UriTemplate = "GetStudentData?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        //StudentData.StudentData GetStudentData(StudentData.StudentData studentData);


        //[OperationContract]
        //[WebInvoke(UriTemplate = "GetSelectedCourses?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        //List<int> GetSelectedCourses(StudentData.StudentData studentData);


        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateStudentData?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        bool UpdateStudentData(StudentData.StudentData studentData);
    }
}
