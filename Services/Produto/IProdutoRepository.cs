using APICatalogo.Dto.Categoria;
using APICatalogo.Dto.Produto;
using APICatalogo.Models;

namespace APICatalogo.Services.Produto;

public interface IProdutoRepository
{
    Task<IEnumerable<ProdutoModel>> ListarProdutos();
    Task<ProdutoModel> BuscarProdutoPorId(int produtoId);
    Task<IEnumerable<ProdutoModel>> CriaProduto(ProdutoCriacaoDto produtoCriacaoDto);
    Task<IEnumerable<ProdutoModel>> EditarProduto(int idProduto, ProdutoCriacaoDto produtoCriacaoDto);
    Task<IEnumerable<ProdutoModel>> RemoverProduto(int idProduto);
}
