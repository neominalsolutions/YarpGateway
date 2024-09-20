using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
  [Route("api1")]
  [ApiController]
  public class TestsController : ControllerBase
  {

    private readonly HttpClient client;

    public TestsController(IHttpClientFactory httpClientFactory)
    {
      client = httpClientFactory.CreateClient("api2");
    }

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

    [HttpGet("suppliers")]
    public async Task<IActionResult> SendRequest()
    {
      var data = await  this.client.GetStringAsync("/api2/suppliers?name=ali&company=tskb");

      return Ok(data);
    }




  }
}
