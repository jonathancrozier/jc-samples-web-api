using Newtonsoft.Json;
using System.Collections.Generic;
using JC.Samples.Web.Api.Errors.Base;

namespace JC.Samples.Web.Api.Errors
{
    // <summary>
    /// A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807.
    /// Contains 'extended members' which are allowable in accordance with the official RFC.
    /// </summary>
    public class ApiProblemDetails : ProblemDetails
    {
        #region Properties

        /// <summary>
        /// The API Error Code, represented as a string value.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The API Error Category, represented as a string value.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// A collection of model Validation Errors relating to the API request.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<ValidationError> ValidationErrors { get; set; }

        #endregion
    }
}