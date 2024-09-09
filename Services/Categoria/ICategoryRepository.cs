using APICatalogo.Dto.Categoria;
using APICatalogo.Models;

namespace APICatalogo.Services.Categoria;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoriaModel>> ListarCategorias();
    Task<CategoriaModel> BuscarCategoriaPorId(int categoriaId);
    Task<IEnumerable<CategoriaModel>> ListarCategoriasComProdutos();
    Task<IEnumerable<CategoriaModel>> CriaCategoria(CategoriaCriacaoDto categoriaCriacaoDto);
    Task<IEnumerable<CategoriaModel>> EditarCategoria(int idCategoria, CategoriaCriacaoDto categoriaCriacaoDto);
    Task<IEnumerable<CategoriaModel>> RemoverCategoria(int idCategoria);


}
