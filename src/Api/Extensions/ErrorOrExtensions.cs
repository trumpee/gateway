using Api.Models.Responses;
using ErrorOr;
using FastEndpoints;

namespace Api.Extensions;

internal static class ErrorOrExtensions
{
    internal static ApiResponse<TResponse> ToApiResponse<TEntity, TResponse>(
        this ErrorOr<TEntity> errorOr, Func<TEntity, TResponse> mapper)
    {
        if (errorOr.IsError)
        {
            var details = new ProblemDetails
            {
                Errors = errorOr.ToProblemDetailsErrors()
            };
            var errorResponse = ApiResponse<TResponse>.Fail(details);
            return errorResponse;
        }

        var templateResponse = mapper(errorOr.Value);

        return ApiResponse<TResponse>.Success(templateResponse);
    }

    private static IEnumerable<ProblemDetails.Error> ToProblemDetailsErrors<T>(this ErrorOr<T> errorOr)
    {
        if (!errorOr.IsError)
            throw new ArgumentException("DU contains value, not error");

        var errors = errorOr.Errors;
        var mappedErrors = errors.Select(ToProblemDetailsErrors);

        return mappedErrors;
    }

    private static ProblemDetails.Error ToProblemDetailsErrors(Error error)
    {
        return new ProblemDetails.Error
        {
            Name = error.Code,
            Code = error.Code,
            Reason = error.Description,
            Severity = error.Type.ToString()
        };
    }
}