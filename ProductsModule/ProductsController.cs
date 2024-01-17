using Microsoft.AspNetCore.Authorization;
using Core;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A custom request class.
/// </summary>
public class CustomRequest
{
	/// <summary>
	/// The ID of the user.
	/// </summary>
	/// <example>123</example>
	public int UserId { get; set; }

	/// <summary>
	/// The name of the user.
	/// </summary>
	/// <example>John Doe</example>
	public string UserName { get; set; }

	// Add other properties with examples as needed
}

namespace ProductsModule
{
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
