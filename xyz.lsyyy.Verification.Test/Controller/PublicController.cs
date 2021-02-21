using Microsoft.AspNetCore.Mvc;
using xyz.lsyyy.Verification.Extension;

namespace xyz.lsyyy.Verification.Test
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PublicController : ControllerBase
	{
		[HttpGet]
		[AuthorizationTag(Name = "hello world")]
		public object Hello()
		{
			return Ok("hello world");
		}
	}
}
