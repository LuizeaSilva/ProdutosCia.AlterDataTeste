namespace ProdutosCia.Domain.Exceptions;

public class ObjectNotFoundException(string objectName) : BaseException(404,
    "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
    "Object could not be found.",
    objectName)
{
}