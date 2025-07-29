using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentManagement_Service.StudentData;

namespace StudnetManagement.BLL
{
    public class StudentProfileBLL
    {
        public static StudentData GetStudentData(StudentData student)
        {
            StudentManagement_Service.StudentProfileService service = new StudentManagement_Service.StudentProfileService();
            try
            {
                StudentData studentData = new StudentData();
                studentData = service.GetAllStudentData(student);
                //studentData = service.GetStudentData(student);
                //service.Close();
                return studentData;
            }
            catch(Exception excep)
            {
                //service.Abort();
                throw excep;
            }
        }

        //public static List<int> GetSelectedCourses(StudentData student)
        //{
        //    StudentManagement_Service.StudentProfileService service = new StudentManagement_Service.StudentProfileService();
        //    try
        //    {
        //        List<int> courseId = service.GetSelectedCourses(student);
        //        //service.Close();
        //        return courseId;
        //    }
        //    catch(Exception excep)
        //    {
        //        //service.Abort();
        //        throw excep;
        //    }
        //}

        public static bool UpdateStudentData(StudentData student)
        {
            StudentManagement_Service.StudentProfileService service = new StudentManagement_Service.StudentProfileService();
            try
            {
                bool isDataUpdated = service.UpdateStudentData(student);
                //service.Close();
                return isDataUpdated;
            }
            catch(Exception excep)
            {
                //service.Abort();
                throw excep;
            }
        }
    }
}