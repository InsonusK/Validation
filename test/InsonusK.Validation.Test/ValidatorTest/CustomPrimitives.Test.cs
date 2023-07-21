

using System.ComponentModel.DataAnnotations;
using InsonusK.Validation.Test.Tools;
using InsonusK.Validation.Test.ValidatorTest.Mocks;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace InsonusK.Validation.Test.ValidatorTest;
public class CustomPrimitives_Test : BaseTest
{

  public CustomPrimitives_Test(ITestOutputHelper output) : base(output)
  {

  }
  public class TestObject
  {

    public IntPrimitive f1 { get; set; }
  }
  [Fact]
  public void WHEN_validate_obj_with_it_THEN_validation_work()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usedObj = new TestObject()
    {
      f1 = 5
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var context = new ValidationContext(usedObj);
    context.MemberName = "f1";
    var assertedResult = new List<ValidationResult>();
    var assertedValidation = Validator.TryValidateProperty(usedObj.f1, context, assertedResult);
    #endregion

    #region Assert
    Logger.LogDebug("Test ASSERT");

    Expect("Validation is not succesfull", () => Assert.False(assertedValidation));
    Expect("Result contain 2 errors", () => Assert.Equal(2, assertedResult.Count()));
    Logger.LogInformation(string.Join("\n", assertedResult.Select(r => r.ToString()).ToArray()));

    #endregion
  }
}