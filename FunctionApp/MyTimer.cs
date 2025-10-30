using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public class MyTimer
    {
        private readonly ILogger _logger;

        public MyTimer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyTimer>();
        }

        [Function("MyTimer")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            var x = Environment.GetEnvironmentVariable("StorageAccount");
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"{x} Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
