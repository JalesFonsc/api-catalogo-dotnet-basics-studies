using APICatalogo.Context;
using APICatalogo.Dto.Categoria;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Services.Categoria;

public class CategoryService : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CategoriaModel> BuscarCategoriaPorId(int categoriaId)
    {
        return await _context.Categorias.FirstOrDefaultAsync(categoriaBanco => categoriaBanco.CategoriaId == categoriaId);
    }

    public async Task<IEnumerable<CategoriaModel>> CriaCategoria(CategoriaCriacaoDto categoriaCriacaoDto)
    {
        var categoria = new CategoriaModel()
        {
            Nome = categoriaCriacaoDto.Nome,
            ImagemUrl = categoriaCriacaoDto.ImagemUrl
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<CategoriaModel>> EditarCategoria(int idCategoria, CategoriaCriacaoDto categoriaCriacaoDto)
    {
        var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(categoriaBanco => categoriaBanco.CategoriaId == idCategoria);

        var categoriaModificada = new CategoriaModel()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoriaCriacaoDto.Nome,
            ImagemUrl = categoriaCriacaoDto.ImagemUrl
        };

        _context.Categorias.Entry(categoriaModificada).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<CategoriaModel>> ListarCategorias()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<CategoriaModel>> ListarCategoriasComProdutos()
    {
        return await _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<CategoriaModel>> RemoverCategoria(int idCategoria)
    {
        var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(i => i.CategoriaId == idCategoria);

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return await _context.Categorias.AsNoTracking().ToListAsync();
    }
}
