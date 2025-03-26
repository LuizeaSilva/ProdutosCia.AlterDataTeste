namespace ProdutosCia.Application.Dtos;

public class BulkCreateResponse
{
    public object Item { get; set; }
    public int StatusCode { get; set; }
    public object? Erros { get; set; }
}