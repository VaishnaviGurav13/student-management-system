using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminDashboardService" in both code and config file together.
    [ServiceContract]
    public interface IAdminDashboardService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "GetCountOfStudent", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        StudentData.CountData GetCountOfStudent();


        [OperationContract]
        [WebInvoke(UriTemplate = "GetDataFromUsertbl?studentData={studentData}&offSet={offSet}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        StudentData.CountData GetDataFromUsertbl(StudentData.StudentData studentData, int offSet, int limit);


        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateStudentStatus?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        bool UpdateStudentStatus(StudentData.StudentData studentData);
    }
}
