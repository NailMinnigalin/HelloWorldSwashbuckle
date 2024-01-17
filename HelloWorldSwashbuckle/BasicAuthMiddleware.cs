using System.Security.Claims;
using System.Text;

namespace HelloWorldSwashbuckle
{
	public class BasicAuthMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _username = "testuser";
		private readonly string _password = "testpassword";

		public BasicAuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			string? authHeader = context.Request.Headers["Authorization"];
			if (authHeader != null && authHeader.StartsWith("Basic "))
			{
				var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
				var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
				var username = decodedUsernamePassword.Split(':', 2)[0];
				var password = decodedUsernamePassword.Split(':', 2)[1];

				if (username == _username && password == _password)
				{
					var claims = new[] { new Claim(ClaimTypes.Name, username) };
					var identity = new ClaimsIdentity(claims, "Basic");
					context.User = new ClaimsPrincipal(identity);

					await _next(context);
					return;
				}
			}

			context.Response.Headers["WWW-Authenticate"] = "Basic";
			context.Response.StatusCode = 401; // Unauthorized
			return;
		}
	}
}
