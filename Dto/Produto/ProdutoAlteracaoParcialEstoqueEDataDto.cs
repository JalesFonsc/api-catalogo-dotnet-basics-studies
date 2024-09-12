using APICatalogo.Validations;

namespace APICatalogo.Dto.Produto;

public class ProdutoAlteracaoParcialEstoqueEDataDto
{
    [NotNegativeAndZero]
    public float Estoque { get; set; }

    [NotAcceptingPastDatesValidation]
    public DateTime DataCadastro { get; set; }
}
