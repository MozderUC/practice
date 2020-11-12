
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NetCoreMentoring.App.Infrastructure
{
    public class ActionInvocationLoggingAttribute : IActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ActionInvocationLoggingAttribute> _logger;

        public ActionInvocationLoggingAttribute(
            IConfiguration configuration,
            ILogger<ActionInvocationLoggingAttribute> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Finished {Action}", context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!bool.TryParse(_configuration["IsLogActionParameters"], out var isLogActionParameters) || isLogActionParameters)
            {
                _logger.LogInformation("Started {Action}; Parameters: {@Parameters} ", context.ActionDescriptor.DisplayName, context.ModelState.Values);
                return;
            }

            _logger.LogInformation("Started {Action};", context.ActionDescriptor.DisplayName);
        }
    }
}
