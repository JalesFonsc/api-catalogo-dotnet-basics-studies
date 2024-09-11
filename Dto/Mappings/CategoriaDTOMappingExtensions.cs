using APICatalogo.Dto.Categoria;
using APICatalogo.Models;

namespace APICatalogo.Dto.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaCriacaoDto? ToCategoriaDTO(this CategoriaModel categoria)
        {
            if (categoria == null)
            {
                return null;
            }

            var categoriaDTO = new CategoriaCriacaoDto()
            {
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return categoriaDTO;
        }

        public static CategoriaModel? ToCategoria(this CategoriaCriacaoDto categoriaDTO, int? categoriaId)
        {
            if (categoriaDTO == null)
            {
                return null;
            }

            var categoria = new CategoriaModel()
            {
                CategoriaId = categoriaId ?? 0,
                Nome = categoriaDTO.Nome,
                ImagemUrl = categoriaDTO.ImagemUrl
            };

            return categoria;
        }

        public static IEnumerable<CategoriaCriacaoDto> ToCategoriaDTOList(this IEnumerable<CategoriaModel> categorias)
        {
            if (categorias is null || !categorias.Any())
            {
                return new List<CategoriaCriacaoDto>();
            }

            return categorias.Select(categoria => new CategoriaCriacaoDto()
            {
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            }).ToList();
        }
    }

}
