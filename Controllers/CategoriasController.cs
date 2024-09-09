using APICatalogo.Dto.Categoria;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services.Categoria;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriasController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
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
            var categorias = await _categoryRepository.ListarCategorias();

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
            var categoria = await _categoryRepository.BuscarCategoriaPorId(idCategoria);

            if (categoria == null) { 
                return BadRequest("Dados não encontrados!");
            }

            return Ok(categoria);

        }

        [HttpPost("CriaCategoria")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> CriaCategoria(CategoriaCriacaoDto categoriaCriacaoDto)
        {
            var categorias = await _categoryRepository.CriaCategoria(categoriaCriacaoDto);

            if (categorias == null) { 
                return BadRequest("Dados não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            return Ok(categorias);
        }

        [HttpPut("EditarCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> EditarCategoria(int idCategoria, CategoriaCriacaoDto categoriaCriacaoDto)
        {
            var categorias = await _categoryRepository.EditarCategoria(idCategoria, categoriaCriacaoDto);

            if (categorias == null)
            {
                return BadRequest("Dados não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            return Ok(categorias);
        }

        [HttpDelete("RemoverCategoria/{idCategoria:int}")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> RemoverCategoria(int idCategoria)
        {
            var categorias = await _categoryRepository.RemoverCategoria(idCategoria);

            if (categorias == null)
            {
                return BadRequest("Dados não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            return Ok(categorias);
        }
    }
}
