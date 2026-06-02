using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Controllers;

[ApiController]
#pragma warning disable CA1515
#pragma warning disable CS1591
public class ApiController : ControllerBase
#pragma warning restore CA1515
{
#pragma warning disable MA0016
#pragma warning disable CA1002
    protected IActionResult Problem(List<Result> errors)
#pragma warning restore CA1002
#pragma warning restore MA0016
#pragma warning restore CS1591
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.TrueForAll(error => error.Error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }
    
    // to-do: Dont just disable this error in future. Apply the fix necessary for performance gain.
#pragma warning disable CA1859
    private IActionResult Problem(Result error)
#pragma warning restore CA1859
    {
        var statusCode = error.Error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Error.Description);
    }
    
    
    // to-do: Dont just disable this error in future. Apply the fix necessary for performance gain.
#pragma warning disable CA1859
    private IActionResult ValidationProblem(ICollection<Result> errors)
#pragma warning restore CA1859
    {
        ModelStateDictionary modelStateDictionary = new();

#pragma warning disable S3267
        foreach (Result error in errors)
#pragma warning restore S3267
        {
            modelStateDictionary.AddModelError(
                error.Error.Code,
                error.Error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}
