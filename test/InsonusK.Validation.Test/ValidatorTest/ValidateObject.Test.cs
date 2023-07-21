using System.ComponentModel.DataAnnotations;
using System.Net;
using InsonusK.Validation.Test.Tools;
using InsonusK.Validation.Test.ValidatorTest.Mocks;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
namespace InsonusK.Validation.Test.ValidatorTest;
public class ValidateObject_Test : BaseTest
{

  public ValidateObject_Test(ITestOutputHelper output) : base(output)
  {

  }

  [Fact]
  public void WHEN_give_incorrect_object_and_specify_validate_values_THEN_object_validate()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124", FieldWithValidation2 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");
    var validationContext = new ValidationContext(usingObject);
    var assertedValidationResults = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateObject(usingObject, validationContext, assertedValidationResults);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Result is Valid", () => Assert.True(assertedIsValid));

    Logger.LogWarning("""
    If parameter validateAllProperties into Validator.[Try]ValidateObject doesn't pass or equal false.
    alidator checks on [Required] annotiotion.
    https://github.com/dotnet/runtime/blob/main/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Validator.cs#L482
    
    Call stack:
    https://github.com/dotnet/runtime/blob/main/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Validator.cs#L250
    https://github.com/dotnet/runtime/blob/main/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Validator.cs#L146
    https://github.com/dotnet/runtime/blob/main/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Validator.cs#L411
    https://github.com/dotnet/runtime/blob/main/src/libraries/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/Validator.cs#L482
    """);

    #endregion
  }

  [Fact]
  public void WHEN_give_incorrect_object_THEN_object_validate()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "123124", FieldWithValidation2 = "123124" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");
    var validationContext = new ValidationContext(usingObject);
    var assertedValidationResults = new List<ValidationResult>();
    var assertedIsValid = Validator.TryValidateObject(usingObject, validationContext, assertedValidationResults, true);


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
  public void WHEN_give_incorrect_object_THEN_raise_expection()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject() { FieldWithValidation1 = "1234", FieldWithValidation2 = "4321" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject);
    var assertedValidationResult = new List<ValidationResult>();

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Raise Exception", () =>
      Assert.Throws<ValidationException>(() =>
        Validator.ValidateObject(usingObject, validationContext, true)
      ),
      out ValidationException assertedException
    );
    Expect("Value in exception: 1234", () =>
      Assert.Equal("1234", assertedException.Value)
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

  [Fact]
  public void WHEN_validate_object_THEN_member_name_in_validation_context_is_correct()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usingObject = new TestObject2() { FieldWithValidation1 = "345", FieldWithValidation2 = "678" };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var validationContext = new ValidationContext(usingObject);
    var assertedValidationResult = new List<ValidationResult>();

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Raise Exception", () =>
      Assert.Throws<ValidationException>(() =>
        Validator.ValidateObject(usingObject, validationContext, true)
      ),
      out ValidationException assertedException
    );
    Expect("Value in exception: 345", () =>
      Assert.Equal("345", assertedException.Value)
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