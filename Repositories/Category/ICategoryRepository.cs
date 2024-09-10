using APICatalogo.Models;

namespace APICatalogo.Repositories.Category
{
    public interface ICategoryRepository : IRepository<CategoriaModel>
    {
        Task<IEnumerable<CategoriaModel>> ListarCategoriasComProdutos();
    }
}
