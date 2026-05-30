using Microsoft.AspNetCore.Mvc;
using Uber.Shared.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Uber.ServiceDefaults.Http;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        if (error is ValidationError ve)
        {
           
            var modelState = new ModelStateDictionary();
            foreach (var subError in ve.Errors)
            {
                modelState.AddModelError(subError.Code, subError.Description);
            }

            return this.ValidationProblem(modelState);
        }
        var statusCode = error.Type switch
        {
            ErrorType.NotFound     => StatusCodes.Status404NotFound,
            ErrorType.Validation   => StatusCodes.Status400BadRequest,
            ErrorType.Conflict     => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _                      => StatusCodes.Status500InternalServerError
        };

        return Problem(detail: error.Description, statusCode: statusCode, title: error.Code);
    }
}