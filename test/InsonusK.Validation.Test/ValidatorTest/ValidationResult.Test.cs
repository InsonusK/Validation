using System.ComponentModel.DataAnnotations;
using System.Net;
using InsonusK.Validation.Test.Tools;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
namespace InsonusK.Validation.Test.ValidatorTest;
public class ValidationResult_Test : BaseTest
{

  public ValidationResult_Test(ITestOutputHelper output) : base(output)
  {

  }

  [Fact]
  public void WHEN_to_string_THEN_get_text()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var assertedResult = new ValidationResult("Test message", new[] { "m1", "m2" });

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");



    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Logger.LogInformation(assertedResult.ToString());

    #endregion
  }
}