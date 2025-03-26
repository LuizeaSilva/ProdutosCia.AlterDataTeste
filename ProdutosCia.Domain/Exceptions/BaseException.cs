namespace ProdutosCia.Domain.Exceptions;

public abstract class BaseException(int statusCode, string type, string title, string detail) : Exception
{
    public int StatusCode { get; protected set; } = statusCode;
    public string Type { get; protected set; } = type;
    public string Title { get; protected set; } = title;
    public string Detail { get; protected set; } = detail;
    public IDictionary<string, string[]>? Errors { get; protected set; }

    protected BaseException(int status, string type, string title, string detail, IDictionary<string, string[]> errors)
        : this(status, type, title, detail)
    {
        Errors = errors;
    }
}