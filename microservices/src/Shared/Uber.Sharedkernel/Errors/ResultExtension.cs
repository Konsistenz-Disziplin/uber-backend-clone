using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Uber.Sharedkernel.Errors;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(new { message = "Success" });
        }

        // You can add conditional checks here to return 404 vs 400 based on the Error code
        return new ObjectResult(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = result.Error.Code,
            Detail = result.Error.Description
        });
    }
}
