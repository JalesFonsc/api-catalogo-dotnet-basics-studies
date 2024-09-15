using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Produto;

public interface IProdutoRepository : IRepository<ProdutoModel>
{ 
    Task<PagedList<ProdutoModel>> ListarProdutos(ProdutosParameters produtosParameters);

    Task<PagedList<ProdutoModel>> ListarPorProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    Task<IEnumerable<ProdutoModel>> ListarProdutosPorIdCategoria(int idCategoria);
}
