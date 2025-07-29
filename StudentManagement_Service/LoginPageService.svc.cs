using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using StudentManagement_Service_DAL;
using MySql.Data.MySqlClient;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginPageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoginPageService.svc or LoginPageService.svc.cs at the Solution Explorer and start debugging.
    public class LoginPageService : ILoginPageService
    {
        //StudentData.StudentData studentData =null;

        public StudentData.StudentData GetUserData(StudentData.StudentData studentData)
        {
            IDataReader dataReaderStudent = null;
            try
            {
                //DatabaseProviderFactory factory = new DatabaseProviderFactory();
                //DatabaseFactory.SetDatabaseProviderFactory(factory); 

                Database db = DatabaseFactory.CreateDatabase("newcon");

                dataReaderStudent = LoginPageDAL.GetUserDataUsertbl(db, studentData.EmailID);
                //dataReaderStudent = StudentProfileDAL.GetDataFromUsertbl(db, 0, null);

                if (dataReaderStudent.Read())
                {
                    if (dataReaderStudent["Status"].ToString() != "I")
                    {
                        studentData = new StudentData.StudentData();
                        studentData.StudentId = Convert.ToInt32(dataReaderStudent["SId"]);
                        studentData.EmailID = dataReaderStudent["Email"].ToString();
                        studentData.Status = dataReaderStudent["Status"].ToString();
                        studentData.Role = dataReaderStudent["Role"].ToString();
                        studentData.Password = dataReaderStudent["password"].ToString();
                    }
                }
                dataReaderStudent.Close();
                return studentData;
            }
            catch(Exception excep)
            {
                throw excep;
            }
            finally
            {
                if (dataReaderStudent!=null && !dataReaderStudent.IsClosed)
                {
                    dataReaderStudent.Close();
                }
            }

        }

    }
}
