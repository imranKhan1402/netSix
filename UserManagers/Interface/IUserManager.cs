using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagers.Interface
{
    public interface IUserManager
    {
        IEnumerable<BG_MUSERS> GetAllUser();
    }
}
