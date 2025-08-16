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

public interface IProdutoService
{
    public Task<List<Produto>> listarTodos();
    public Task<Produto> buscarPorId(long id);
    public Task<List<Produto>> buscarPorNome(String nome);
    public Task<Produto> inserir(Produto produto);
    public Task<Produto> salvar(Produto produto);
    public Task<bool> deletar(long id);
    public Task<long> contarProdutos();
}


public class ProdutoService : IProdutoService{

    private readonly ProdutoRepository _context;

    public ProdutoService(ProdutoRepository context)
    {
        _context = context;
    }

    public async Task<List<Produto>> listarTodos() { 
        return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto> buscarPorId(long id) { 
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<List<Produto>> buscarPorNome(String nome) { 
        return _context.Produtos
                                    .Where(b => b.Name.Contains(nome))
                                    .ToList();
    }

    public async Task<Produto> inserir(Produto produto){
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

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

    public async Task<long> contarProdutos() { 
        return _context.Produtos.Count();
    }

    private bool ProdutoExists(long id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }

}