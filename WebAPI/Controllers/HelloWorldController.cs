using Microsoft.AspNetCore.Mvc;
using DAL;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private DAL.APIDbContext _context;

        public  HelloWorldController(APIDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public string Get()
        {
            var l = _context.User.Select(o => o.UserName == "Hello World");
            // do something
            return "Hello World";
        }
    }
}
