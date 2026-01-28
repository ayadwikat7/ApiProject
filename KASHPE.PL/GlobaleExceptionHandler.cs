using DAL.DTOs.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace KASHPE.PL
{
    public class GlobaleExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var errorDetails = new ErrorDetalies
            {
                StatusCode = 500,
                Message = "server error ...",
               // stackTrace = e.InnerException.Message
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(errorDetails);
            return true;
        }
    }
}
