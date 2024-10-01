using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.src.Utils;

namespace BookStore.src.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new { ex.StatusCode, ex.Message };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
