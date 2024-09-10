using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using APICatalogo.Repositories.Produto;
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
            var produtos = await _produtoRepository.Listar();

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

        [HttpGet("BuscarProdutoPorId/{idProduto:int}")]
        public async Task<ActionResult<ProdutoModel>> BuscarProdutoPorId(int idProduto)
        {
            var produto = await _produtoRepository.BuscarPorId(i => i.ProdutoId == idProduto);

            if (produto == null)
            {
                return BadRequest($"Produto com id = {idProduto} não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost("CriaProduto")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> CriaProduto(ProdutoCriacaoDto produtoCriacaoDto)
        {
            if (produtoCriacaoDto == null)
            {
                return BadRequest("Os dados necessários não foram passados!");
            }

            var produtoASerCriado = new ProdutoModel()
            {
                Nome = produtoCriacaoDto.Nome,
                Descricao = produtoCriacaoDto.Descricao,
                Estoque = produtoCriacaoDto.Estoque,
                DataCadastro = produtoCriacaoDto.DataCadastro,
                CategoriaId = produtoCriacaoDto.CategoriaId,
                ImagemUrl = produtoCriacaoDto.ImagemUrl,
                Preco = produtoCriacaoDto.Preco
            };

            var produto =  await _produtoRepository.Criar(produtoASerCriado);

            if (produto == null)
            {
                return BadRequest("Produto não encontrado!");
            }

            return Ok(produto);
        }

        [HttpPut("EditarProduto/{idProduto:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> EditarProduto(int idProduto, ProdutoCriacaoDto produtoCriacaoDto)
        {
            if (produtoCriacaoDto == null)
            {
                return BadRequest("Os dados necessários não foram passados!");
            }

            var produtoASerEditado = new ProdutoModel()
            {
                ProdutoId = idProduto,
                Nome = produtoCriacaoDto.Nome,
                Descricao = produtoCriacaoDto.Descricao,
                Estoque = produtoCriacaoDto.Estoque,
                DataCadastro = produtoCriacaoDto.DataCadastro,
                CategoriaId = produtoCriacaoDto.CategoriaId,
                ImagemUrl = produtoCriacaoDto.ImagemUrl,
                Preco = produtoCriacaoDto.Preco
            };

            var produto = await _produtoRepository.Editar(produtoASerEditado);

            if (produto == null)
            {
                return BadRequest("Produto não encontrados!");
            }

            return Ok(produto);
        }

        [HttpDelete("RemoverProduto/{idProduto:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> RemoverProduto(int idProduto)
        {
            var produtoASerDeletado = new ProdutoModel()
            {
                ProdutoId = idProduto
            };

            var produto = await _produtoRepository.Deletar(produtoASerDeletado);

            if (produto == null)
            {
                return BadRequest("Produto não encontrado!");
            }

            return Ok(produto);
        }
    }
}
