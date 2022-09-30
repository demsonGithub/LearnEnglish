using Demkin.Core.Exceptions;
using Demkin.Utils;
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

            string message;
            if (context.Exception.GetType() == typeof(DomainException))
            {
                message = exception.Message;
            }
            else if (_env.IsDevelopment())
            {
                message = exception.ToString();
                if (message.Contains("Cannot resolve parameter"))
                {
                    message = StringHelper.MidStrEx(message, "Cannot resolve parameter", "of constructor") + "Dependency injection is not implemented";
                }
            }
            else
            {
                message = "服务器发生了错误";
                _logger.LogError(exception, "UnhandledException occured");
            }

            ObjectResult result = new ObjectResult(ApiResult<string>.Build(ApiCode.Fail, message));

            context.Result = result;
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}