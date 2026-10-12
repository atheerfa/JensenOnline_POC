public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next) { _next = next; }
    public async Task Invoke(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { message = "Ett internt serverfel uppstod." });
        }
    }
}