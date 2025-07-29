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
    public class StudentRegistrationDAL
    {

        public static bool CheckCountOfStudent(Database db, string emailId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT COUNT(EMAIL) AS USERCOUNT FROM LDB1_USER ");
            sqlCmdBuilder.Append("WHERE EMAIL=@EMAIL");


            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@EMAIL", DbType.AnsiString, emailId);


            bool isExist = false;
            DataTable dt = db.ExecuteDataSet(dbCmd).Tables[0];
            
            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
            {
                isExist = true;    
            }
            return isExist;
        }

        public static IDataReader InsertDataUsertbl(Database db, DbTransaction transaction, string fullName, string userName, int sClass, string emailId, string gender, string phoneNum, string password)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("INSERT INTO LDB1_USER ");
            sqlCmdBuilder.Append("(FULLNAME, USERNAME, CLASS, EMAIL, GENDER, PHONENUM, PASSWORD) ");
            sqlCmdBuilder.Append("VALUES (@FULLNAME, @USERNAME, @CLASS, @EMAIL, @GENDER, @PHONENUM, @PASSWORD)");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@FULLNAME", DbType.AnsiString, fullName);
            db.AddInParameter(dbCmd, "@USERNAME", DbType.AnsiString, userName);
            db.AddInParameter(dbCmd, "@CLASS", DbType.Int32, sClass);
            db.AddInParameter(dbCmd, "@EMAIL", DbType.AnsiString, emailId);
            db.AddInParameter(dbCmd, "@GENDER", DbType.AnsiString, gender);
            db.AddInParameter(dbCmd, "@PHONENUM", DbType.AnsiString, phoneNum);
            db.AddInParameter(dbCmd, "@PASSWORD", DbType.AnsiString, password);

            if (transaction == null)
                db.ExecuteNonQuery(dbCmd);
            else
                db.ExecuteNonQuery(dbCmd, transaction);

            DbCommand dbCmdId = db.GetSqlStringCommand("SELECT LAST_INSERT_ID() AS SID");
            dbCmdId.CommandType = CommandType.Text;

            return db.ExecuteReader(dbCmdId, transaction);

            //return true;
        }

        public static IDataReader GetAllCourses(Database db)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT CID, COURSENAME FROM TBLCOURSES ");
            sqlCmdBuilder.Append("WHERE FSTATUS='A' ");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            return db.ExecuteReader(dbCmd);
        }

        //public static IDataReader GetStudentId(Database db, DbTransaction transaction, string emailId)
        //{
        //    StringBuilder sqlCmdBuilder = new StringBuilder();
        //    sqlCmdBuilder.Append("SELECT SID FROM LDB1_USER ");
        //    sqlCmdBuilder.Append("WHERE STATUS!='I' ");
        //    sqlCmdBuilder.Append("AND EMAIL=@EMAIL");

        //    DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
        //    dbCmd.CommandType = CommandType.Text;

        //    db.AddInParameter(dbCmd, "@EMAIL", DbType.AnsiString, emailId);

        //    return db.ExecuteReader(dbCmd, transaction);
        //    //return Convert.ToInt32(db.ExecuteScalar(dbCmd, transaction));
        //}

        public static bool InsertSelectedCourses(Database db, DbTransaction transaction, int studentId, int courseId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("INSERT INTO LDB1_SELECTEDCOURSES ");
            sqlCmdBuilder.Append("(SID, COURSEID) ");
            sqlCmdBuilder.Append("VALUES (@SID, @COURSEID)");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@SID", DbType.Int32, studentId);
            db.AddInParameter(dbCmd, "@COURSEID", DbType.Int32, courseId);

            db.ExecuteNonQuery(dbCmd, transaction);
            return true;
        }
    }
}
