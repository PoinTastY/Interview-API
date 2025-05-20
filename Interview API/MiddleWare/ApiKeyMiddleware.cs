namespace Interview_API.MiddleWare
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER_NAME = "Authorization";
        private const string API_KEY_SCHEME = "Bearer";
        private const string API_KEY_VALUE = "KEY";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var authHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization header is missing.");
                return;
            }

            var header = authHeader.ToString();

            if (!header.StartsWith($"{API_KEY_SCHEME} ", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid authorization scheme.");
                return;
            }

            var extractedApiKey = header.Substring(API_KEY_SCHEME.Length).Trim();

            if (!API_KEY_VALUE.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }
}
