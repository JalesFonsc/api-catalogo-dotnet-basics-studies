using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations
{
    public class NotAcceptingPastDatesValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var objectToDate = Convert.ToDateTime(value);

            if (objectToDate <= DateTime.Now.Date) return new ValidationResult("Não é possivel inserir uma data anterior ao momento atual.");

            return ValidationResult.Success;
        }
    }
}
