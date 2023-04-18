using ImperiumLogistics.SharedKernel;
using System.Net;

namespace ImperiumLogistics.API.Diagnostics
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                Console.WriteLine($"Error handler caught exception => {error.ToJson()}");
                switch (error)
                {
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync("Something went wrong, your request could not be processed.");
            }
        }
    }

}
