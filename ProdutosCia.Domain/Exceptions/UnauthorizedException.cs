namespace ProdutosCia.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string erroMessage, string detail = "See Errors property for details.")
        : base(401,
            "https://datatracker.ietf.org/doc/html/rfc9110#name-401-unauthorized",
            "Request requires authentication.",
            detail)
    {
        Errors = new Dictionary<string, string[]>()
        {
            ["UserName"] = new string[] { erroMessage }
        };
    }
}