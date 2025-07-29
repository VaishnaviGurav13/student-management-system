using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace StudentManagement_Service_DAL
{
    public class StudentProfileDAL
    {
        public static IDataReader GetStudentData(Database db, int studentId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT * FROM LDB1_USER u ");
            sqlCmdBuilder.Append("INNER JOIN LDB1_SELECTEDCOURSES sc ON u.SID=sc.SID ");
            sqlCmdBuilder.Append("WHERE sc.SID=@SID AND FSTATUS='A' ");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

            return db.ExecuteReader(dbCmd);
        }

        //public static IDataReader GetDataFromUsertbl(Database db, int studentId, string status, int limit, int offset)
        //{
        //    StringBuilder sqlCmdBuilder = new StringBuilder();
        //    sqlCmdBuilder.Append("SELECT SID, FULLNAME, CLASS, EMAIL, GENDER, STATUS, PHONENUM, ROLE FROM LDB1_USER ");
        //    sqlCmdBuilder.Append("WHERE STATUS!='I' ");
        //    sqlCmdBuilder.Append("AND ROLE='Student' ");

        //    if (studentId != 0)
        //        sqlCmdBuilder.Append("AND SID=@SID");

        //    if (!string.IsNullOrEmpty(status))
        //    {
        //        sqlCmdBuilder.Append("AND STATUS=@STATUS ");
        //    }
        //    if (limit != 0)
        //    {
        //        sqlCmdBuilder.Append("LIMIT @LIMIT OFFSET @OFFSET");
        //    }


        //    DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
        //    dbCmd.CommandType = CommandType.Text;

        //    if (studentId!=0)
        //        db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
        //    if (!string.IsNullOrEmpty(status))
        //    {
        //        db.AddInParameter(dbCmd, "@STATUS", DbType.AnsiString, status);
        //        //db.AddInParameter(dbCmd, "@LIMIT", DbType.Int32, limit);
        //        //db.AddInParameter(dbCmd, "@OFFSET", DbType.Int32, offset);
        //    }
        //    if (limit != 0)
        //    {
        //        db.AddInParameter(dbCmd, "@LIMIT", DbType.Int32, limit);
        //        db.AddInParameter(dbCmd, "@OFFSET", DbType.Int32, offset);
        //    }

        //    return db.ExecuteReader(dbCmd);
        //}

        public static DataTable GetSelectedCourses(Database db, int studentId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT COURSEID FROM LDB1_SELECTEDCOURSES ");
            sqlCmdBuilder.Append("WHERE FSTATUS='A' ");
            sqlCmdBuilder.Append("AND SID=@SID");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

            return db.ExecuteDataSet(dbCmd).Tables[0];
            
        }

        public static bool UpdateStudentData(Database db, DbTransaction transaction, int studentId, string fullName, int sClass, string gender, string emailId, string phoneNum)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("UPDATE LDB1_USER ");
            sqlCmdBuilder.Append("SET FULLNAME=@FULLNAME, CLASS=@CLASS, GENDER=@GENDER, EMAIL=@EMAIL, PHONENUM=@PHONENUM ");
            sqlCmdBuilder.Append("WHERE SID=@SID");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@FULLNAME", DbType.AnsiString, fullName);
            db.AddInParameter(dbCmd, "@CLASS", DbType.Int32, sClass);
            db.AddInParameter(dbCmd, "@GENDER", DbType.AnsiString, gender);
            db.AddInParameter(dbCmd, "@EMAIL", DbType.AnsiString, emailId);
            db.AddInParameter(dbCmd, "@PHONENUM", DbType.AnsiString, phoneNum);
            //db.AddInParameter(dbCmd, "@USERNAME", DbType.AnsiString, userName);
            //db.AddInParameter(dbCmd, "@PASSWORD", DbType.AnsiString, password);
            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

            db.ExecuteNonQuery(dbCmd, transaction);
            return true;
        }

        public static bool UpdateCourseFstatus(Database db, DbTransaction transaction, string fStatus, int studentId, int courseId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("UPDATE LDB1_SELECTEDCOURSES SET FSTATUS=@FSTATUS ");

            if(studentId!=0)
                sqlCmdBuilder.Append("WHERE SID=@SID ");

            if (courseId != 0)
                if(studentId==0)
                    sqlCmdBuilder.Append("WHERE COURSEID=@COURSEID");
                else
                    sqlCmdBuilder.Append("AND COURSEID=@COURSEID");


            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@FSTATUS", DbType.AnsiString, fStatus);

            if (studentId != 0)
                db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

            if (courseId != 0)
                db.AddInParameter(dbCmd, "@COURSEID", DbType.Int32, courseId);

            db.ExecuteNonQuery(dbCmd, transaction);
            return true;
        }

        public static bool CheckCourseCount(Database db, DbTransaction transaction, int studentId, int courseId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT COUNT(SID) FROM LDB1_SELECTEDCOURSES ");
            sqlCmdBuilder.Append("WHERE SID=@SID AND COURSEID=@COURSEID");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
            db.AddInParameter(dbCmd, "@COURSEID", DbType.Int32, courseId);

            bool isCourseExist = false;
            DataTable dt = db.ExecuteDataSet(dbCmd).Tables[0];
            if(dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                isCourseExist = true;
            }
            return isCourseExist;
        }

        //public static bool DeletePreviousCourses(Database db, DbTransaction transaction, int studentId, int courseId)
        //{
        //    StringBuilder sqlCmdBuilder = new StringBuilder();
        //    sqlCmdBuilder.Append("DELETE FROM LDB1_SELECTEDCOURSES ");
        //    sqlCmdBuilder.Append("WHERE SID=@SID");

        //    DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
        //    dbCmd.CommandType = CommandType.Text;

        //    db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

        //    db.ExecuteNonQuery(dbCmd, transaction);
        //    return true;
        //}

        //public static bool InsertNewCourses(Database db, DbTransaction transaction, int studentId, int courseId)
        //{
        //    StringBuilder sqlCmdBuilder = new StringBuilder();
        //    sqlCmdBuilder.Append("INSERT INTO LDB1_SELECTEDCOURSES ");
        //    sqlCmdBuilder.Append("(SID, COURSEID) VALUES (@SID, @COURSEID)");

        //    DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
        //    dbCmd.CommandType = CommandType.Text;

        //    db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
        //    db.AddInParameter(dbCmd, "@COURSEID", DbType.Int32, courseId);
        //    db.ExecuteNonQuery(dbCmd, transaction);
        //    return true;
        //}
    }
}
