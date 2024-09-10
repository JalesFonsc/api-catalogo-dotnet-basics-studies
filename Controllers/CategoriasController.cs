using APICatalogo.Dto.Categoria;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Category;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoryRepository categoryRepository, ILogger<CategoriasController> logger)
        {
            this._categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet("ListarCategoriasComProdutos")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> ListarCategoriasComProdutos()
        {
            var categoriasComProdutos = await _categoryRepository.ListarCategoriasComProdutos();

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
            var categorias = await _categoryRepository.Listar();

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
            var categoria = await _categoryRepository.BuscarPorId(categoria => categoria.CategoriaId == idCategoria);

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

            var categoria = await _categoryRepository.Criar(categoriaASerCriada);

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

            var categoria = await _categoryRepository.Editar(categoriaASerEditada);

            if (categoria == null)
            {
                return BadRequest("Categoria não encontrada!");
            }

            return Ok(categoria);
        }

        [HttpDelete("RemoverCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> RemoverCategoria(int idCategoria)
        {
            var categoriaASerDeletada = new CategoriaModel()
            {
                CategoriaId = idCategoria
            };

            var categoria = await _categoryRepository.Deletar(categoriaASerDeletada);

            if (categoria == null)
            {
                return BadRequest("Categoria não encontrada!");
            }

            return Ok(categoria);
        }
    }
}
