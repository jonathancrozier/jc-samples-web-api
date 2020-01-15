using System.Net;

namespace JC.Samples.Web.Api.Errors.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="HttpStatusCode"/>.
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        #region Methods

        /// <summary>
        /// Gets an appropriate human-readable message for the <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="value">The <see cref="HttpStatusCode"/> to operate on</param>
        /// <returns>The message to return to the client</returns>
        public static string GetDefaultMessage(this HttpStatusCode value)
        {
            const string defaultErrorMessage = "A server error has occurred.";

            string message = defaultErrorMessage;

            switch (value)
            {
                case HttpStatusCode.BadRequest:
                    message = "An error occurred due to a bad request.";
                    break;
                case HttpStatusCode.Unauthorized:
                    message = "An authorisation error occurred.";
                    break;
                case HttpStatusCode.PaymentRequired: // Not currently used.                    
                    break;
                case HttpStatusCode.Forbidden:
                    message = "The requested operation is forbidden.";
                    break;
                case HttpStatusCode.NotFound:
                    message = "The item you are looking for cannot be found.";
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    message = "The function you are trying to perform is not allowed.";
                    break;
                case HttpStatusCode.NotAcceptable:
                    message = "The content cannot be provided in the requested format.";
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    message = "A proxy authentication error occurred.";
                    break;
                case HttpStatusCode.RequestTimeout:
                    message = "The request has timed out.";
                    break;
                case HttpStatusCode.Conflict:
                    message = "A resource conflict occurred.";
                    break;
                case HttpStatusCode.Gone:
                    message = "The requested resource has been deleted.";
                    break;
                case HttpStatusCode.LengthRequired:
                    message = "The length of the content must be specified.";
                    break;
                case HttpStatusCode.PreconditionFailed:
                    message = "A precondition has not been met.";
                    break;
                case HttpStatusCode.RequestEntityTooLarge:
                    message = "The request payload is too large";
                    break;
                case HttpStatusCode.RequestUriTooLong:
                    message = "The request URI is too long";
                    break;
                case HttpStatusCode.UnsupportedMediaType:
                    message = "The media format of the requested data is not supported.";
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    message = "The specified range is invalid.";
                    break;
                case HttpStatusCode.ExpectationFailed:
                    message = "The specified expectation cannot be met.";
                    break;
                case HttpStatusCode.UpgradeRequired:
                    message = "The protocol must be upgraded before proceeding.";
                    break;
                case HttpStatusCode.InternalServerError:
                    message = "The request failed due to an internal error.";
                    break;
                case HttpStatusCode.NotImplemented:
                    message = "The requested operation has not been implemented.";
                    break;
                case HttpStatusCode.BadGateway:
                    message = "Failed to get a valid server response.";
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    message = "The server is not ready to handle the request.";
                    break;
                case HttpStatusCode.GatewayTimeout:
                    message = "Failed to get a valid server response within the timeout period.";
                    break;
                case HttpStatusCode.HttpVersionNotSupported:
                    message = "The requested HTTP Version is not supported.";
                    break;
                default:
                    message = defaultErrorMessage;
                    break;
            }

            return message;
        }

        #endregion
    }
}