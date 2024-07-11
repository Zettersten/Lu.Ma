using Lu.Ma.Models.Shared;

namespace Lu.Ma.Http;

public class LumaApiException(string message, Error? apiError, int statusCode = 500) : Exception(message)
{
    public Error? ApiError { get; } = apiError;

    public int StatusCode { get; } = statusCode;
}