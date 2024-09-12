using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories.Produto
{
    public class ProdutoRepository : Repository<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<ProdutoModel>> ListarProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = await Listar();

            var produtosLista = produtos
                .OrderBy(produto => produto.ProdutoId)
                .AsQueryable();

            var produtosOrdernados = PagedList<ProdutoModel>.ToPagedList(produtosLista, produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdernados;
        }

        public async Task<IEnumerable<ProdutoModel>> ListarProdutosPorIdCategoria(int idCategoria)
        {
            return await _context.Produtos.Where(produtoBanco => produtoBanco.CategoriaId == idCategoria).ToListAsync();
        }
    }
}
