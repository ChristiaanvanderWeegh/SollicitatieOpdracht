using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OAuth2Api.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class WebApiController : Controller
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello REST API", "I am Authorized" };
        }

        public string Get(int id)
        {
            return "Hello Authorized API with ID = " + id;
        }

        public void Post([FromBody] string value)
        {
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
