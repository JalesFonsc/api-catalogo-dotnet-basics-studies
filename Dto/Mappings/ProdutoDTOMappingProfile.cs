using APICatalogo.Dto.Categoria;
using APICatalogo.Dto.Produto;
using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.Dto.Mappings;

public class ProdutoDTOMappingProfile : Profile
{
    public ProdutoDTOMappingProfile()
    {
        CreateMap<ProdutoModel, ProdutoCriacaoDto>().ReverseMap();
        CreateMap<CategoriaModel, CategoriaCriacaoDto>().ReverseMap();
        CreateMap<ProdutoModel, ProdutoAlteracaoParcialEstoqueEDataDto>().ReverseMap();
    }
}
