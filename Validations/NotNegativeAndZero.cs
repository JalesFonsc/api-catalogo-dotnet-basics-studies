using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations;

public class NotNegativeAndZero : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = Convert.ToSingle(value.ToString());

        if (currentValue <= 0) return new ValidationResult("O valor deve ser maior do que 0.");

        return ValidationResult.Success;
    }
}
