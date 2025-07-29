using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using StudentManagement_Service_DAL;

namespace StudentManagement_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminDashboardService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AdminDashboardService.svc or AdminDashboardService.svc.cs at the Solution Explorer and start debugging.
    public class AdminDashboardService : IAdminDashboardService
    {
        public StudentData.CountData GetCountOfStudent()
        {
            IDataReader dataReaderCount = null;
            //IDataReader dataReaderCourseCount = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");
                StudentData.CountData countData = new StudentData.CountData();
                dataReaderCount = AdminDashboardDAL.GetStudentStatusCount(db);

                while (dataReaderCount.Read())
                {
                    string status = dataReaderCount["STATUS"].ToString();

                    if (status == "P")
                        countData.PendingStudentCount = Convert.ToInt32(dataReaderCount["USERCOUNT"]);
                    else if (status == "A")
                        countData.AcceptedStudentCount = Convert.ToInt32(dataReaderCount["USERCOUNT"]);
                    else if (status == "R")
                        countData.RejectedStudentCount = Convert.ToInt32(dataReaderCount["USERCOUNT"]);
                }
                dataReaderCount.Close();

                //countData.PendingStudentCount = StudentRegistrationDAL.GetCountOfStudent(db, null, "P");
                //countData.AcceptedStudentCount = StudentRegistrationDAL.GetCountOfStudent(db, null, "A");
                //countData.RejectedStudentCount = StudentRegistrationDAL.GetCountOfStudent(db, null, "R");

                countData.TotalStudentCount = countData.PendingStudentCount + countData.AcceptedStudentCount
                    + countData.RejectedStudentCount;


                dataReaderCount = AdminDashboardDAL.GetCourseCount(db, null);

                while (dataReaderCount.Read())
                {
                    if(dataReaderCount["FSTATUS"].ToString()=="A")
                        countData.TotalCourseCount = Convert.ToInt32(dataReaderCount["COURSECOUNT"]);
                }
                dataReaderCount.Close();

                return countData;
            }
            catch(Exception excep)
            {
                throw excep;
            }
            finally
            {
                if (dataReaderCount != null && !dataReaderCount.IsClosed)
                {
                    dataReaderCount.Close();
                }

                //if (dataReaderCourseCount != null && !dataReaderCourseCount.IsClosed)
                //{
                //    dataReaderCourseCount.Close();
                //}
            }
        }

        public StudentData.CountData GetDataFromUsertbl(StudentData.StudentData studentData, int offSet, int limit)
        {
            IDataReader dataReaderStudent = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");
                
                dataReaderStudent = AdminDashboardDAL.GetDataFromUsertbl(db, 0, studentData.Status, limit, offSet);

                StudentData.CountData countData = new StudentData.CountData(); 

                while (dataReaderStudent.Read())
                {
                    StudentData.StudentData data = new StudentData.StudentData();

                    data.StudentId = Convert.ToInt32(dataReaderStudent["SId"]);
                    data.Fullname = dataReaderStudent["Fullname"].ToString();
                    data.Class = Convert.ToInt32(dataReaderStudent["Class"]);
                    data.Gender = dataReaderStudent["Gender"].ToString();
                    data.EmailID = dataReaderStudent["Email"].ToString();
                    data.Status = dataReaderStudent["Status"].ToString();

                    countData.StudentData.Add(data);
                }

                dataReaderStudent.Close();
                return countData;
            }
            catch(Exception excep)
            {
                throw excep;
            }
            finally
            {
                if (dataReaderStudent != null && !dataReaderStudent.IsClosed)
                {
                    dataReaderStudent.Close();
                }
            }
        }

        public bool UpdateStudentStatus(StudentData.StudentData studentData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");
                bool isStatusUpdated = false;

                if (studentData.Status != "I")
                {
                    isStatusUpdated = AdminDashboardDAL.UpdateStudentStatus(db, null, studentData.StudentId, studentData.Status);
                }
                else
                {
                    System.Data.Common.DbTransaction transaction;

                    using (System.Data.Common.DbConnection connection = db.CreateConnection())
                    {
                        connection.Open();
                        transaction = connection.BeginTransaction();

                        try
                        {
                            //bool isStudentDeleted = AdminDashboardDAL.InactiveStudentData(db, transaction, student.StudentId);
                            bool isStudentDeleted = AdminDashboardDAL.UpdateStudentStatus(db, transaction, studentData.StudentId, "I");
                            bool isCoursesDeleted = false;

                            if (isStudentDeleted)
                            {
                                isCoursesDeleted = StudentProfileDAL.UpdateCourseFstatus(db, transaction, "I", studentData.StudentId, 0);
                            }
                            else
                            {
                                transaction.Rollback();
                                return isCoursesDeleted;
                            }

                            transaction.Commit();
                            isStatusUpdated = isStudentDeleted && isCoursesDeleted;
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
                return isStatusUpdated;
            }
            catch (Exception excep)
            {
                throw excep;
            }
        }

        //public bool UpdateStudentStatus(StudentData.StudentData studentData)
        //{
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase("newcon");
        //        bool isStatusUpdated = AdminDashboardDAL.UpdateStudentStatus(db, null, studentData.StudentId, studentData.Status);
        //        return isStatusUpdated;
        //    }
        //    catch(Exception excep)
        //    {
        //        throw excep;
        //    }
        //}

        //public bool DeleteStudent(StudentData.StudentData student)
        //{
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase("newcon");
        //        System.Data.Common.DbTransaction transaction;

        //        using(System.Data.Common.DbConnection connection = db.CreateConnection())
        //        {
        //            connection.Open();
        //            transaction = connection.BeginTransaction();

        //            try
        //            {
        //                //bool isStudentDeleted = AdminDashboardDAL.InactiveStudentData(db, transaction, student.StudentId);
        //                bool isStudentDeleted = AdminDashboardDAL.UpdateStudentStatus(db, transaction, student.StudentId, "I");
        //                bool isCoursesDeleted = false;

        //                if (isStudentDeleted)
        //                {
        //                    isCoursesDeleted = StudentProfileDAL.InactivePreviousCourses(db, transaction, "I", student.StudentId, 0);
        //                }
        //                else
        //                {
        //                    transaction.Rollback();
        //                }

        //                transaction.Commit();
        //                return isStudentDeleted && isCoursesDeleted;
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
        //    catch(Exception excep)
        //    {
        //        throw excep;
        //    }
        //}

        public StudentData.ErrorData InsertDataCoursetbl(StudentData.CoursesData coursesData)
        {
            IDataReader dataReaderCourseCount = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");

                dataReaderCourseCount = AdminDashboardDAL.GetCourseCount(db, coursesData.CourseName);
                bool isCourseInserted = false;
                StudentData.ErrorData errorData = new StudentData.ErrorData();

                if (dataReaderCourseCount.Read())
                {
                    //while (dataReaderCourseCount.Read())
                    //{
                        string fStatus = dataReaderCourseCount["FSTATUS"].ToString();
                        int courseCount = Convert.ToInt32(dataReaderCourseCount["COURSECOUNT"]);
                        if (fStatus == "A" && courseCount > 0)
                        {
                            errorData.ErrorCode = 1;
                            errorData.ErrorMsg = "This Course Already exists!!";
                        }
                        else if (fStatus == "I" && courseCount > 0)
                        {
                            bool isUpdated = AdminDashboardDAL.ChangeCourseFstatus(db, null, "A", coursesData.CourseName);

                            if (!isUpdated)
                            {
                                errorData.ErrorCode = 1;
                                errorData.ErrorMsg = "An Error has Occured. Please Check.";
                            }
                        }
                    //}
                }
                else
                {
                    isCourseInserted = AdminDashboardDAL.InsertDataCoursetbl(db, coursesData.CourseName);

                }
                dataReaderCourseCount.Close();
                
                return errorData;
            }
            catch (Exception excep)
            {
                throw excep;
            }
            finally
            {
                if (dataReaderCourseCount != null && !dataReaderCourseCount.IsClosed)
                {
                    dataReaderCourseCount.Close();
                }
            }
        }

        public bool DeleteCourse(StudentData.CoursesData coursesData)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("newcon");
                DbTransaction transaction;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    try
                    {
                        //bool isCourseDeleted = AdminDashboardDAL.ChangeCourseFstatus(db, transaction, "I", coursesData.CourseId);
                        bool isCourseDeleted = AdminDashboardDAL.ChangeCourseFstatus(db, transaction, "I", coursesData.CourseName);

                        bool isMappedCourseDeleted = false;

                        if (isCourseDeleted)
                        {
                            isMappedCourseDeleted = StudentProfileDAL.UpdateCourseFstatus(db, transaction, "I", 0, coursesData.CourseId);
                        }
                        else
                        {
                            transaction.Rollback();
                            return isMappedCourseDeleted;
                        }
                        transaction.Commit();
                        return isMappedCourseDeleted;
                    }
                    catch(Exception excep)
                    {
                        transaction.Rollback();
                        throw excep;
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
