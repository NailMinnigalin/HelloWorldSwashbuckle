using Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(ProductsModule.ProductsController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
List<string> groups = new();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("core", new OpenApiInfo { Title = "Core API", Version = "v1" });
	c.SwaggerDoc("products", new OpenApiInfo { Title = "Products API", Version = "v1" });

	c.DocInclusionPredicate((docName, apiDesc) =>
	{
		if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
		{
			return false;
		}

		var groupAttr = methodInfo.DeclaringType.GetCustomAttribute<SwaggerGroupAttribute>();
		if (groupAttr == null)
		{
			return false;
		}

		return groupAttr.GroupName == docName;
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI( c =>
	{
		c.SwaggerEndpoint("/swagger/core/swagger.json", "Core API");
		c.SwaggerEndpoint("/swagger/products/swagger.json", "Products API");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
