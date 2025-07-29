using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginPageService" in both code and config file together.
    [ServiceContract]
    public interface ILoginPageService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "GetUserData?studentData={studentData}", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        StudentData.StudentData GetUserData(StudentData.StudentData studentData);
    }
}
