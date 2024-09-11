using APICatalogo.Dto.Categoria;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("ListarCategoriasComProdutos")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> ListarCategoriasComProdutos()
        {
            var categoriasComProdutos = await _unitOfWork.CategoryRepository.ListarCategoriasComProdutos();

            if(categoriasComProdutos == null)
            {
                return BadRequest("Dados não encontrados!");
            }

            if (categoriasComProdutos.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            return Ok(categoriasComProdutos);
        }

        [HttpGet("ListarCategorias")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> ListarCategorias()
        {
            var categorias = await _unitOfWork.CategoryRepository.Listar();

            if(categorias == null)
            {
                return BadRequest("Dados não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            return Ok(categorias);

        }

        [HttpGet("BuscarCategoriaPorId/{idCategoria:int}")]
        public async Task<ActionResult<CategoriaModel>> BuscarCategoriaPorId(int idCategoria)
        {
            var categoria = await _unitOfWork.CategoryRepository.BuscarPorId(categoria => categoria.CategoriaId == idCategoria);

            if (categoria == null) { 
                return BadRequest("Categoria não encontrada!");
            }

            return Ok(categoria);

        }

        [HttpPost("CriaCategoria")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> CriaCategoria(CategoriaCriacaoDto categoriaCriacaoDto)
        {
            if (categoriaCriacaoDto == null)
            {
                return BadRequest("Os dados necessários não foram passados!");
            }

            var categoriaASerCriada = new CategoriaModel()
            {
                Nome = categoriaCriacaoDto.Nome,
                ImagemUrl = categoriaCriacaoDto.ImagemUrl
            };

            var categoria = await _unitOfWork.CategoryRepository.Criar(categoriaASerCriada);
            await _unitOfWork.Commit();

            if (categoria == null) { 
                return BadRequest("Categoria não encontrada!");
            }

            return Ok(categoria);
        }

        [HttpPut("EditarCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> EditarCategoria(int idCategoria, CategoriaCriacaoDto categoriaCriacaoDto)
        {
            if (categoriaCriacaoDto == null)
            {
                return BadRequest("Os dados necessários não foram passados!");
            }

            var categoriaASerEditada = new CategoriaModel()
            {
                CategoriaId = idCategoria,
                Nome = categoriaCriacaoDto.Nome,
                ImagemUrl = categoriaCriacaoDto.ImagemUrl
            };

            var categoria = await _unitOfWork.CategoryRepository.Editar(categoriaASerEditada);
            await _unitOfWork.Commit();

            if (categoria == null)
            {
                return BadRequest("Categoria não encontrada!");
            }

            return Ok(categoria);
        }

        [HttpDelete("RemoverCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> RemoverCategoria(int idCategoria)
        {
            var categoria = await _unitOfWork.CategoryRepository.BuscarPorId(categoriaBanco => categoriaBanco.CategoriaId == idCategoria);

            if (categoria == null)
            {
                return BadRequest("Categoria não encontrada!");
            }

            await _unitOfWork.CategoryRepository.Deletar(categoria);
            await _unitOfWork.Commit();


            return Ok(categoria);
        }
    }
}
