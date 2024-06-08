using FastEndpoints;

namespace Api.Models.Responses;

public record ApiResponse
{
    protected ApiResponse()
    {
    }

    public bool IsSuccess { get; init; }
    public ProblemDetails? Details { get; init; }

    public static ApiResponse Success() => new() { IsSuccess = true };

    public static ApiResponse Fail(ProblemDetails details)
    {
        return new ApiResponse
        {
            Details = details,
            IsSuccess = false
        };
    }
}

public record ApiResponse<TData> : ApiResponse
{
    public TData? Data { get; init; }

    public static ApiResponse<TData> Success(TData data)
    {
        return new ApiResponse<TData>
        {
            Data = data,
            IsSuccess = true
        };
    }


    public new static ApiResponse<TData> Fail(ProblemDetails details)
    {
        return new ApiResponse<TData>
        {
            Details = details,
            IsSuccess = true
        };
    }
}