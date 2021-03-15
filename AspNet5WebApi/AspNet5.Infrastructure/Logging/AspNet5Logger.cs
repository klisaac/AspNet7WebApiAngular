using AspNet5.Core.Logging;
using Microsoft.Extensions.Logging;

namespace AspNet5.Infrastructure.Logging
{
    public class AspNet5Logger<T> : IAspNet5Logger<T>
    {
        private readonly ILogger<T> _logger;

        public AspNet5Logger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
    }
}
