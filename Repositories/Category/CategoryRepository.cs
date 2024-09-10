using APICatalogo.Context;
using APICatalogo.Models;
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
}
