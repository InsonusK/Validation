
using System.ComponentModel.DataAnnotations;
using InsonusK.Validation.Test.Tools;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
namespace InsonusK.Validation.Test.ValidatorTest;
/// <summary>
/// Тесты валидации свойст на основе их атрибутов
/// </summary>
public class SubValidation_Test : BaseTest
{

  public SubValidation_Test(ITestOutputHelper output) : base(output)
  {

  }

  public class ParentClass
  {
    [MaxLength(3)]
    public string FieldLen3 { get; set; }

    public List<ChildClass> children { get; set; }
  }

  public class ChildClass
  {
    [MaxLength(3)]
    public string FieldLen3 { get; set; }

  }

  [Fact]
  public void WHEN_child_is_invalid_THEN_DOT_NOT_SEE_IT()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new SubValidation_Test.ParentClass()
    {
      FieldLen3 = "123",
      children = new List<ChildClass>(){
        new SubValidation_Test.ChildClass(){ FieldLen3 = "1234"}
      }
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.children) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usingObject.children, validationContext, assertedValidationResult);


    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is valid", () => Assert.True(assertedIsValid));
    #endregion
  }

}