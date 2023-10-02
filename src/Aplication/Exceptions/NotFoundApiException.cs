namespace Application.Exceptions;

public class NotFoundApiException : Exception
{
    public NotFoundApiException(string message) : base(message)
    {
    }

    public NotFoundApiException(string resource, string key) : base($"{resource} with key {key} not found.")
    {
    }
}