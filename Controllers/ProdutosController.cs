﻿using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Produto;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;

        public ProdutosController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("ListarProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> ListarProdutos()
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

            return Ok(produtos);
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

            var produto = await _unitOfWork.ProdutoRepository.Editar(produtoASerEditado);
            await _unitOfWork.Commit();

            if (produto == null)
            {
                return BadRequest("Produto não encontrados!");
            }

            return Ok(produto);
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
