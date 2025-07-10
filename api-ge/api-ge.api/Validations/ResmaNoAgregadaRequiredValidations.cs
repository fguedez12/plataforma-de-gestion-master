using api_gestiona.DTOs.Resmas;
using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Validations
{
    public class ResmaNoAgregadaRequiredValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var resma = (ResmaDTO)validationContext.ObjectInstance;
            if (resma.Agregada)
            {
                return ValidationResult.Success;
            }

            if (value == null)
            {

                new ValidationResult("Value is required.");
            }

            return ValidationResult.Success;
        }
    }
}
