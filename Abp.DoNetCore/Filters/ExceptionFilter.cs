using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DoNetCore.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var cException = context.Exception;
            //We should create self result on it to return front-end
            if (context.Exception is ArgumentException)
            {
                context.Result = new NotFoundObjectResult(cException.Message);
            }
            else
            {
                context.Result = new BadRequestResult();
            }
            var exceptionMessage = cException.InnerException == null ? cException.Message : cException.InnerException.Message;
            _logger.LogError($"{exceptionMessage}");
            base.OnException(context);
        }
    }
}
