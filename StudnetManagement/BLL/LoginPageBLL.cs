using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentManagement_Service;
using StudentManagement_Service.StudentData;

namespace StudnetManagement.BLL
{
    public class LoginPageBLL
    {

        public static StudentData GetUserData(StudentData data)
        {
            try
            {
                StudentManagement_Service.LoginPageService loginPageService = new StudentManagement_Service.LoginPageService();
                //LoginPageService loginPageService = new LoginPageService();
                return loginPageService.GetUserData(data);
            }
            catch(Exception excep)
            {
                throw excep;
            }
        }

    }
}