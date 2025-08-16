namespace ProdutosApi.Models;

public class Produto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float UnitPrice { get; set; }
}