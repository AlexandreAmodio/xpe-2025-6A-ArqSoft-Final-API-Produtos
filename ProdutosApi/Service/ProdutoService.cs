using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosApi.Models;
using ProdutosApi.Repository;

namespace ProdutosApi.Service;

/// <summary>
/// Contrato que define os serviços relacionados a produtos.
/// Contém operações de CRUD e consultas auxiliares.
/// </summary>
public interface IProdutoService
{
    public Task<List<Produto>> listarTodos();
    public Task<Produto> buscarPorId(long id);
    public Task<List<Produto>> buscarPorNome(String nome);
    public Task<Produto> inserir(Produto produto);
    public Task<Produto> salvar(Produto produto);
    public Task<bool> deletar(long id);
    public Task<int> contarProdutos();
}


/// <summary>
/// Implementação concreta da interface <see cref="IProdutoService"/>.
/// Responsável por aplicar a lógica de negócio e interagir com o repositório de produtos.
/// </summary>
public class ProdutoService : IProdutoService{

    private readonly ProdutoRepository _context;

    /// <summary>
    /// Construtor que recebe o repositório de produtos via injeção de dependência.
    /// </summary>
    /// <param name="context">Repositório de produtos (<see cref="ProdutoRepository"/>) utilizado para persistência.</param>
    public ProdutoService(ProdutoRepository context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista todos os produtos cadastrados.
    /// </summary>
    public async Task<List<Produto>> listarTodos() { 
        return await _context.Produtos.ToListAsync();
    }

    /// <summary>
    /// Busca um produto pelo seu identificador único.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    public async Task<Produto> buscarPorId(long id) { 
        return await _context.Produtos.FindAsync(id);
    }

    /// <summary>
    /// Busca produtos que contenham parte do nome informado.
    /// </summary>
    /// <param name="nome">Texto a ser buscado no nome do produto.</param>
    public async Task<List<Produto>> buscarPorNome(String nome) { 
        return _context.Produtos
                                    .Where(b => b.Name.Contains(nome))
                                    .ToList();
    }

    /// <summary>
    /// Insere um novo produto no banco de dados.
    /// </summary>
    /// <param name="produto">Objeto <see cref="Produto"/> a ser inserido.</param>
    public async Task<Produto> inserir(Produto produto){
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    /// <summary>
    /// Atualiza os dados de um produto existente.
    /// </summary>
    /// <param name="produto">Objeto <see cref="Produto"/> com os dados atualizados.</param>
    public async Task<Produto> salvar(Produto produto) { 
            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return produto;
 
    }

    /// <summary>
    /// Remove um produto do banco de dados.
    /// </summary>
    /// <param name="id">Identificador do produto a ser removido.</param>
    /// <returns>Retorna <c>true</c> se a exclusão foi realizada, ou <c>false</c> caso não encontrado.</returns>
    public async Task<bool> deletar(long id) { 
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return false;
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return true; 
    }

    /// <summary>
    /// Retorna a quantidade total de produtos cadastrados.
    /// </summary>
    public async Task<int> contarProdutos() { 
        return _context.Produtos.Count();
    }

    /// <summary>
    /// Verifica se existe um produto no banco de dados com o identificador informado.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <returns><c>true</c> se o produto existir, caso contrário <c>false</c>.</returns>
    private bool ProdutoExists(long id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }

}