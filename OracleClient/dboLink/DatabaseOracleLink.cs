using Models.Custom;
using OracleClient.DBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleClient.dboLink
{
    public static class DatabaseOracleLink
    {
        #region Oracle_DB_Method
        public static Tuple<DataTable, CAResult> GetDataTable(int ApplicationId,string sql, object[] _parameters = null, string _con = "")
        {
            return DatabaseOracle.ExecuteQuery(ApplicationId, _command: sql, _parameters: _parameters);
        }
        public static CAResult PostSqlList(int ApplicationId, List<string> sqlList, string _con = "")
        {
            return DatabaseOracle.ExecuteNonQueryList(ApplicationId, _command_list: sqlList);
        }
        public static CAResult PostSql(int ApplicationId, string sql, string _con = "")
        {
            return DatabaseOracle.ExecuteNonQuery(ApplicationId, _command: sql);
        }
        public static CAResult PostSqlOut(int ApplicationId, string sql, string _out, string _con = "")
        {
            return DatabaseOracle.ExecuteNonQueryOut(ApplicationId, _command: sql, _outId: _out);
        }
        #endregion


        #region Oracle_DB_Method_SP
        public static Tuple<DataSet, CAResult> GetDataSetSP(int ApplicationId, string sql, object[] _in_param, object[] _out_param, string _con = "")
        {
            return DatabaseOracle.ExecuteSPQuery(ApplicationId, _command: sql, _in_parameters: _in_param, _out_parameters: _out_param);
        }
        public static CAResult PostSP(int ApplicationId, string sql, object[] _in_param, string _con = "")
        {
            return DatabaseOracle.ExecuteSPNonQuery(ApplicationId, _command: sql, _in_parameters: _in_param);
        }
        #endregion
    }
}
