using Models.Custom;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleClient.DBO
{
    public static class DatabaseOracle 
    {
        public static string IsConstring(int ApplicationId)
        {
            if (ApplicationId.Equals(1))
            {
                string PICNIC = "";
                return PICNIC;
            }
            else if (ApplicationId.Equals(2))
            {
                string YA = "";
                return YA;
            }
            return "";
        }
        public static CAResult ExecuteNonQueryList(int ApplicationId, List<string> _command_list)
        {
            CAResult result = new CAResult();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            OracleCommand cmd = new OracleCommand();
            OracleTransaction trn;
            con.Open();
            cmd.Connection = con;
            cmd.CommandTimeout = int.MaxValue;
            trn = con.BeginTransaction();
            cmd.Transaction = trn;
            try
            {
                int r = 0;
                foreach (string _command in _command_list)
                {
                    cmd.CommandText = _command;
                    r += cmd.ExecuteNonQuery();
                }
                trn.Commit();

                result.SUCCESS = true;
                result.MESSAGES = CAMessage.SUCCESS_MESSAGES;
                result.ROWS = r;
            }
            catch (OracleException ex)
            {
                trn.Rollback();
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static CAResult ExecuteNonQuery(int ApplicationId, string _command)
        {
            CAResult result = new CAResult();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            OracleCommand cmd = new OracleCommand();
            OracleTransaction trn;
            con.Open();
            cmd.Connection = con;
            cmd.CommandTimeout = int.MaxValue;
            trn = con.BeginTransaction();
            cmd.Transaction = trn;
            try
            {
                cmd.CommandText = _command;
                result.ROWS = cmd.ExecuteNonQuery();
                trn.Commit();

                result.SUCCESS = true;
                result.MESSAGES = CAMessage.SUCCESS_MESSAGES;
            }
            catch (OracleException ex)
            {
                trn.Rollback();
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static CAResult ExecuteNonQueryListSP(int ApplicationId, List<string> _command_list)
        {
            CAResult result = new CAResult();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandTimeout = int.MaxValue;

            try
            {
                string _query = "BEGIN ";
                foreach (string command in _command_list)
                {
                    _query += command + ";";
                }
                _query += "COMMIT;END;";
                cmd.CommandText = _query;
                result.ROWS = cmd.ExecuteNonQuery();

                result.SUCCESS = true;
                result.MESSAGES = CAMessage.SUCCESS_MESSAGES;
            }
            catch (OracleException ex)
            {
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// _outId >>> id_param
        /// </summary>
        /// <param name="IsConstring(ApplicationId)"></param>
        /// <param name="_command"></param>
        /// <param name="_outId"></param>
        /// <returns></returns>
       // public static Tuple<string, string> ExecuteNonQueryOut(int ApplicationId, string _command, string _outId)
        public static CAResult ExecuteNonQueryOut(int ApplicationId, string _command, string _outId)
        {
            CAResult result = new CAResult();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            OracleCommand cmd = new OracleCommand();
            OracleTransaction trn;
            con.Open();
            cmd.Connection = con;
            trn = con.BeginTransaction();
            cmd.Transaction = trn;
            try
            {
                cmd.CommandText = _command;
                OracleParameter outputParameter = new OracleParameter(_outId, OracleDbType.Int64);
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);
                result.ROWS = cmd.ExecuteNonQuery();
                result.MESSAGES = outputParameter.Value.ToString();
                trn.Commit();

                result.SUCCESS = true;
            }
            catch (OracleException ex)
            {
                trn.Rollback();
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public static Tuple<DataTable, CAResult> ExecuteQuery(int ApplicationId, string _command, object[] _parameters = null)
        {
            CAResult result = new CAResult();
            result.SUCCESS = true;
            result.MESSAGES = CAMessage.SUCCESS_MESSAGES;

            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            try
            {
                OracleCommand cmd = new OracleCommand(_command, con);
                if (_parameters != null)
                {
                    cmd.Parameters.AddRange(_parameters);
                }
                cmd.CommandTimeout = int.MaxValue;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (OracleException ex)
            {
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
                return new Tuple<DataTable, CAResult>(new DataTable(), result);
            }
            finally
            {
                con.Close();
            }
            result.ROWS = ds.Tables[0].Rows.Count;
            return new Tuple<DataTable, CAResult>(ds.Tables[0], result);
        }


        public static Tuple<DataSet, CAResult> ExecuteSPQuery(int ApplicationId, string _command, object[] _in_parameters, object[] _out_parameters)
        {
            CAResult result = new CAResult();
            result.SUCCESS = true;
            result.MESSAGES = CAMessage.SUCCESS_MESSAGES;

            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter();
            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            try
            {
                OracleCommand cmd = new OracleCommand(_command, con);
                if (_in_parameters != null)
                {
                    cmd.Parameters.AddRange(_in_parameters);
                }
                if (_out_parameters != null)
                {
                    cmd.Parameters.AddRange(_out_parameters);
                }
                cmd.CommandTimeout = int.MaxValue;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (OracleException ex)
            {
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
                return new Tuple<DataSet, CAResult>(new DataSet(), result);
            }
            finally
            {
                con.Close();
            }
            result.ROWS = ds.Tables.Count;
            return new Tuple<DataSet, CAResult>(ds, result);
        }
        public static CAResult ExecuteSPNonQuery(int ApplicationId, string _command, object[] _in_parameters)
        {
            CAResult result = new CAResult();
            result.SUCCESS = true;
            result.MESSAGES = CAMessage.SUCCESS_MESSAGES;

            OracleConnection con = new OracleConnection(IsConstring(ApplicationId));
            OracleCommand cmd = new OracleCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandTimeout = int.MaxValue;
            try
            {
                if (_in_parameters != null)
                {
                    cmd.Parameters.AddRange(_in_parameters);
                }
                cmd.CommandText = _command;
                result.ROWS = cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                result.SUCCESS = false;
                result.MESSAGES = ex.Message;
                result.ROWS = 0;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}
