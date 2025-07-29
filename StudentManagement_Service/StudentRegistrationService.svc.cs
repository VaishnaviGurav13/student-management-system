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
using System.Data.Common;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentRegistrationPageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StudentRegistrationPageService.svc or StudentRegistrationPageService.svc.cs at the Solution Explorer and start debugging.
    public class StudentRegistrationService : IStudentRegistrationService
    {
        public StudentData.ErrorData GetCountOfStudent(StudentData.StudentData studentData)
        {

            IDataReader dataReaderStudentId = null;
            try
            {
                //DatabaseProviderFactory factory = new DatabaseProviderFactory();
                //DatabaseFactory.SetDatabaseProviderFactory(factory);

                Database db = DatabaseFactory.CreateDatabase("newcon");
                DbTransaction transaction;

                bool IsExist = StudentRegistrationDAL.CheckCountOfStudent(db, studentData.EmailID);
                //bool isInsertedUsertbl = false;
                bool isCoursesInserted = false;
                StudentData.ErrorData errorData = new StudentData.ErrorData();

                if (IsExist == false)
                {
                    if (studentData.CourseId.Count != 0)
                    {
                        DbConnection connection = db.CreateConnection();
                        connection.Open();
                        transaction = connection.BeginTransaction();

                        try
                        {

                            //isInsertedUsertbl = StudentRegistrationDAL.InsertDataUsertbl(db, transaction, studentData.Fullname, studentData.Username, studentData.Class, studentData.EmailID, studentData.Gender, studentData.PhoneNum, studentData.Password);
                            //dataReaderStudentId = StudentRegistrationDAL.GetStudentId(db, transaction, studentData.EmailID);

                            dataReaderStudentId = StudentRegistrationDAL.InsertDataUsertbl(db, transaction, studentData.Fullname, studentData.Username, studentData.Class, studentData.EmailID, studentData.Gender, studentData.PhoneNum, studentData.Password);

                            if (dataReaderStudentId.Read())
                            {
                                studentData.StudentId = Convert.ToInt32(dataReaderStudentId["SID"]);
                            }
                            else
                            {
                                errorData.ErrorCode = 1;
                                errorData.ErrorMsg = "Failed to Register User. Please Try Again!!";

                                //return errorData;
                            }
                            dataReaderStudentId.Close();

                            isCoursesInserted = InsertDataSelectedCoursestbl(db, transaction, studentData);

                            if (!isCoursesInserted)
                            {
                                transaction.Rollback();
                                errorData.ErrorCode = 1;
                                errorData.ErrorMsg = "An Unexpected Error Occured. Please Check!!";
                                return errorData;
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                        finally
                        {
                            
                            connection.Close();

                            if (dataReaderStudentId != null && !dataReaderStudentId.IsClosed)
                            {
                                dataReaderStudentId.Close();
                            }
                        }

                    }
                    else
                    {
                        errorData.ErrorCode = 1;
                        errorData.ErrorMsg = "An Unexpected Error Occured. Please Check!!";
                    }
                }
                else
                {
                    errorData.ErrorCode = 1;
                    errorData.ErrorMsg = "This Email ID already exists. Please check";
                }
                //return isInsertedUsertbl && isCoursesInserted; ;
                return errorData;
            }
            catch (Exception excep)
            {
                throw excep;
            }

        }

        public List<StudentData.CoursesData> GetAllCourses()
        {
            IDataReader dataReaderCourses = null;
            try
            {
                //DatabaseProviderFactory factory = new DatabaseProviderFactory();
                //DatabaseFactory.SetDatabaseProviderFactory(factory);
                Database db = DatabaseFactory.CreateDatabase("newcon");

                dataReaderCourses = StudentRegistrationDAL.GetAllCourses(db);
                List<StudentData.CoursesData> courses = new List<StudentData.CoursesData>();

                while (dataReaderCourses.Read())
                {
                    StudentData.CoursesData coursesData = new StudentData.CoursesData();
                    coursesData.CourseId = Convert.ToInt32(dataReaderCourses["CId"]);
                    coursesData.CourseName = dataReaderCourses["CourseName"].ToString();

                    courses.Add(coursesData);
                    //studentData.Courses.Add(coursesData);
                }
                //return studentData;
                dataReaderCourses.Close();
                return courses;
            }
            catch (Exception excep)
            {
                throw excep;
            }
            finally
            {
                if (dataReaderCourses != null && !dataReaderCourses.IsClosed)
                {
                    dataReaderCourses.Close();
                }
            }

        }

        public bool InsertDataSelectedCoursestbl(Database db, DbTransaction transaction, StudentData.StudentData studentData)
        {
            try
            {
                //DatabaseProviderFactory factory = new DatabaseProviderFactory();
                //DatabaseFactory.SetDatabaseProviderFactory(factory);
                //Database db = DatabaseFactory.CreateDatabase("newcon");

                //int studentId = StudentRegistrationDAL.GetStudentId(db, transaction, studentData.EmailID);
                bool isDataInserted = false;

                if (studentData.CourseId.Count > 0)
                {
                    foreach (int courseId in studentData.CourseId)
                    {
                        isDataInserted = StudentRegistrationDAL.InsertSelectedCourses(db, transaction, studentData.StudentId, courseId);

                        if (!isDataInserted)
                            return isDataInserted;
                    }
                }
                return isDataInserted;
            }
            catch (Exception excep)
            {
                throw excep;
            }
        }
    }
}
