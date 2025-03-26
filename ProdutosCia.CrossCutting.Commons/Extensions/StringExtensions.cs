using System.Security.Cryptography;
using System.Text;

namespace ProdutosCia.CrossCutting.Commons.Extensions;

public static class StringExtensions
{
    public static string ToSha512(this string inputString)
    {
        using var sha512 = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(inputString);
        var hexString = string.Concat(sha512.ComputeHash(bytes).Select(b => b.ToString("x2")));
        return hexString;
    }
}