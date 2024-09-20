using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
  [Route("api1")]
  [ApiController]
  public class TestsController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      string headerValue = HttpContext.Request.Headers["api1-request-header"];

      return Ok("API1 " + headerValue);
    }


    [HttpGet("products/{id}")]
    public IActionResult Get(int id)
    {
      return Ok("GET Products By Id" + id);
    }

  


  }
}
