using System.ComponentModel.DataAnnotations;

namespace InsonusK.Validation.Test.ValidatorTest.Mocks;

public interface TestInterface
{
  public class TestClass : TestInterface
  {
    public string FieldWithValidation1 { get; set; }
  }

  [MaxLength(3)]
  [Required]
  public string FieldWithValidation1 { get; set; }
}


