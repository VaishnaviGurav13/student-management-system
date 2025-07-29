using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentManagement_Service.StudentData;

namespace StudnetManagement.BLL
{
    public class StudentRegistrationBLL
    {
        public static ErrorData GetCountOfStudent(StudentData studentData)
        {
            StudentManagement_Service.StudentRegistrationService service = new StudentManagement_Service.StudentRegistrationService();
            try
            {
                ErrorData errorData = service.GetCountOfStudent(studentData);
                //bool isCountZero = service.GetCountOfStudent(studentData);
                //service.Close();
                return errorData;
            }
            catch(Exception excep)
            {
                //service.Abort();
                throw excep;
            }
        }

        public static List<CoursesData> GetAllCourses()
        {
            StudentManagement_Service.StudentRegistrationService service = new StudentManagement_Service.StudentRegistrationService();
            try
            {
                List<CoursesData> courses = service.GetAllCourses();
                //service.Close();
                return courses;
            }
            catch(Exception excep)
            {
                //service.Abort();
                throw excep;
            }
        }

        //public static bool InsertDataSelectedCoursestbl(StudentData studentData)
        //{
        //    StudentManagement_Service.StudentRegistrationService service = new StudentManagement_Service.StudentRegistrationService();
        //    try
        //    {
        //        bool isCoursesInserted = service.InsertDataSelectedCoursestbl(studentData);
        //        //service.Close();
        //        return isCoursesInserted;
        //    }
        //    catch(Exception excep)
        //    {
        //        //service.Abort();
        //        throw excep;
        //    }
        //}
    }
}