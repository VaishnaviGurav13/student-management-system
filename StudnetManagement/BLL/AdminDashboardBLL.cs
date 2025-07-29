using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentManagement_Service.StudentData;

namespace StudnetManagement.BLL
{
    public class AdminDashboardBLL
    {

        public static CountData GetCountOfStudent()
        {
            StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
            try
            {
                CountData countData = new CountData();
                countData = adminDashboardService.GetCountOfStudent();
                //adminDashboardService.Close();
                return countData;
            }
            catch(Exception excep)
            {
                //adminDashboardService.Abort();
                throw excep;
            }
        }

        public static CountData GetDataFromUsertbl(StudentData studentData, int offSet, int limit)
        {
            StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
            try
            {
                CountData countData = new CountData();
                countData = adminDashboardService.GetDataFromUsertbl(studentData, offSet, limit);
                //adminDashboardService.Close();
                return countData;
            }
            catch(Exception excep)
            {
                //adminDashboardService.Abort();
                throw excep;
            }
        }

        public static bool UpdateStudentStatus(StudentData studentData)
        {
            StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
            try
            {
                bool isStatusUpdated = adminDashboardService.UpdateStudentStatus(studentData);
                //adminDashboardService.Close();
                return isStatusUpdated;
            }
            catch(Exception excep)
            {
                //adminDashboardService.Abort();
                throw excep;
            }
        }

        //public static bool DeleteStudent(StudentData studentData)
        //{
        //    StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
        //    try
        //    {
        //        bool isDataDeleted = adminDashboardService.DeleteStudent(studentData);
        //        //adminDashboardService.Close();
        //        return isDataDeleted;
        //    }
        //    catch(Exception excep)
        //    {
        //        //adminDashboardService.Abort();
        //        throw excep;
        //    }
        //}

        public static ErrorData InsertDataCoursetbl(CoursesData coursesData)
        {
            StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
            try
            {
                ErrorData errorData = adminDashboardService.InsertDataCoursetbl(coursesData);
                //adminDashboardService.Close();
                return errorData;
            }
            catch (Exception excep)
            {
                //adminDashboardService.Abort();
                throw excep;
            }
        }

        public static bool DeleteCourse(CoursesData coursesData)
        {
            StudentManagement_Service.AdminDashboardService adminDashboardService = new StudentManagement_Service.AdminDashboardService();
            try
            {
                bool isCourseDeleted = adminDashboardService.DeleteCourse(coursesData);
                //adminDashboardService.Close();
                return isCourseDeleted;
            }
            catch (Exception excep)
            {
                //adminDashboardService.Abort();
                throw excep;
            }
        }
    }
}