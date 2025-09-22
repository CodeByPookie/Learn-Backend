using Microsoft.AspNetCore.Mvc;

namespace SampleWebApi.Controllers
{
	[Route("custom/[controller]")]
	[ApiController]
	public class HelloWorldController : ControllerBase
	{
		[HttpGet("hello")]
		public IActionResult Hello()
		{
			return Ok(new { message = "Hello from Sample API!" });
		}
	}	
}
