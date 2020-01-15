using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace JC.Samples.Web.Api.Errors.Handlers
{
    /// <summary>
    /// Handles exceptions centrally for the application.
    /// </summary>
    public class ApiExceptionHandler : ExceptionHandler
    {
        #region Methods

        /// <summary>
        /// Handles the exception asynchronously.
        /// </summary>
        /// <param name="context">The exception handler context</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests</param>
        /// <returns>A Task representing the asynchronous exception handling operation</returns>
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // First of all, check if the exception should be handled.
            if (!ShouldHandle(context))
            {
                return Task.FromResult(0);
            }

            // Create the API Problem Details model.
            var problemDetails = new ApiProblemDetails();

            if (context.Exception is UnauthorizedAccessException)
            {
                // Unauthorised.
                problemDetails = ApiErrorFactory.Create(new ApiException(HttpStatusCode.Unauthorized));
            }
            else if (context.Exception is ApiException)
            {
                // Custom API error.
                problemDetails = ApiErrorFactory.Create(context.Exception as ApiException);
            }
            else
            {
                // An unhandled exception occurred.
                problemDetails = ApiErrorFactory.Create(new ApiException(HttpStatusCode.InternalServerError));
            }

            // Log the error.
            Trace.TraceError(problemDetails.Instance);

            // Create and return the error response.
            var response = context.Request.CreateResponse((HttpStatusCode)problemDetails.Status, problemDetails);

            context.Result = new ResponseMessageResult(response);

            return Task.FromResult(0);
        }

        #endregion
    }
}