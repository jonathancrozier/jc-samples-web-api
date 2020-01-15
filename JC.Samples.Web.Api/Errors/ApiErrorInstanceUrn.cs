using JC.Samples.Web.Api.Extensions;
using System;
using System.Linq;
using System.Net;

namespace JC.Samples.Web.Api.Errors
{
    /// <summary>
    /// Encapsulates an API Error Instance URN.
    /// </summary>
    public class ApiErrorInstanceUrn
    {
        #region Properties

        #region Public

        /// <summary>
        /// [0] The standard URN prefix.
        /// </summary>
        public string UrnPrefix => "urn";

        /// <summary>
        /// [1] The Namespace Identifier (Company).
        /// </summary>
        public string NamespaceIdentifier => "jc";

        /// <summary>
        /// [2] The first part of the Namespace Specific String (API).
        /// </summary>
        public string NssApiText => "api";

        /// <summary>
        /// [3] The second part of the Namespace Specific String (Error Indicator).
        /// </summary>
        public string NssErrorText => "error";

        /// <summary>
        /// [4] The third part of the Namespace Specific String (Error Category).
        /// </summary>
        public ApiErrorCategory ErrorCategory { get; }

        /// <summary>
        /// [5] The fourth part of the Namespace Specific String (Error Code).
        /// </summary>
        public ApiErrorCode ErrorCode { get; }

        /// <summary>
        /// [6] The fifth part of the Namespace Specific String (Instance GUID).
        /// </summary>
        public Guid InstanceGuid { get; }

        #endregion

        #region Internal

        /// <summary>
        /// The Error Category as a camelCase string.
        /// </summary>
        public string ErrorCategoryString => ErrorCategory.ToString().ToCamelCase();

        /// <summary>
        /// The Error Code as a camelCase string.
        /// </summary>
        public string ErrorCodeString => ErrorCode != ApiErrorCode.None ? ErrorCode.ToString().ToCamelCase() : _httpStatusCode.ToString().ToCamelCase();

        #endregion

        #endregion

        #region Readonlys

        private readonly HttpStatusCode _httpStatusCode;

        #endregion

        #region Constructor

        /// <summary>
        /// Main constructor.
        /// Populates the API Error Instance URN object from the specified API Exception.
        /// </summary>
        /// <param name="apiException">The API Exception relating to the API Error</param>
        public ApiErrorInstanceUrn(ApiException apiException)
        {
            _httpStatusCode = apiException.HttpStatus;

            ErrorCategory = apiException.ErrorCategory;
            ErrorCode     = apiException.ErrorCode;
            InstanceGuid  = Guid.NewGuid();
        }

        /// <summary>
        /// Secondary constructor.
        /// Populates the API Error Instance URN object from the specified URN string.
        /// </summary>
        /// <param name="urn">The URN string to parse</param>
        public ApiErrorInstanceUrn(string urn)
        {
            var urnSplit   = urn.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            int splitCount = urnSplit.Count();

            if (splitCount < 7) return;

            Enum.TryParse(urnSplit[4], ignoreCase: true, result: out ApiErrorCategory errorCategory);
            Enum.TryParse(urnSplit[5], ignoreCase: true, result: out ApiErrorCode errorCode);
            Guid.TryParse(urnSplit[6], out Guid instanceGuid);

            ErrorCategory = errorCategory;
            ErrorCode     = errorCode;
            InstanceGuid  = instanceGuid;

            if (ErrorCode == ApiErrorCode.None)
            {
                Enum.TryParse(urnSplit[5], ignoreCase: true, result: out HttpStatusCode httpStatusCode);

                _httpStatusCode = httpStatusCode;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the standard 'ToString' method with the details of the API Error Instance URN.
        /// </summary>
        /// <returns>The API Error Instance as a URN string</returns>
        public override string ToString()
        {
            // e.g. urn:jc:api:error:general:badRequest:a4baf437-1dd2-4d27-8076-87b256ccd3f2
            string instance = $"urn:" +                   // URN prefix.
                              $"{NamespaceIdentifier}:" + // Namespace identifier.
                              $"{NssApiText}:" +          // Namespace-specific string (API).
                              $"{NssErrorText}:" +        // Namespace-specific string (Error Indicator).
                              $"{ErrorCategoryString}:" + // Error Category.
                              $"{ErrorCodeString}:" +     // Error code.
                              $"{InstanceGuid}";          // Error instance identifier.
            return instance;
        }

        #endregion
    }
}