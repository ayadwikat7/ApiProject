namespace KASHPE.PL.MiddelWare
{
    public class GlobalExptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExptionHandling(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try { 
            
                await _next(context);
            }
            catch(Exception e) { }
        }
    }
}
