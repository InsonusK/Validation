using System.ComponentModel.DataAnnotations;

namespace InsonusK.Validation.Test.AttributeTest.Mocks;

public class RequiredTestClass
{

  [Required]
  public int? IntProp { get; set; }
}