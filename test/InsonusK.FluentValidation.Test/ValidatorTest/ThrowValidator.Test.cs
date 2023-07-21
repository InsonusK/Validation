
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using InsonusK.Xunit.ExpectationsTest;
using FluentValidation;

namespace InsonusK.FluentValidation.Test.ValidatorTest;


/// <summary>
/// Тесты валидации свойст на основе их атрибутов
/// </summary>
public class ThrowValidation_Test : ExpectationsTestBase
{

  public ThrowValidation_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }

  public class ParentValidator : AbstractValidator<ParentClass>
  {
    public ParentValidator()
    {
      RuleFor(e => e.PFieldLen3).Length(1, 3).WithMessage("Len must be from 1 to 3");
      RuleForEach(e => e.children).SetValidator(new ChildValidator());
    }
  }

  public class ChildValidator : AbstractValidator<ChildClass>
  {
    public ChildValidator()
    {
      RuleFor(e => e.CFieldLen3).Length(1, 3).WithMessage("Len must be from 1 to 3");
    }
  }

  public class ParentClass
  {
    public string PFieldLen3 { get; set; }

    public List<ChildClass> children { get; set; }
  }

  public class ChildClass
  {
    public string CFieldLen3 { get; set; }

  }

  [Fact]
  public void WHEN_several_errors_THEN_throw_with_all_errors()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new ThrowValidation_Test.ParentClass()
    {
      PFieldLen3 = "1234",
      children = new List<ChildClass>(){
        new ThrowValidation_Test.ChildClass(){ CFieldLen3 = "1234"}
      }
    };

    var usingValidator = new ParentValidator();
    #endregion


    #region Act
    Logger.LogDebug("Test ACT");




    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Throw exception", () => Assert.Throws<ValidationException>(() => usingValidator.ValidateAndThrow(usingObject)), out var assertedException);
    Expect("Exception contain 2 errors", () => Assert.Equal(2, assertedException.Errors.Count()));
    ExpectGroup("First error is about parent field", () =>
    {
      var assertError = assertedException.Errors.ElementAt(0);
      Expect($"Field name is {nameof(ParentClass.PFieldLen3)}", () => Assert.Equal(nameof(ParentClass.PFieldLen3), assertError.PropertyName));
    });
    ExpectGroup("First error is about chield field", () =>
    {
      var assertError = assertedException.Errors.ElementAt(1);
      Expect($"Field name is {nameof(ChildClass.CFieldLen3)}", () => Assert.Equal(nameof(ParentClass.children) + "[0]." + nameof(ChildClass.CFieldLen3), assertError.PropertyName));
    });
    #endregion
  }

}
