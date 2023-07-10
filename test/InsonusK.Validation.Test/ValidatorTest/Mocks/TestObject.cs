using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InsonusK.Validation.Test.ValidatorTest.Mocks;

public class TestObject
{

  public string FieldNoValidation { get; set; } = "";

  [MaxLength(3)]
  public string FieldWithValidation1 { get; set; } = "";
  [MaxLength(3)]
  public string FieldWithValidation2 { get; set; } = "";
}

public class TestObject2
{

  public string FieldNoValidation { get; set; } = "";

  [ContainAtribute("1")]
  public string FieldWithValidation1 { get; set; } = "";
  [ContainAtribute("1")]
  public string FieldWithValidation2 { get; set; } = "";
}