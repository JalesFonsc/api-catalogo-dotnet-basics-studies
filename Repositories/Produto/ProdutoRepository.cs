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

        public async Task<PagedList<ProdutoModel>> ListarPorProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await Listar();

            var produtosToQueryable = produtos.AsQueryable();

            if (produtosFiltroPreco.Preco.HasValue && produtosFiltroPreco.PrecoCriterio.HasValue)
            {
                switch(produtosFiltroPreco.PrecoCriterio)
                {
                case PrecoCriterio.Maior:
                    produtosToQueryable = produtosToQueryable.Where(produto => produto.Preco > produtosFiltroPreco.Preco).OrderBy(p => p.Preco);
                    break;
                case PrecoCriterio.Igual:
                    produtosToQueryable = produtosToQueryable.Where(produto => produto.Preco == produtosFiltroPreco.Preco).OrderBy(p => p.Preco);
                    break;
                case PrecoCriterio.Menor:
                    produtosToQueryable = produtosToQueryable.Where(produto => produto.Preco < produtosFiltroPreco.Preco).OrderBy(p => p.Preco);
                    break;
                }
            }

            var produtosFiltrados = PagedList<ProdutoModel>.ToPagedList(produtosToQueryable, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);

            return produtosFiltrados;
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
