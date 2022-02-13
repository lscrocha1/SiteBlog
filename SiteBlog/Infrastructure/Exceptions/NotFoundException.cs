namespace SiteBlog.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
        Message = string.Empty;
    }

    public NotFoundException(string message) : base(message)
    {
        Message = string.Empty;
    }

    public string Message { get; set; }
}