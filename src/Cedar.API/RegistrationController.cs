using Microsoft.AspNetCore.Mvc;

namespace Cedar.API
{
    using System.Threading.Tasks;
    using Models;

    [Route("api/registration")]
    public class RegistrationController : Controller
    {
        public RegistrationController()
        {
            
        }


        [HttpPost]
        [Route("")]
        public  ActionResult Register([FromBody] RegistrationRequest input)
        {
            if (input == null)
            {
                return BadRequest("No data provided");
            }
            
            //TODO: Add logic to store the information

            return Ok(null);
        }
    }
}