using APICatalogo.Dto.Produto;
using APICatalogo.Models;

namespace APICatalogo.Dto.Mappings;

public static class ProdutoDTOMappingExtensions
{
    public static ProdutoCriacaoDto? ToProdutoDTO(this ProdutoModel produto)
    {
        if (produto == null)
        {
            return null;
        }

        var produtoDTO = new ProdutoCriacaoDto()
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            ImagemUrl = produto.ImagemUrl,
            DataCadastro = produto.DataCadastro,
            CategoriaId = produto.CategoriaId
        };

        return produtoDTO;
    }

    public static ProdutoModel? ToProduto(this ProdutoCriacaoDto produtoCriacaoDto, int? idProduto)
    {
        if (produtoCriacaoDto == null)
        {
            return null;
        }

        var produto = new ProdutoModel()
        {
            ProdutoId = idProduto ?? 0,
            Nome = produtoCriacaoDto.Nome,
            Descricao = produtoCriacaoDto.Descricao,
            ImagemUrl = produtoCriacaoDto.ImagemUrl,
            Preco = produtoCriacaoDto.Preco,
            Estoque = produtoCriacaoDto.Estoque,
            DataCadastro = produtoCriacaoDto.DataCadastro,
            CategoriaId = produtoCriacaoDto.CategoriaId
        };

        return produto;
    }

    public static IEnumerable<ProdutoCriacaoDto> ToProdutoDTOList(this IEnumerable<ProdutoModel> produtos)
    {
        if(produtos == null || !produtos.Any())
        {
            return new List<ProdutoCriacaoDto>();
        }

        return produtos.Select(produto => new ProdutoCriacaoDto()
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            ImagemUrl = produto.ImagemUrl,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            DataCadastro = produto.DataCadastro,
            CategoriaId = produto.CategoriaId
        });
    }
}
