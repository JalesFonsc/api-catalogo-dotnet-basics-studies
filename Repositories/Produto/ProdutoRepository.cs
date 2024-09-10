using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories.Produto
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
