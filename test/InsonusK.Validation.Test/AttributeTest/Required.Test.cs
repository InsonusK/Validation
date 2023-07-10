namespace InsonusK.Validation.Test.AttributeTest;

using System.ComponentModel.DataAnnotations;
using System.Net;
using InsonusK.Validation.Test.AttributeTest.Mocks;
using InsonusK.Validation.Test.Tools;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
public class Required_Test : BaseTest
{

  public Required_Test(ITestOutputHelper output) : base(output)
  {

  }

  [Theory]
  [InlineData(null)]
  public void WHEN_int_value_null_THEN_error(int? initValue)
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usedObj = new RequiredTestClass() { IntProp = initValue };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usedObj) { MemberName = nameof(usedObj.IntProp) };
    var validationResults = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usedObj.IntProp, validationContext, validationResults);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Object invalid", () => Assert.False(assertedIsValid));
    ExpectGroup("Has invalid IntProp", () =>
    {
      Expect("ValidationResults has one error", () =>
        Assert.Single(validationResults),
        out var assertedValidationResult);
      Expect("ValidationResult has 1 member name", () =>
        Assert.Single(assertedValidationResult.MemberNames),
        out var memberName);
      Expect("Member name is IntProp", () =>
        Assert.Equal("IntProp", memberName));

      Logger.LogInformation("Validation error: " + assertedValidationResult.ErrorMessage);
    });
    #endregion
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  [InlineData(1)]
  public void WHEN_int_value_LE_0_THEN_error(int? initValue)
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usedObj = new RequiredTestClass() { IntProp = initValue };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usedObj) { MemberName = nameof(usedObj.IntProp) };
    var validationResults = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usedObj.IntProp, validationContext, validationResults);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Object valid", () => Assert.True(assertedIsValid));
    #endregion
  }
}