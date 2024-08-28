using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace MoneyHeist.Api.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult ToApiResponse<TResult>(this Result<TResult> result, Func<TResult, IActionResult> onSuccess)
        {
            return result.Match<IActionResult>(
                resultObject => onSuccess(resultObject),
                exception =>
                {
                    if (exception is FluentValidation.ValidationException validationException)
                    {
                        return new BadRequestObjectResult(validationException.Errors);
                    }

                    return new StatusCodeResult(500);
                });
        }
    }
}
