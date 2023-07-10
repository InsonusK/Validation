using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InsonusK.Validation.Test.ValidatorTest.Mocks;

public class TestObjectMultiAnnotation
{

  public string FieldNoValidation { get; set; } = "";

  [MaxLength(3)]
  [ContainAtribute("1")]
  public string FieldWithValidation1 { get; set; } = "";

  [MaxLength(3)]
  [ContainAtribute("2")]
  public string FieldWithValidation2 { get; set; } = "";
}