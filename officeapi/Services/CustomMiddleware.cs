namespace officeapi.Services
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _message;

        public CustomMiddleware(RequestDelegate next,string message)
        {
            _next = next;
            _message = message;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Custom Message before resp: {_message}");
            context.Response.Headers.Add("X-Custom-Header", "Hello from Middleware");
            await _next(context);
            Console.WriteLine($"Custom Message after resp: {_message}");
        }
    }
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app, IConfiguration config)
        {
            var message = config["CustomMessage"];
            return app.UseMiddleware<CustomMiddleware>(message);
        }
    }
}
