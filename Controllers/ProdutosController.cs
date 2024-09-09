using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using APICatalogo.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("ListarProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> ListarProdutos()
        {
            var produtos = await _produtoRepository.ListarProdutos();

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return Ok(produtos);
        }

        [HttpGet("BuscarProdutoPorId/{idProduto:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoModel>> BuscarProdutoPorId(int idProduto)
        {
            var produto = await _produtoRepository.BuscarProdutoPorId(idProduto);

            if (produto == null)
            {
                return BadRequest($"Produto com id = {idProduto} não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost("CriaProduto")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> CriaProduto(ProdutoCriacaoDto produtoCriacaoDto)
        {
            var produtos =  await _produtoRepository.CriaProduto(produtoCriacaoDto);

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return Ok(produtos);
        }

        [HttpPut("EditarProduto/{idProduto:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> EditarProduto(int idProduto, ProdutoCriacaoDto produtoCriacaoDto)
        {
            var produtos = await _produtoRepository.EditarProduto(idProduto, produtoCriacaoDto);

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return Ok(produtos);
        }

        [HttpDelete("RemoverProduto/{idProduto:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> RemoverProduto(int idProduto)
        {
            var produtos = await _produtoRepository.RemoverProduto(idProduto);

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return Ok(produtos);
        }
    }
}
