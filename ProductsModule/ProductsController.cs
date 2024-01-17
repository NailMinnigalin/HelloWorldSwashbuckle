using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class CustomRequest
{
	/// <example>123</example>
	public int UserId { get; set; }

	/// <example>John Doe</example>
	public string UserName { get; set; }
}

namespace ProductsModule
{
	[SwaggerTag("ProductsController")]
	[SwaggerGroup("products")]
	[Authorize]
	[ApiController]
	[Route("[controller]/[action]")]
	public class ProductsController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllProducts()
		{
			// Implement your logic here
			return Ok(new[] { "Product1", "Product2" });
		}

		/// <param name="x" example="5"></param>
		[HttpGet]
		public IActionResult MultiplyBy2(int x)
		{
			return Ok(x * 2);
		}

		[HttpPost]
		public IActionResult CustomEndpoint(CustomRequest request)
		{
			// Your logic here
			return Ok();
		}
	}
}
