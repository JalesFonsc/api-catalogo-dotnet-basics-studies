using APICatalogo.Context;
using APICatalogo.Dto.Categoria;
using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Services.Produto;

public class ProdutoService : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProdutoModel> BuscarProdutoPorId(int produtoId)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(produtoBanco => produtoBanco.ProdutoId == produtoId);
    }

    public async Task<IEnumerable<ProdutoModel>> CriaProduto(ProdutoCriacaoDto produtoCriacaoDto)
    {
        var produto = new ProdutoModel()
        {
            Nome = produtoCriacaoDto.Nome,
            Descricao = produtoCriacaoDto.Descricao,
            Estoque = produtoCriacaoDto.Estoque,
            DataCadastro = produtoCriacaoDto.DataCadastro,
            ImagemUrl = produtoCriacaoDto.ImagemUrl,
            Preco = produtoCriacaoDto.Preco,
            CategoriaId = produtoCriacaoDto.CategoriaId
        };

        _context.Add(produto);
        await _context.SaveChangesAsync();

        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<ProdutoModel>> EditarProduto(int idProduto, ProdutoCriacaoDto produtoCriacaoDto)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(produtoBanco => produtoBanco.ProdutoId == idProduto);

        var produtoModificado = new ProdutoModel()
        {
            ProdutoId = produto.ProdutoId,
            Nome = produtoCriacaoDto.Nome,
            Preco = produtoCriacaoDto.Preco,
            Descricao = produtoCriacaoDto.Descricao,
            ImagemUrl = produtoCriacaoDto.ImagemUrl,
            Estoque = produtoCriacaoDto.Estoque,
            DataCadastro = produtoCriacaoDto.DataCadastro,
            CategoriaId = produtoCriacaoDto.CategoriaId
        };

        _context.Produtos.Entry(produtoModificado).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<ProdutoModel>> ListarProdutos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<ProdutoModel>> RemoverProduto(int idProduto)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(i => i.ProdutoId == idProduto);

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return await _context.Produtos.AsNoTracking().ToListAsync();
    }
}
