﻿using APICatalogo.Dto.Categoria;
using APICatalogo.Dto.Mappings;
using APICatalogo.Dto.Produto;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IMapper _mapper;

        public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private void DefinirHeadersCategorias(PagedList<CategoriaModel> categorias)
        {
            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        }

        [HttpGet("ListarCategoriasPorFiltros")]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> ListarCategoriasPorFiltros([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _unitOfWork.CategoryRepository.ListarCategoriasPorFiltros(categoriasParameters);

            if (categorias == null)
            {
                return BadRequest("Dados não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Não há nenhuma categoria cadastrada!");
            }

            DefinirHeadersCategorias(categorias);

            return Ok(categorias);
        }

        [HttpGet("ListarPorCategoriasFiltroNome")]
        public async Task<ActionResult<IEnumerable<CategoriaCriacaoDto>>> ListarPorCategoriasFiltroNome([FromQuery] CategoriasFiltroNome categoriasFiltroNome)
        {
            var categorias = await _unitOfWork.CategoryRepository.ListarPorCategoriasFiltroNome(categoriasFiltroNome);

            if (categorias == null)
            {
                return BadRequest("Produtos não encontrados!");
            }

            if (categorias.ToList().Count == 0)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            DefinirHeadersCategorias(categorias);

            var categoriasToCategoriasCriacaoDto = _mapper.Map<IEnumerable<CategoriaCriacaoDto>>(categorias);

            return Ok(categoriasToCategoriasCriacaoDto);

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

            var categoriaASerCriada = _mapper.Map<CategoriaModel>(categoriaCriacaoDto);

            if (categoriaASerCriada == null)
            {
                return BadRequest("Houve um erro na conversão dos dados.");
            }

            var categoria = await _unitOfWork.CategoryRepository.Criar(categoriaASerCriada);
            await _unitOfWork.Commit();

            if (categoria == null) { 
                return BadRequest("A criação da categoria obteve um erro!");
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

            var categoriaASerEditada = categoriaCriacaoDto.ToCategoria(idCategoria);

            if (categoriaASerEditada == null)
            {
                return BadRequest("Houve um erro na conversão dos dados.");
            }

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
