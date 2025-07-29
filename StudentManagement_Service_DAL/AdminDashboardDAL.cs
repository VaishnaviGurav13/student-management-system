using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace StudentManagement_Service_DAL
{
    public class AdminDashboardDAL
    {
        public static IDataReader GetStudentStatusCount(Database db)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT STATUS, COUNT(STATUS) AS USERCOUNT FROM LDB1_USER ");
            sqlCmdBuilder.Append("WHERE STATUS!='I' ");
            sqlCmdBuilder.Append("AND ROLE='STUDENT' ");
            sqlCmdBuilder.Append("GROUP BY STATUS");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            return db.ExecuteReader(dbCmd);
        }

        public static IDataReader GetCourseCount(Database db, string courseName)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            //sqlCmdBuilder.Append("SELECT COUNT(COURSENAME) AS COURSECOUNT FROM TBLCOURSES ");
            //sqlCmdBuilder.Append("WHERE FSTATUS='A' ");

            sqlCmdBuilder.Append("SELECT FSTATUS, COUNT(COURSENAME) AS COURSECOUNT FROM TBLCOURSES ");

            if (!string.IsNullOrEmpty(courseName))
            {
                sqlCmdBuilder.Append("WHERE COURSENAME=@COURSENAME ");
            }

            sqlCmdBuilder.Append("GROUP BY FSTATUS");

            //if (!string.IsNullOrEmpty(courseName))
            //{
            //    sqlCmdBuilder.Append("AND COURSENAME=@COURSENAME");
            //}

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            if (!string.IsNullOrEmpty(courseName))
            {
                db.AddInParameter(dbCmd, "@COURSENAME", DbType.AnsiString, courseName);
            }

            return db.ExecuteReader(dbCmd);
        }

        public static IDataReader GetDataFromUsertbl(Database db, int studentId, string status, int limit, int offset)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT SID, FULLNAME, CLASS, EMAIL, GENDER, STATUS, PHONENUM, ROLE FROM LDB1_USER ");
            sqlCmdBuilder.Append("WHERE STATUS!='I' ");
            sqlCmdBuilder.Append("AND ROLE='Student' ");

            if (studentId != 0)
                sqlCmdBuilder.Append("AND SID=@SID");

            if (!string.IsNullOrEmpty(status))
            {
                sqlCmdBuilder.Append("AND STATUS=@STATUS ");
            }
            if (limit != 0)
            {
                sqlCmdBuilder.Append("LIMIT @LIMIT OFFSET @OFFSET");
            }


            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            if (studentId != 0)
                db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
            if (!string.IsNullOrEmpty(status))
            {
                db.AddInParameter(dbCmd, "@STATUS", DbType.AnsiString, status);
                //db.AddInParameter(dbCmd, "@LIMIT", DbType.Int32, limit);
                //db.AddInParameter(dbCmd, "@OFFSET", DbType.Int32, offset);
            }
            if (limit != 0)
            {
                db.AddInParameter(dbCmd, "@LIMIT", DbType.Int32, limit);
                db.AddInParameter(dbCmd, "@OFFSET", DbType.Int32, offset);
            }

            return db.ExecuteReader(dbCmd);
        }

        public static bool UpdateStudentStatus(Database db, DbTransaction transaction, int studentId, string status)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("UPDATE LDB1_USER SET STATUS=@STATUS ");
            sqlCmdBuilder.Append("WHERE SID=@SID");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@STATUS", DbType.AnsiString, status);
            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);

            if (transaction == null)
                db.ExecuteNonQuery(dbCmd);
            else
                db.ExecuteNonQuery(dbCmd, transaction);
            return true;
        }

        //public static bool InactiveStudentData(Database db, DbTransaction transaction, int studentId)
        //{
        //    StringBuilder sqlCmdBuilder = new StringBuilder();
        //    sqlCmdBuilder.Append("UPDATE LDB1_USER ");
        //    sqlCmdBuilder.Append("SET FSTATUS='I' ");
        //    sqlCmdBuilder.Append("WHERE SID=@SID");

        //    DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
        //    dbCmd.CommandType = CommandType.Text;

        //    db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
        //    db.ExecuteNonQuery(dbCmd, transaction);

        //    return true;
        //}

        public static bool InsertDataCoursetbl(Database db, string courseName)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("INSERT INTO TBLCOURSES ");
            sqlCmdBuilder.Append("(COURSENAME) ");
            sqlCmdBuilder.Append("VALUES (@COURSENAME)");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@COURSENAME", DbType.AnsiString, courseName);

            db.ExecuteNonQuery(dbCmd);
            return true;
        }

        public static bool ChangeCourseFstatus(Database db, DbTransaction transaction, string fStatus, string courseName)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("UPDATE TBLCOURSES SET FSTATUS=@FSTATUS ");
            sqlCmdBuilder.Append("WHERE COURSENAME=@COURSENAME");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@FSTATUS", DbType.AnsiString, fStatus);
            db.AddInParameter(dbCmd, "@COURSENAME", DbType.AnsiString, courseName);

            if (transaction == null)
                db.ExecuteNonQuery(dbCmd);
            else
                db.ExecuteNonQuery(dbCmd, transaction);
            return true;
        }
    }
}
