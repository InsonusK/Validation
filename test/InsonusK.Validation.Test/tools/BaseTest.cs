using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace InsonusK.Validation.Test.Tools;

public class BaseTest : LoggingTestsBase
{
  public BaseTest(ITestOutputHelper output) : base(output, LogLevel.Debug)
  {

  }

  protected void Expect(string exception, Action assertAction)
  {
    try
    {
      assertAction.Invoke();
    }
    catch (System.Exception ex)
    {
      Logger.LogInformation($"{exception} - Failed");
      throw ex;
    }
    Logger.LogInformation($"{exception} - Checked");
  }
  protected void Expect<TRet>(string exception, Func<TRet> assertFunc, out TRet returnObject)
  {
    try
    {
      returnObject = assertFunc.Invoke();
    }
    catch (System.Exception ex)
    {
      Logger.LogInformation($"{exception} - Failed");
      throw ex;
    }
    Logger.LogInformation($"{exception} - Checked");
  }
  protected void ExpectGroup(string exception, Action assertAction)
  {
    Logger.LogInformation($"{exception} - Checking");
    try
    {
      assertAction.Invoke();
    }
    catch (System.Exception ex)
    {
      Logger.LogInformation($"{exception} - Failed");
      throw ex;
    }
    Logger.LogInformation($"{exception} - Checked");
  }
  protected void ExpectGroup<TRet>(string exception, Func<TRet> assertFunc, out TRet returnObject)
  {
    Logger.LogInformation($"{exception} - Checking");
    try
    {
      returnObject = assertFunc.Invoke();
    }
    catch (System.Exception ex)
    {
      Logger.LogInformation($"{exception} - Failed");
      throw ex;
    }
    Logger.LogInformation($"{exception} - Checked");
  }
}