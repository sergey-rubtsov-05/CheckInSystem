using System.Net;
using Business.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundExpection)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            context.Result = new JsonResult(context.Exception.Message);

            base.OnException(context);
        }
    }
}