
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using InsonusK.Xunit.ExpectationsTest;
using FluentValidation;

namespace InsonusK.FluentValidation.Test.ValidatorTest;


/// <summary>
/// Тесты валидации свойст на основе их атрибутов
/// </summary>
public class SplittedValidators_Test : ExpectationsTestBase
{

  public SplittedValidators_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }

  public class ParentValidator : AbstractValidator<ParentClass>
  {
    public ParentValidator()
    {
      RuleFor(e => e.FieldLen3).SetValidator(new FieldValidator());
      RuleForEach(e => e.children).SetValidator(new ChildValidator());
    }
  }

  public class ChildValidator : AbstractValidator<ChildClass>
  {
    public ChildValidator()
    {
      RuleFor(e => e.FieldLen3).SetValidator(new FieldValidator());
    }
  }

  public class FieldValidator : AbstractValidator<string>
  {
    public FieldValidator()
    {
      RuleFor(e => e).Length(1, 3).WithMessage("Len must be from 1 to 3");
    }
  }

  public class ParentClass
  {
    public string FieldLen3 { get; set; }

    public List<ChildClass> children { get; set; }
  }

  public class ChildClass
  {
    public string FieldLen3 { get; set; }

  }

  [Fact]
  public void WHEN_child_is_invalid_THEN_()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new SplittedValidators_Test.ParentClass()
    {
      FieldLen3 = "123",
      children = new List<ChildClass>(){
        new SplittedValidators_Test.ChildClass(){ FieldLen3 = "1234"}
      }
    };

    var usingValidator = new ParentValidator();
    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var assertedResult = usingValidator.Validate(usingObject);


    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is InValid", () => Assert.False(assertedResult.IsValid));


    foreach (var error in assertedResult.Errors)
    {
      this.Logger.LogInformation($"{nameof(error.PropertyName)}: {error.PropertyName}");
      this.Logger.LogInformation($"{nameof(error.ErrorMessage)}: {error.ErrorMessage}");
    }

    #endregion
  }

}
