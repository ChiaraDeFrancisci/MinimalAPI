namespace MSMinimalAPI.Middlewares;
public class GlobalExceptionMiddleware
{
    //deve avere un costruttore particolare per agire come middleware

    public readonly RequestDelegate next;
    public readonly ILogger<GlobalExceptionMiddleware> logger;
    public GlobalExceptionMiddleware(RequestDelegate next,ILogger<GlobalExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }
    //gestione eccezioni nei middleware
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError($"Eccezione non gestita: {ex.Message}");
            //gestione tipo eccezione
            await HandleException(ex, context);
        }
    }
    private static Task HandleException(Exception ex, HttpContext context)
    {
        var (statusCode, title) = ex switch
        {
            KeyNotFoundException =>(StatusCodes.Status404NotFound,"Resource not found"),
            InvalidProgramException => (StatusCodes.Status406NotAcceptable, "Invalid operation"),
            ValidationException => (StatusCodes.Status400BadRequest, "Bad Request"),
            //per il caso default
            _ => (StatusCodes.Status500InternalServerError,"Generic server error")

        };
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        var problemDetails = new ProblemDetails()
        {
            Detail = ex.Message,
            Status = statusCode,
            Title = title,
            Instance = context.Request.Path
        };
        problemDetails.Extensions["TraceId"] = traceId;
        return ( context.Response.WriteAsJsonAsync(problemDetails));
    }
}
