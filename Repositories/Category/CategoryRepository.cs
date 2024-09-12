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
}
