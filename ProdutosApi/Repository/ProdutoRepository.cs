using Microsoft.EntityFrameworkCore;
using ProdutosApi.Models;

namespace ProdutosApi.Repository;

public class ProdutoRepository : DbContext
{
    public ProdutoRepository(DbContextOptions<ProdutoRepository> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; } = null!;
}