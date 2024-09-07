using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Executa-se após a action
        _logger.LogInformation("### Executando -> OnActionExecuted");
        _logger.LogInformation("#######################################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"Stats Code: {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("#######################################################");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Executa-se antes da action
        _logger.LogInformation("### Executando -> OnActionExecuting");
        _logger.LogInformation("#######################################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        _logger.LogInformation("#######################################################");
    }
}
