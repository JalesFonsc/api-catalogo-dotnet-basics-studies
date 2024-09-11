using APICatalogo.Models;

namespace APICatalogo.Repositories.Produto;

public interface IProdutoRepository : IRepository<ProdutoModel>
{ 
    Task<IEnumerable<ProdutoModel>> ListarProdutosPorIdCategoria(int idCategoria);
}
