using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Produto;

public interface IProdutoRepository : IRepository<ProdutoModel>
{ 
    Task<PagedList<ProdutoModel>> ListarProdutos(ProdutosParameters produtosParameters);
    Task<IEnumerable<ProdutoModel>> ListarProdutosPorIdCategoria(int idCategoria);
}
