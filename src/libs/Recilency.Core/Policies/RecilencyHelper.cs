using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recilency.Core.Policies
{
  public static class RecilencyHelper
  {

    public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy(int retryCount,TimeSpan sleepDuration)
    {
      return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(retryCount, x =>
      {
        Console.WriteLine("İstek başlatılıdı");
        return sleepDuration;
      }, onRetry: (outcome,timespan,retryCount,context) =>
      {
        Console.WriteLine("İstek tekrar deneniyor");
      });
    }

    public static IAsyncPolicy<HttpResponseMessage> CreateTimeoutPolicy(TimeSpan timeout)
    {
      return Policy.TimeoutAsync<HttpResponseMessage>(timeout, (context, timespan, task) =>
      {
        Console.WriteLine("İstek zaman aşımına uğradı");
        return Task.CompletedTask;
      });
    }


    public static IAsyncPolicy<HttpResponseMessage> CreateCircuitBrakerPolicy(int errorCount, TimeSpan durationOfBreak)
    {
      return HttpPolicyExtensions.HandleTransientHttpError()
        .Or<Exception>().CircuitBreakerAsync(errorCount, durationOfBreak, onBreak: (exception, duration) =>
      {
        Console.WriteLine("Kesintiye uğradığında");
      }, onReset: () =>
      {
        Console.WriteLine("durationOfBreak kadar bekletipi hizmet yeniden açılıdığında");
      });
    }


  }
}
