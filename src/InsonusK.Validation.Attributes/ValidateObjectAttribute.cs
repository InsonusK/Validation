using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InsonusK.Validation.Attributes;
public class ValidateObjectAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    if (value != null)
    {
      var results = new List<ValidationResult>();
      var context = new ValidationContext(value, null, null);

      Validator.TryValidateObject(value, context, results, true);

      if (results.Count != 0)
      {
        var compositeResults = new CompositeValidationResult(String.Format("Validation for {0} failed!",
            validationContext.DisplayName));
        results.ForEach(compositeResults.AddResult);

        return compositeResults;
      }
    }

    return ValidationResult.Success;
  }
}