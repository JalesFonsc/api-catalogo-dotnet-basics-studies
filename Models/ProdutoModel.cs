using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class ProdutoModel
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage ="O nome do produto é obrigatório.")]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage ="A descrição do produto é obrigatória.")]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Column(TypeName="decimal(10,2)")]
    [NotNegativeAndZero]
    public decimal Preco {  get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    [NotNegativeAndZero]
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    [Required]
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public CategoriaModel? Categoria { get; set; }
}
