using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InsonusK.Validation.Test.ValidatorTest.Mocks;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
class ContainAtribute : ValidationAttribute
{
  private readonly string containSubString;

  public ContainAtribute(string containSubString)
  {
    this.containSubString = containSubString;
  }

  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value != null && value.ToString()!.Contains(containSubString))
    {
      return ValidationResult.Success;
    }
    return new ValidationResult($"Value {validationContext.MemberName} doesn't contain {containSubString}", new[] { validationContext.MemberName });
  }
}