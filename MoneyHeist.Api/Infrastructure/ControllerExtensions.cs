using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace MoneyHeist.Api.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult ToApiResponse<TResult>(this Result<TResult> result)
        {
            return result.Match<IActionResult>(resultObject =>
            {
                return new OkObjectResult(resultObject);
            }, exception =>
            {
                if (exception is FluentValidation.ValidationException validationException)
                {
                    return new BadRequestObjectResult(validationException);
                }

                return new StatusCodeResult(500);
            });
        }
    }
}
