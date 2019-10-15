using System.Web.Http;

namespace WebAPI.Check.Controllers
{
    public class ConnectionController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Successful connection.");
        }
    }
}
