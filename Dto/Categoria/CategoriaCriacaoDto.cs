using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Dto.Categoria;

public class CategoriaCriacaoDto
{
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
}
