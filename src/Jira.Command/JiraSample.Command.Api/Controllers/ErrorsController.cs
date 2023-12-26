using FluentValidation.Results;
using JiraSample.Command.Domain.JiraItem.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;

namespace JiraSample.Command.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is ValidationException validationException)
            {
                var moselStateDictionary = new ModelStateDictionary();

                foreach (ValidationFailure? error in validationException.Errors)
                {
                    moselStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return ValidationProblem(moselStateDictionary);
            }

            var (statusCode, message) = exception switch
            {
                ArgumentNullException argumentNullException => (StatusCodes.Status400BadRequest, "Bad/Improper request recieved"),
                JiraItemDomainException jiraItemDomainException => (StatusCodes.Status400BadRequest, "Bad/Improper request recieved"),
                //IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured")
            };

            return Problem(title: message, statusCode: statusCode);
        }
    }
}
