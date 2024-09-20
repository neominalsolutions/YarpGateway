using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
  [Route("api2")]
  [ApiController]
  public class TestsController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("API2");
    }

    [HttpGet("suppliers")]
    public IActionResult Get(string name,string company)
    {
      return Ok($"GET Suppliers {company}/{name}");
    }

    //[Authorize(Policy = "Authenticated")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("authUser")] // api2/authUser
    public IActionResult AuthenticatedUser()
    {
      return Ok("AuthenticatedUser");
    }



  }
}
