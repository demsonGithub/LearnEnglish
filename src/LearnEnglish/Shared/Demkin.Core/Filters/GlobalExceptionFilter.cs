using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Core.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            Exception exception = context.Exception;
            _logger.LogError(exception, "UnhandledException occured");

            string message;
            if (_env.IsDevelopment())
            {
                message = exception.ToString();
            }
            else
            {
                message = exception.Message;
            }

            ObjectResult result = new ObjectResult(ApiResultBuilder.Fail(message));

            context.Result = result;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}