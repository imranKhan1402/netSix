using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagers.Interface;

namespace UserManagers.Repository
{
    public class UserManager : IUserManager
    {
        //DBORCL odb = new DBORCL();
        private readonly IUserService iUserService;
        public UserManager()
        {
            iUserService = new UserService();
        }

        public IEnumerable<BG_MUSERS> GetAllUser()
        {
            try
            {
                //odb.BeginTransaction();
                var DataList = iUserService.GetAllUser();
                return DataList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
    }
}
