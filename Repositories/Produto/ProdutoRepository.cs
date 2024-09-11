using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Produto
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProdutoModel>> ListarProdutosPorIdCategoria(int idCategoria)
        {
            return await _context.Produtos.Where(produtoBanco => produtoBanco.CategoriaId == idCategoria).ToListAsync();
        }
    }
}
