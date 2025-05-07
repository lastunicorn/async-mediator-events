using AsyncMediator;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMediatorEvents.WebApi.Presentation;

internal static class MediatorResultExtensions
{
    public static IActionResult Process<TResponse>(this ICommandWorkflowResult result, Func<TResponse, IActionResult> onSuccess)
        where TResponse : class, new()
    {
        if (onSuccess is null)
            throw new ArgumentNullException(nameof(onSuccess));

        if (result.Success)
        {
            TResponse response = result.Result<TResponse>();

            return onSuccess(response);
        }
        else
        {
            return new StatusCodeResult(500);
        }
    }
}
