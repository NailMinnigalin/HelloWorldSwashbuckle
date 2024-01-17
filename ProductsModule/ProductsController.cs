﻿using Microsoft.AspNetCore.Authorization;
using Core;
using Microsoft.AspNetCore.Mvc;

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

		// Additional actions and methods
	}
}
