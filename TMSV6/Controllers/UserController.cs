using Managers.Interface;
using Managers.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TMSV6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserManager _iuserManager = new UserManager();
        // GET api/<controller>
        //get All
        [HttpGet]
        public string GetAllEmployee()
        {
            string result = string.Empty;
            try
            {
                var data = _iuserManager.GetAllUser();
                result = JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }
            return result;
        }
    }
}
