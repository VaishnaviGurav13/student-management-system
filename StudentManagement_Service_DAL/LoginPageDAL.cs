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
    public class LoginPageDAL
    {

        public static IDataReader GetUserDataUsertbl(Database db, string emailId)
        {
            StringBuilder sqlCmdBuilder = new StringBuilder();
            sqlCmdBuilder.Append("SELECT SID, EMAIL, STATUS, ROLE, PASSWORD ");
            sqlCmdBuilder.Append("FROM LDB1_USER ");
            //sqlCmdBuilder.Append("WHERE STATUS!='I' ");
            //sqlCmdBuilder.Append("AND EMAIL=@EMAIL");
            sqlCmdBuilder.Append("WHERE EMAIL=@EMAIL");

            DbCommand dbCmd = db.GetSqlStringCommand(sqlCmdBuilder.ToString());
            dbCmd.CommandType = CommandType.Text;

            db.AddInParameter(dbCmd, "@EMAIL", DbType.AnsiString, emailId);
            return db.ExecuteReader(dbCmd);
        }

    }
}
