using Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HelloWorldSwashbuckle
{
	public class SwaggerTagOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var actionTags = context.MethodInfo.GetCustomAttributes(true)
				.OfType<SwaggerTagAttribute>()
				.SelectMany(attr => attr.Tags)
				.Distinct();

			var controllerTags = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
			.OfType<SwaggerTagAttribute>()
			.SelectMany(attr => attr.Tags)
			.Distinct();

			var allTags = actionTags.Union(controllerTags ?? Array.Empty<string>()).Distinct();

			if (allTags.Any())
			{
				operation.Tags = allTags.Select(tag => new OpenApiTag { Name = tag }).ToList();
			}
		}
	}
}
