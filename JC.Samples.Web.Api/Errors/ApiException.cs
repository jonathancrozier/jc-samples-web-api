using System;
using System.Collections.Generic;
using System.Net;

namespace JC.Samples.Web.Api.Errors
{
    /// <summary>
    /// API Exception which wraps the details of a HTTP API error.
    /// </summary>
    public class ApiException : Exception
    {
        #region Properties

        /// <summary>
        /// The Error Category.
        /// </summary>
        public ApiErrorCategory ErrorCategory { get; set; }

        /// <summary>
        /// The Error Code.
        /// </summary>
        public ApiErrorCode ErrorCode { get; set; }

        /// <summary>
        /// The Error Message (detail).
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The HTTP Status Code.
        /// </summary>
        public HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.InternalServerError;

        /// <summary>
        /// The model Validation Errors (if any).
        /// </summary>
        public ICollection<ValidationError> ValidationErrors { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="httpStatus">The HTTP Status Code (status)</param>
        /// <param name="errorCode">The custom API Error Code (urn: defaults to 'none')</param>
        /// <param name="errorMessage">The error message (detail)</param>
        /// <param name="errorCategory">The error category (urn: defaults to 'protocol')</param>
        /// <param name="validationErrors">The model Validation Errors (if any)</param>
        public ApiException(HttpStatusCode               httpStatus,
                            ApiErrorCode                 errorCode        = ApiErrorCode.None,
                            string                       errorMessage     = null,
                            ApiErrorCategory             errorCategory    = ApiErrorCategory.None,
                            ICollection<ValidationError> validationErrors = null)
        {
            if (errorCode != ApiErrorCode.None && errorCategory == ApiErrorCategory.None)
            {
                // Default the Category to 'Custom' if a custom Code has 
                // been specified and a Category has not been specified.
                errorCategory = ApiErrorCategory.Custom;
            }
            else if (errorCategory == ApiErrorCategory.None)
            {
                // Default the Category to 'Protocol' if a Category has not been specified.
                errorCategory = ApiErrorCategory.Protocol;
            }

            HttpStatus       = httpStatus;
            ErrorMessage     = errorMessage;
            ErrorCode        = errorCode;
            ErrorCategory    = errorCategory;
            ValidationErrors = validationErrors;
        }

        #endregion
    }
}