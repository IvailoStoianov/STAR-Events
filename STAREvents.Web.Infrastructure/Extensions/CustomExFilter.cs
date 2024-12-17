using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace STAREvents.Web.Infrastructure.Extensions
{
    public class CustomExFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExFilter> _logger;

        public CustomExFilter(ILogger<CustomExFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled || !context.HttpContext.Response.HasStarted)
            {
                var exceptionCode = Guid.NewGuid().ToString();

                _logger.LogError(context.Exception, "An unhandled exception occurred. Exception Code: {ExceptionCode}", exceptionCode);

                context.HttpContext.Response.StatusCode = 500;

                var result = new ViewResult
                {
                    ViewName = "~/Views/Error/Error500.cshtml"
                };

                result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    { "ExceptionCode", exceptionCode }
                };

                context.Result = result;
                context.ExceptionHandled = true;
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.ContentType = "text/html";
            }
        }
    }
}





