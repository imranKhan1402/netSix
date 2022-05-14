using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleClient.DBO
{
    public class OracleDbContext : IDisposable
    {
        #region Private Property
        private OracleConnection _cn;
        private static OracleConnection _icn;
        private DataTable _dt;
        private OracleCommand _cmd;
        private OracleDataReader _reader;
        private Dictionary<string, object> ParamList;

        private static OracleConnection _con;
        private static string PICNIC;
        private static string YA;
        #endregion

        public OracleDbContext()
        {
            //string x = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.8.105)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=pran)));PASSWORD=9552312;USER ID=RFL;Unicode=True;pooling=False;";
            string x = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.4.199)(PORT=9107))(CONNECT_DATA=(SERVICE_NAME=pran)));PASSWORD=picnice2021Imran;USER ID=PICNIC;pooling=False;";
            PICNIC = x;

            string x1 = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.17.4.199)(PORT=9107))(CONNECT_DATA=(SERVICE_NAME=pran)));PASSWORD=YA;USER ID=ya;pooling=False;";
            YA = x1;
        }



        public static string IsConstring(int ApplicationId)
        {
            if (ApplicationId.Equals(1))
            {
                return PICNIC;
            }
            else if (ApplicationId.Equals(2))
            {
                return YA;
            }
            return "";
        }


        #region -- Dispose --

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cn != null)
                {
                    _cn.Dispose();
                    _cn = null;
                }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_cmd != null)
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        ~OracleDbContext()
        {
            Dispose(false);
        }

        #endregion

        public string ExecuteNonQuery(string strSql, int ApplicationId)
        {
            try
            {
                string intResult = "Success";
                using (_cn = new OracleConnection(IsConstring(ApplicationId)))
                {
                    _cn.Open();
                    using (_cmd = new OracleCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        _cmd.ExecuteNonQuery();
                    }
                }
                return intResult;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }

        public DataTable ExecuteQuery(string strSql, int ApplicationId)
        {
            try
            {
                using (_cn = new OracleConnection(IsConstring(ApplicationId)))
                {
                    _cn.Open();
                    using (_cmd = new OracleCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;
                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch (Exception ex)
                            {
                                string error = ex.Message;
                            }
                        }
                    }
                }
                return _dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _cn.Dispose();
                _cn.Close();
                OracleConnection.ClearPool(_cn);
            }
        }


        public DataSet IDataSetPro(string ProName, string[][] aParaNameValue, string tblName, int ApplicationId = 0)
        {
            try
            {
                using (_icn = new OracleConnection(IsConstring(ApplicationId)))
                {
                    _icn.Open();
                    OracleCommand _icmd = new OracleCommand(ProName, _icn);
                    _icmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter oraPara;
                    for (int j = 0; j <= aParaNameValue.GetUpperBound(0); j++)
                    {
                        oraPara = new OracleParameter(aParaNameValue[j][0].ToString(), OracleDbType.NVarchar2);
                        oraPara.Direction = ParameterDirection.Input;
                        oraPara.Value = aParaNameValue[j][1].ToString();
                        _icmd.Parameters.Add(oraPara);
                    }

                    oraPara = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);
                    oraPara.Direction = ParameterDirection.Output;
                    _icmd.Parameters.Add(oraPara);
                    OracleDataAdapter odAdapter = new OracleDataAdapter();
                    odAdapter = new OracleDataAdapter(_icmd);
                    DataSet dSet = new DataSet("dSet");
                    dSet = new DataSet();
                    odAdapter.Fill(dSet, tblName);
                    return dSet;
                }

            }
            catch (Exception Ex)
            {
                // ErrorSave(sql, Ex.Message.ToString());
                //Ex = Ex.Message.ToString();
                return null;
            }
            finally
            {
                _icn.Dispose();
                _icn.Close();
                OracleConnection.ClearPool(_icn);
            }
        }


        #region -- Execute Datatable with Procedure --

        //private bool? HasTransaction { get; set; }
        //private OracleTransaction Transaction { get; set; }


        //public DataTable FillDataTable(string procedureName, List<OracleParameter> paramList, int ApplicationId = 0)
        //{
        //    DataTable dtResult = null;
        //    OracleDataAdapter dbAdapter = null;

        //    using (_cn = new OracleConnection(IsConstring(ApplicationId)))
        //    {
        //        using (OracleCommand Command = new OracleCommand())
        //        {
        //            try
        //            {
        //                if (this._cn == null || this._cn.State != ConnectionState.Open) this._cn.Open();

        //                Command.Connection = this._cn;
        //                Command.CommandText = procedureName;
        //                Command.CommandType = CommandType.StoredProcedure;
        //                Command.CommandTimeout = int.MaxValue;// _connectionTimeOut;

        //                if (this.HasTransaction.HasValue && this.HasTransaction.Value) Command.Transaction = this.Transaction;


        //                foreach (OracleParameter item in paramList)
        //                {
        //                    Command.Parameters.Add(item);
        //                }
        //                dbAdapter = new OracleDataAdapter(Command);
        //                dtResult = new DataTable(procedureName);

        //                dbAdapter.Fill(dtResult);
        //            }
        //            catch (Exception exception)
        //            {
        //                throw new Exception(exception.Message);
        //            }
        //            finally
        //            {
        //                if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
        //                    if (this._cn != null && this._cn.State == ConnectionState.Open) this._cn.Open();

        //                dbAdapter.Dispose();
        //            }
        //        }
        //    }
        //    return dtResult;
        //}

        //public int ExecuteNonQuery(string procedureName, List<OracleParameter> paramList = null, int ApplicationId=0)
        //{
        //    int records = 0;
        //    using (_cn = new OracleConnection(IsConstring(ApplicationId)))
        //    {
        //        using (OracleCommand Command = new OracleCommand(IsConstring(ApplicationId)))
        //        {
        //            try
        //            {
        //                if (this._cn == null || this._cn.State != ConnectionState.Open) this._cn.Open();

        //                Command.Connection = this._cn;
        //                Command.CommandText = procedureName;
        //                Command.CommandType = CommandType.StoredProcedure;
        //                Command.CommandTimeout = int.MaxValue; //_connectionTimeOut;
        //                if (this.HasTransaction.HasValue && this.HasTransaction.Value) Command.Transaction = this.Transaction;

        //                foreach (OracleParameter item in paramList)
        //                {
        //                    Command.Parameters.Add(item);
        //                }

        //                records=Command.ExecuteNonQuery();
        //            }
        //            catch (Exception exception)
        //            {
        //                throw new Exception(exception.Message);
        //            }
        //            finally
        //            {
        //                if (!this.HasTransaction.HasValue || (this.HasTransaction.HasValue && !this.HasTransaction.Value))
        //                    if (this._cn != null && this._cn.State == ConnectionState.Open) this._cn.Close();
        //            }
        //        }                
        //    }
        //    return records;
        //}

        #endregion
    }
}
