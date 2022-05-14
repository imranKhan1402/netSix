using Models.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Interface
{
    public interface IUserManager
    {
        IEnumerable<BG_MUSERS> GetAllUser();
    }
}
