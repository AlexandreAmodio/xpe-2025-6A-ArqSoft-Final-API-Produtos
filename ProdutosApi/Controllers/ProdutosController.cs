using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Models;
using ProdutosApi.Service;

namespace ProdutosApi.Controllers
{

    /// <summary>
    /// Controller Produtos, responsável por gerenciar as operações relacionadas a Produtos na API.
    /// Implementa ações RESTful utilizando padrão MVC com controladores de API para criação, leitura, atualização e exclusão (CRUD).
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        /// <summary>
        /// Construtor que recebe uma implementação do serviço de produtos via injeção de dependência.
        /// </summary>
        /// <param name="service">Serviço que gerencia as operações relacionadas a Produto.</param>
        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET: api/Produtos
        /// Retorna uma lista de todos os produtos cadastrados.
        /// </summary>
        /// <returns>Uma lista assíncrona de produtos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _service.listarTodos();
        }

        /// <summary>
        /// GET: api/Produtos/5
        /// Retorna um produto específico com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser buscado.</param>
        /// <returns>O produto encontrado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(long id)
        {
            var produto = await _service.buscarPorId(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        /// <summary>
        /// GET: api/Produtos/nome/teste
        /// Retorna um produto específico com base no nome informado.
        /// </summary>
        /// <param name="nome">Nome ou parte do nome do produto a ser buscado.</param>
        /// <returns>O produto encontrado ou NotFound se não existir.</returns>
        [HttpGet("/nome/{nome}")]
        public async Task<ActionResult<List<Produto>>> buscarPorNome(String nome)
        {
            var produtos = await _service.buscarPorNome(nome);

            if (produtos == null || produtos.Count == 0)
            {
                return NotFound();
            }

            return produtos;
        }

        /// <summary>
        /// PUT: api/Produtos/5
        /// Atualiza um produto existente com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produto">Dados do produto atualizado.</param>
        /// <returns>Sem conteúdo se a atualização for bem-sucedida ou erros de status HTTP apropriados.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(long id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            Produto produtoUptated = null;
            try
            {
                produtoUptated = await _service.salvar(produto);
            }
            catch (Exception ex)
            {
                if (produtoUptated == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST: api/Produtos
        /// Cria um novo produto na base de dados.
        /// </summary>
        /// <param name="produto">Dados do produto a ser inserido.</param>
        /// <returns>O produto criado com sua URL de acesso.</returns>
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            await _service.inserir(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        /// <summary>
        /// DELETE: api/Produtos/5
        /// Remove um produto com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Sem conteúdo se a remoção for bem-sucedida ou NotFound se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(long id)
        {
            var produto = await _service.deletar(id);
            if (produto == false)
            {
                return NotFound();
            }

            return NoContent();
        }


        /// <summary>
        /// DELETE: api/Produtos/5
        /// Remove um produto com base no ID informado.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Sem conteúdo se a remoção for bem-sucedida ou NotFound se não encontrado.</returns>
        [HttpGet("/contar")]
        public async Task<ActionResult<int>> contarProdutos()
        {
            return await _service.contarProdutos();
        }

    }
}
