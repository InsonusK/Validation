namespace InsonusK.Validation.Test.ValidatorTest;

using System.ComponentModel.DataAnnotations;
using InsonusK.Validation.Test.Tools;
using InsonusK.Validation.Test.ValidatorTest.Mocks;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
public class ValidateObject_BasedOnIntervalceAttributes_Test : BaseTest
{

  public ValidateObject_BasedOnIntervalceAttributes_Test(ITestOutputHelper output) : base(output)
  {

  }
  /*
  [Fact]
  public void WHEN_validate_property_THEN_validate_used_interface_attributes()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObj = new TestInterface.TestClass() { FieldWithValidation1 = "12345" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObj) { MemberName = nameof(usingObj.FieldWithValidation1) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty("teoueooeueo", validationContext, assertedValidationResult);


    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is InValid", () => Assert.False(assertedIsValid));
    Expect("ValidationResultList has one record", () => Assert.Single(assertedValidationResult), out var assertedResult);
    Expect("Member names is single", () => Assert.Single(assertedResult.MemberNames), out var assertedMemberName);
    Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation1", assertedMemberName));

    #endregion
  }
  */
}