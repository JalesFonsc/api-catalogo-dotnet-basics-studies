using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Pagination;

public enum PrecoCriterio
{
    [Display(Name = "Maior")]
    Maior,
    [Display(Name = "Igual")]
    Igual,
    [Display(Name = "Menor")]
    Menor
}
