﻿
namespace Borusan.Api.Middleware
{
	public static class MiddleWare
	{
		public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
		{
			return app.UseMiddleware<MyExceptionMiddleware>();
		}
	}


	public class MyExceptionMiddleware : IMiddleware
	{
		private readonly ILogger<MyExceptionMiddleware> _logger;

		public MyExceptionMiddleware(ILogger<MyExceptionMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
		{
			try
			{
				httpContext.Request.EnableBuffering();
				var bodyAsText = await new System.IO.StreamReader(httpContext.Request.Body).ReadToEndAsync();
				httpContext.Request.Body.Position = 0;
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());

				httpContext.Response.StatusCode = 500;
				object response = new
				{
					IsSuccess = false,
					Error = ex.Message
				};

				await httpContext.Response.WriteAsJsonAsync(response);
			}
			finally
			{
				_logger.LogInformation(
					"Request {method} {url} => {statusCode}",
					httpContext.Request?.Method,
					httpContext.Request?.Path.Value,
					httpContext.Response?.StatusCode);
			}
		}
	}
}