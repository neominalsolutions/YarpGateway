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

  }
}
