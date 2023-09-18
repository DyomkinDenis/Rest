using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyCleanArchitecture.Filtres
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(exception.Message);

            }
            else
            {
                context.Result = new StatusCodeResult(500);

            }
        }
    }
}
