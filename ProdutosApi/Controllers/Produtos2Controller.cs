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
    [Route("api/[controller]")]
    [ApiController]
    public class Produtos2Controller : ControllerBase
    {
        private readonly IProdutoService _service;

        public Produtos2Controller(IProdutoService service)
        {
            _service = service;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _service.listarTodos();
        }

        // GET: api/Produtos/5
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

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            await _service.inserir(produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
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

    }
}
