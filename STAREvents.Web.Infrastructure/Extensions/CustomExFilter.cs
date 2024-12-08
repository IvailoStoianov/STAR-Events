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
                // Generate a unique exception code
                var exceptionCode = Guid.NewGuid().ToString();

                // Log the exception with the exception code
                _logger.LogError(context.Exception, "An unhandled exception occurred. Exception Code: {ExceptionCode}", exceptionCode);

                // Set the response status code
                context.HttpContext.Response.StatusCode = 500;

                // Set the view result
                var result = new ViewResult
                {
                    ViewName = "~/Views/Error/Error500.cshtml"
                };

                // Pass the exception code to the view
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





