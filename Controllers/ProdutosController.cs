using APICatalogo.Dto.Mappings;
using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Produto;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        private ActionResult<IEnumerable<ProdutoCriacaoDto>> ObterProdutos(PagedList<ProdutoModel> produtos)
        {
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosToProdutosCriacaoDto = _mapper.Map<IEnumerable<ProdutoCriacaoDto>>(produtos);

            return Ok(produtosToProdutosCriacaoDto);
        }

        [HttpGet("ListarPorProdutosFiltroPreco")]
        public async Task<ActionResult<IEnumerable<ProdutoCriacaoDto>>> ListarProdutosComFiltros([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await _unitOfWork.ProdutoRepository.ListarPorProdutosFiltroPreco(produtosFiltroPreco);

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return ObterProdutos(produtos);
        }

        

        [HttpGet("ListarProdutosComFiltros")]
        public async Task<ActionResult<IEnumerable<ProdutoCriacaoDto>>> ListarProdutosComFiltros([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.ListarProdutos(produtosParameters);

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            return ObterProdutos(produtos);
        }

        [HttpGet("ListarProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoCriacaoDto>>> ListarProdutos()
        {
            var produtos = await _unitOfWork.ProdutoRepository.Listar();

            if (produtos == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (produtos.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            var produtosToProdutosCriacaoDto = _mapper.Map<IEnumerable<ProdutoCriacaoDto>>(produtos);

            return Ok(produtosToProdutosCriacaoDto);
        }

        [HttpGet("ListarProdutosPorIdCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> ListarProdutosPorIdCategoria(int idCategoria)
        {
            var produtos = await _unitOfWork.ProdutoRepository.ListarProdutosPorIdCategoria(idCategoria);

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
            var produto = await _unitOfWork.ProdutoRepository.BuscarPorId(i => i.ProdutoId == idProduto);

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

            var produtoASerCriado = _mapper.Map<ProdutoModel>(produtoCriacaoDto);

            if (produtoASerCriado == null)
            {
                return BadRequest("Houve um erro na conversão dos dados.");
            }

            var produto = await _unitOfWork.ProdutoRepository.Criar(produtoASerCriado);
            await _unitOfWork.Commit();

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

            var produtoASerEditado = produtoCriacaoDto.ToProduto(idProduto);

            if (produtoASerEditado == null)
            {
                return BadRequest("Houve um erro na conversão dos dados.");
            }

            var produto = await _unitOfWork.ProdutoRepository.Editar(produtoASerEditado);
            await _unitOfWork.Commit();

            if (produto == null)
            {
                return BadRequest("Produto não encontrados!");
            }

            return Ok(produto);
        }


        [HttpPatch("{idProduto:int}/AlterarEstoqueEData")]
        public async Task<ActionResult<ProdutoCriacaoDto>> AlterarEstoqueEData(int idProduto, JsonPatchDocument<ProdutoAlteracaoParcialEstoqueEDataDto> produtoAlteracaoParcialEstoqueEDataDto)
        {
            if (produtoAlteracaoParcialEstoqueEDataDto == null || idProduto <= 0)
            {
                return BadRequest("Os dados necessários não foram passados!");
            }

            var produto = await _unitOfWork.ProdutoRepository.BuscarPorId(produtoBanco => produtoBanco.ProdutoId == idProduto);

            if (produto == null)
            {
                return NotFound($"Produto com id = {idProduto} não encontrado!");
            }

            var produtoParaAlteracaoDTO = _mapper.Map<ProdutoAlteracaoParcialEstoqueEDataDto>(produto);

            produtoAlteracaoParcialEstoqueEDataDto.ApplyTo(produtoParaAlteracaoDTO, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoParaAlteracaoDTO))
                return BadRequest(ModelState);

            _mapper.Map(produtoParaAlteracaoDTO, produto);

            await _unitOfWork.ProdutoRepository.Editar(produto);
            await _unitOfWork.Commit();

            var produtoParaProdutoCriacaoDTO = _mapper.Map<ProdutoCriacaoDto>(produto);

            return Ok(produtoParaProdutoCriacaoDTO);
        }

        [HttpDelete("RemoverProduto/{idProduto:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> RemoverProduto(int idProduto)
        {
            var produto = await _unitOfWork.ProdutoRepository.BuscarPorId(produtoBanco => produtoBanco.ProdutoId == idProduto);

            if (produto == null)
            {
                return NotFound($"Produto com id = {idProduto} não encontrado!");
            }

            await _unitOfWork.ProdutoRepository.Deletar(produto);
            await _unitOfWork.Commit();

            return Ok(produto);
        }
    }
}
