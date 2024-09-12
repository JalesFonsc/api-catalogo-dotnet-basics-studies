using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Category
{
    public interface ICategoryRepository : IRepository<CategoriaModel>
    {
        Task<PagedList<CategoriaModel>> ListarCategoriasPorFiltros(CategoriasParameters categoriasParameters);
        Task<IEnumerable<CategoriaModel>> ListarCategoriasComProdutos();
    }
}
