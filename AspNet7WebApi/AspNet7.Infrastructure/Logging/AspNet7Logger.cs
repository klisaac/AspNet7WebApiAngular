using AspNet7.Core.Logging;
using Microsoft.Extensions.Logging;

namespace AspNet7.Infrastructure.Logging
{
    public class AspNet7Logger<T> : IAspNet7Logger<T>
    {
        private readonly ILogger<T> _logger;

        public AspNet7Logger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void Warning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void Information(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
    }
}
