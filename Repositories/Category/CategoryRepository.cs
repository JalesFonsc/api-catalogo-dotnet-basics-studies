using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Category;

public class CategoryRepository : Repository<CategoriaModel>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CategoriaModel>> ListarCategoriasComProdutos()
    {
        return await _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToListAsync();

    }

    public async Task<PagedList<CategoriaModel>> ListarCategoriasPorFiltros(CategoriasParameters categoriasParameters)
    {
        var categorias = await Listar();

        var categoriaLista = categorias
                .OrderBy(categoria => categoria.CategoriaId)
                .AsQueryable();

        var categoriasOrdernadas = PagedList<CategoriaModel>.ToPagedList(categoriaLista, categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdernadas;
    }

    public async Task<PagedList<CategoriaModel>> ListarPorCategoriasFiltroNome(CategoriasFiltroNome categoriasFiltroNome)
    {
        var categorias = await Listar();

        var categoriasQueryable = categorias.AsQueryable();

        if (!string.IsNullOrEmpty(categoriasFiltroNome.Nome)) {
            categoriasQueryable = categoriasQueryable.Where(categoria => categoria.Nome.Contains(categoriasFiltroNome.Nome)).OrderBy(p => p.Nome);
        }

        var categoriasFiltradas = PagedList<CategoriaModel>.ToPagedList(categoriasQueryable, categoriasFiltroNome.PageNumber, categoriasFiltroNome.PageSize);

        return categoriasFiltradas;
    }
}
