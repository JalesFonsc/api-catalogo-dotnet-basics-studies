using APICatalogo.Repositories.Category;
using APICatalogo.Repositories.Produto;

namespace APICatalogo.Repositories;

public interface IUnitOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task Commit();
}
