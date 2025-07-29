using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using StudentManagement_Service;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using StudentManagement_Service_DAL;


namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentProfileService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StudentProfileService.svc or StudentProfileService.svc.cs at the Solution Explorer and start debugging.
    public class StudentProfileService : IStudentProfileService
    {

        public StudentData.StudentData GetAllStudentData(StudentData.StudentData studentData)
        {
            IDataReader dataReaderStudentData = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");

                dataReaderStudentData = StudentProfileDAL.GetStudentData(db, studentData.StudentId);
                int count = 0;
                StudentData.StudentData data = new StudentData.StudentData();

                while (dataReaderStudentData.Read())
                {
                    if (count == 0)
                    {
                        data.Fullname = dataReaderStudentData["Fullname"].ToString();
                        data.Class = Convert.ToInt32(dataReaderStudentData["Class"]);
                        data.Gender = dataReaderStudentData["Gender"].ToString();
                        data.EmailID = dataReaderStudentData["Email"].ToString();
                        data.PhoneNum = dataReaderStudentData["Phonenum"].ToString();
                    }

                    data.CourseId.Add(Convert.ToInt32(dataReaderStudentData["CourseId"]));
                    count++;
                }
                dataReaderStudentData.Close();
                return data;
            }
            catch (Exception excep)
            {
                throw excep;
            }
            finally
            {
                if(dataReaderStudentData!=null && !dataReaderStudentData.IsClosed)
                {
                    dataReaderStudentData.Close();
                }
            }

        }

        //public StudentData.StudentData GetStudentData(StudentData.StudentData studentData)
        //{
        //    IDataReader dataReaderStudentData = null;
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase("newcon");

        //        dataReaderStudentData = StudentProfileDAL.GetDataFromUsertbl(db, studentData.StudentId, null, 0, 0);
        //        StudentData.StudentData data = null;

        //        if (dataReaderStudentData.Read())
        //        {
        //            data = new StudentData.StudentData();
        //            data.Fullname = dataReaderStudentData["Fullname"].ToString();
        //            data.Class = Convert.ToInt32(dataReaderStudentData["Class"]);
        //            data.Gender = dataReaderStudentData["Gender"].ToString();
        //            data.EmailID = dataReaderStudentData["Email"].ToString();
        //            data.PhoneNum = dataReaderStudentData["Phonenum"].ToString();
        //            //data.Username = dataReaderStudentData["username"].ToString();
        //            //data.Password = dataReaderStudentData["password"].ToString();
        //        }
        //        dataReaderStudentData.Close();
        //        return data;
        //    }
        //    catch (Exception excep)
        //    {
        //        throw excep;
        //    }
        //    finally
        //    {
        //        if (dataReaderStudentData != null && !dataReaderStudentData.IsClosed)
        //        {
        //            dataReaderStudentData.Close();
        //        }
        //    }
        //}

        //public List<int> GetSelectedCourses(StudentData.StudentData studentData)
        //{
        //    IDataReader dataReaderCourses = null;
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase("newcon");
        //        List<int> courseId = new List<int>();

        //        dataReaderCourses = StudentProfileDAL.GetSelectedCourses(db, studentData.StudentId);

        //        while (dataReaderCourses.Read())
        //        {
        //            courseId.Add(Convert.ToInt32(dataReaderCourses["CourseId"]));

        //        }
        //        dataReaderCourses.Close();
        //        return courseId;

        //    }
        //    catch (Exception excep)
        //    {
        //        throw excep;
        //    }
        //    finally
        //    {
        //        if (dataReaderCourses != null && !dataReaderCourses.IsClosed)
        //        {
        //            dataReaderCourses.Close();
        //        }
        //    }
        //}

        //public bool UpdateStudentData(StudentData.StudentData studentData)
        //{
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase("newcon");

        //        DataTable dataTableCourses = StudentProfileDAL.GetSelectedCourses(db, studentData.StudentId);
        //        List<int> courseIds = studentData.CourseId;

        //        System.Data.Common.DbTransaction transaction;
        //        using (System.Data.Common.DbConnection connection = db.CreateConnection())
        //        { 
        //            connection.Open();
        //            transaction = connection.BeginTransaction();
        //            try
        //            {
        //                bool isUpdated = StudentProfileDAL.UpdateStudentData(db, transaction, studentData.StudentId, studentData.Fullname, studentData.Class, studentData.Gender, studentData.EmailID, studentData.PhoneNum);

        //                foreach(DataRow row in dataTableCourses.Rows)
        //                {
        //                    for(int i=0; i < courseIds.Count; i++)
        //                    {
        //                        if (Convert.ToInt32(row["COURSEID"]) != courseIds[i])
        //                        {
        //                            if(i+1== courseIds.Count)
        //                            {
        //                                bool isCourseDeactivated = StudentProfileDAL.UpdateCourseFstatus(db, transaction, "I", studentData.StudentId, 0);
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }

        //                for(int i= 0; i < courseIds.Count; i++)
        //                {
        //                    foreach (DataRow row in dataTableCourses.Rows)
        //                    {
        //                        if (courseIds[i] != Convert.ToInt32(row["COURSEID"]))
        //                        {
        //                            if (i + 1 == courseIds.Count)
        //                            {
        //                                bool isCourseInserted = StudentRegistrationDAL.InsertSelectedCourses(db, transaction, studentData.StudentId, courseIds[i]);
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }

        //                transaction.Commit();
        //                return true;
        //            }
        //            catch
        //            {
        //                transaction.Rollback();
        //                throw;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //    catch (Exception excep)
        //    {
        //        throw excep;
        //    }
        //}

        public bool UpdateStudentData(StudentData.StudentData studentData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");
                System.Data.Common.DbTransaction transaction;
                using (System.Data.Common.DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    try
                    {
                        bool isUpdated = StudentProfileDAL.UpdateStudentData(db, transaction, studentData.StudentId, studentData.Fullname, studentData.Class, studentData.Gender, studentData.EmailID, studentData.PhoneNum);

                        //bool isPreCoursesDeleted = StudentProfileDAL.DeletePreviousCourses(db, transaction, studentData.StudentId, 0);
                        bool isPreCoursesUpdated = StudentProfileDAL.UpdateCourseFstatus(db, transaction, "I", studentData.StudentId, 0);

                        bool isNewCourseUpdated = false;
                        //bool isNewCoursesInserted = false;

                        //foreach (StudentData.CoursesData courses in studentData.Courses)
                        foreach (int courseId in studentData.CourseId)
                        {
                            bool isCourseExist = StudentProfileDAL.CheckCourseCount(db, transaction, studentData.StudentId, courseId);

                            if (isCourseExist)
                            {
                                isNewCourseUpdated = StudentProfileDAL.UpdateCourseFstatus(db, transaction, "A", studentData.StudentId, courseId);
                            }
                            else
                            {
                                isNewCourseUpdated = StudentRegistrationDAL.InsertSelectedCourses(db, transaction, studentData.StudentId, courseId);
                            }
                            //isNewCoursesInserted = StudentProfileDAL.InsertNewCourses(db, transaction, studentData.StudentId, courses.CourseId);
                        }

                        transaction.Commit();
                        bool isDataUpdated = isUpdated && isPreCoursesUpdated && isNewCourseUpdated;
                        return isDataUpdated;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception excep)
            {
                throw excep;
            }
        }
    }
}
