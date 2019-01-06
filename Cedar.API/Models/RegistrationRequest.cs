namespace Cedar.API.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegistrationRequest : IValidatableObject
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required.");
            }

            if (string.IsNullOrEmpty(Email))
            {
                yield return new ValidationResult("Email is required.");
            }
        }
    }
}