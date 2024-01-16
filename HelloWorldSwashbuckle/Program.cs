using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(typeof(ProductsModule.ProductsController).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
List<string> groups = new();
builder.Services.AddSwaggerGen(c =>
{
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI( c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello World API V1");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
