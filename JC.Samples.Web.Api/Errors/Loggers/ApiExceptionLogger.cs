using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace JC.Samples.Web.Api.Errors.Loggers
{
    /// <summary>
    /// Handles the logging of any exceptions centrally for the application.
    /// </summary>
    public class ApiExceptionLogger : ExceptionLogger
    {
        #region Methods

        /// <summary>
        /// Logs an exception asynchronously.
        /// </summary>
        /// <param name="context">The exception logger context</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests</param>
        /// <returns>A task representing the asynchronous exception logging operation</returns>
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            // First of all, check if the exception should be logged.
            if (!ShouldLog(context))
            {
                return Task.FromResult(0);
            }

            // Log the exception.
            Trace.TraceError(context.ExceptionContext.Exception.ToString());

            return Task.FromResult(0);
        }

        #endregion
    }
}