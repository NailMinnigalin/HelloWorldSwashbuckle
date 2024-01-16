using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace ProductsModule
{
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
		[SwaggerRequestExample(typeof(int), typeof(MultiplyBy2Example))]
		[HttpGet]
		public IActionResult MultiplyBy2(int x)
		{
			return Ok(x * 2);
		}

		// Additional actions and methods
	}
}
