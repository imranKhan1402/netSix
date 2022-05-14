using Models.Custom;
using Models.Model.User;
using OracleClient.DBO;
using OracleClient.dboLink;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserService : IUserService
    {
        //OracleDatabaseManager odm = new OracleDatabaseManager();
        OracleDbContext odc = new OracleDbContext();

        public UserService()
        { }
        public IEnumerable<BG_MUSERS> GetAllUser()
        {
            try
            {
                var Query = @"SELECT 
                        USER_TEXT, USER_NAME, USER_PASS, 
                        USER_DEPT, USER_MAIL, USER_MOBL, 
                        USER_IMAG, USER_ROLE, ACT, 
                        CDT, UDT, CDU, 
                        UDU, VAR
                        FROM PICNIC.BG_MUSERS";
                var data = odc.ExecuteQuery(Query, 1); //odm.ExecuteQuery(Query); //_dbContext.GetDataTable(Query.ToString());
                return from DataRow row in data.Rows select BG_MUSERS.ConvertToModel(row);
                #region No Need
                //Tuple<DataTable, CAResult> _tpl = DatabaseOracleLink.GetDataTable(1, Query);
                //if (_tpl.Item2.SUCCESS)
                //{
                //    DataTable dataTable = _tpl.Item1;
                //    return from DataRow row in dataTable.Rows select BG_MUSERS.ConvertToModel(row);
                //}
                //else
                //    return null; 
                #endregion
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}