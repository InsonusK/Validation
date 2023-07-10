using System.Net;
using Xunit.Abstractions;
using InsonusK.Validation.Test.Tools;
using Microsoft.Extensions.Logging;
using InsonusK.Validation.Test.ValidatorTest.Mocks;
using System.ComponentModel.DataAnnotations;

namespace InsonusK.Validation.Test.ValidatorTest;

public class ValidateProperty_Test : BaseTest
{

  public ValidateProperty_Test(ITestOutputHelper output) : base(output)
  {

  }

  [Fact]
  public void WHEN_give_property_THEN_validate_only_property()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation2) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usingObject.FieldWithValidation2, validationContext, assertedValidationResult);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is Valid", () => Assert.True(assertedIsValid));
    Expect("ValidationResultList is empty", () => Assert.Empty(assertedValidationResult));

    #endregion
  }

  [Fact]
  public void WHEN_give_incorrect_property_THEN_validate_only_property()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation1) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usingObject.FieldWithValidation1, validationContext, assertedValidationResult);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is InValid", () => Assert.False(assertedIsValid));
    Expect("ValidationResultList has one record", () => Assert.Single(assertedValidationResult), out var assertedResult);
    Expect("Member names is single", () => Assert.Single(assertedResult.MemberNames), out var assertedMemberName);
    Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation1", assertedMemberName));
    #endregion
  }

  [Fact]
  public void WHEN_give_multi_incorrect_property_THEN_validate_only_property()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObjectMultiAnnotation() { FieldWithValidation1 = "45563464" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation1) };
    var assertedValidationResults = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty(usingObject.FieldWithValidation1, validationContext, assertedValidationResults);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is InValid", () => Assert.False(assertedIsValid));
    Expect("ValidationResultList has 2 records", () => Assert.Equal(2, assertedValidationResults.Count));

    ExpectGroup("First error is about prop 1", () =>
    {
      var assertedValidationResult = assertedValidationResults[0];
      Expect("Member names is single", () => Assert.Single(assertedValidationResult.MemberNames), out var assertedMemberName);
      Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation1", assertedMemberName));
      Logger.LogInformation($"Error for prop1: {assertedValidationResult.ErrorMessage}");
    });

    ExpectGroup("Second error is about prop 1", () =>
    {
      var assertedValidationResult = assertedValidationResults[1];
      Expect("Member names is single", () => Assert.Single(assertedValidationResult.MemberNames), out var assertedMemberName);
      Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation1", assertedMemberName));
      Logger.LogInformation($"Error for prop1: {assertedValidationResult.ErrorMessage}");
    });
    #endregion
  }

  [Fact]
  public void WHEN_give_different_correct_value_THEN_check_given_value()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation1) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty("t", validationContext, assertedValidationResult);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is Valid", () => Assert.True(assertedIsValid));
    Expect("ValidationResultList is empty", () => Assert.Empty(assertedValidationResult));
    #endregion
  }

  [Fact]
  public void WHEN_give_different_incorrect_value_THEN_check_given_value()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");
    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation2) };
    var assertedValidationResult = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateProperty("teoueooeueo", validationContext, assertedValidationResult);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is InValid", () => Assert.False(assertedIsValid));
    Expect("ValidationResultList has one record", () => Assert.Single(assertedValidationResult), out var assertedResult);
    Expect("Member names is single", () => Assert.Single(assertedResult.MemberNames), out var assertedMemberName);
    Expect("Member name is FieldWithValidation2", () => Assert.Equal("FieldWithValidation2", assertedMemberName));
    #endregion
  }

  [Fact]
  public void WHEN_MemberName_is_not_actual_member_THEN_get_error()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");
    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = "TestMemberName" };
    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");
    Expect("Raise error", () =>
      Assert.Throws<ArgumentException>(() =>
        Validator.TryValidateProperty("teoueooeueo", validationContext, new List<ValidationResult>())
      )
    );
    #endregion
  }

  [Fact]
  public void WHEN_you_reuse_validation_context_THEN_()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");
    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT 1");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation1) };
    var assertedValidationResults = new List<ValidationResult>();
    var assertedIsValid1 = Validator.TryValidateProperty("Wrong value for prop1", validationContext, assertedValidationResults);

    Logger.LogDebug("Test ACT 2");

    validationContext.MemberName = nameof(usingObject.FieldWithValidation2);
    var assertedIsValid2 = Validator.TryValidateProperty("Wrong value for prop2", validationContext, assertedValidationResults);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result 1 is InValid", () => Assert.False(assertedIsValid1));
    Expect("Result 2 is InValid", () => Assert.False(assertedIsValid2));
    Expect("ValidationResultList has 2 records", () => Assert.Equal(2, assertedValidationResults.Count));

    ExpectGroup("First error is about prop 1", () =>
    {
      var assertedValidationResult = assertedValidationResults[0];
      Expect("Member names is single", () => Assert.Single(assertedValidationResult.MemberNames), out var assertedMemberName);
      Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation1", assertedMemberName));
      Logger.LogInformation($"Error for prop1: {assertedValidationResult.ErrorMessage}");
    });

    ExpectGroup("First error is about prop 2", () =>
    {
      var assertedValidationResult = assertedValidationResults[1];
      Expect("Member names is single", () => Assert.Single(assertedValidationResult.MemberNames), out var assertedMemberName);
      Expect("Member name is FieldWithValidation1", () => Assert.Equal("FieldWithValidation2", assertedMemberName));
      Logger.LogInformation($"Error for prop1: {assertedValidationResult.ErrorMessage}");
    });
    #endregion
  }


  [Fact]
  public void WHEN_give_incorrect_property_THEN_raise_expection()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject) { MemberName = nameof(usingObject.FieldWithValidation1) };
    var assertedValidationResult = new List<ValidationResult>();

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Raise Exception", () =>
      Assert.Throws<ValidationException>(() =>
        Validator.ValidateProperty("Wrong value for field", validationContext)
      ),
      out ValidationException assertedException
    );
    Expect("Value in exception: Wrong value for field", () =>
      Assert.Equal("Wrong value for field", assertedException.Value)
    );
    Logger.LogInformation(assertedException.Message);

    Expect("Validation result not null", () =>
      Assert.NotNull(assertedException.ValidationResult));
    Expect("Member names is single", () =>
      Assert.Single(assertedException.ValidationResult.MemberNames),
      out var assertedMemberName
    );
    Expect("Member name is FieldWithValidation1", () =>
      Assert.Equal("FieldWithValidation1", assertedMemberName));
    Logger.LogInformation($"Error for prop1: {assertedException.ValidationResult.ErrorMessage}");
    #endregion
  }
}