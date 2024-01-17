using Core;
using HelloWorldSwashbuckle;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddApplicationPart(typeof(ProductsModule.ProductsController).Assembly);

builder.Services.AddEndpointsApiExplorer();
List<string> groups = new();
builder.Services.AddSwaggerGen(c =>
{
	// Создание документов под проекты
	c.SwaggerDoc("core", new OpenApiInfo { Title = "Core API", Version = "v1" });
	c.SwaggerDoc("products", new OpenApiInfo { Title = "Products API", Version = "v1" });

	//Создание предиката который определяет какой метод идет в какой документ
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

	//Определение параметров авторизации
	c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "basic",
		In = ParameterLocation.Header,
		Description = "Basic Authorization header using the Bearer scheme."
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "basic"
				}
			},
			new string[] {}
		}
	});

	//Добавление XML комментариев в swagger
	var executingAssembly = Assembly.GetExecutingAssembly();
	var currentDirectory = AppContext.BaseDirectory;

	// Include XML comments from the current assembly
	var executingAssemblyXmlFile = $"{executingAssembly.GetName().Name}.xml";
	var executingAssemblyXmlPath = Path.Combine(currentDirectory, executingAssemblyXmlFile);
	c.IncludeXmlComments(executingAssemblyXmlPath);

	// Include XML comments from other assemblies
	var referencedAssemblies = executingAssembly.GetReferencedAssemblies();
	foreach (var assemblyName in referencedAssemblies)
	{
		var assembly = Assembly.Load(assemblyName);
		var xmlFile = $"{assembly.GetName().Name}.xml";
		var xmlPath = Path.Combine(currentDirectory, xmlFile);

		if (File.Exists(xmlPath))
		{
			c.IncludeXmlComments(xmlPath);
		}
	}

	//Добавление фильтра операций который добавляет им тэги
	c.OperationFilter<SwaggerTagOperationFilter>();
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
app.UseMiddleware<BasicAuthMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
